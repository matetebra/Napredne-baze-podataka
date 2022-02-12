using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Backend.Models.DTO;
using MongoDB.Driver.Linq;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class RezervacijaController : ControllerBase
{
    [HttpPost]
    [Route("AddReservation/{email}")]
    public IActionResult AddReservation([FromBody] RezervacijaDTO rez, string email)
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");
        var restoranCollection = database.GetCollection<Restoran>("restoran");

        var restourant = (from restoran in restoranCollection.AsQueryable<Restoran>()
                          where restoran.Email == email
                          select restoran).FirstOrDefault();
        if (restourant == null)
        {
            return BadRequest("Greskica");
        }

        var userCollection = database.GetCollection<Korisnik>("korisnik");

        var userEmail = HttpContext.User.Identity.Name;

        var user = (from korisnik in userCollection.AsQueryable<Korisnik>()
                    where korisnik.Email == userEmail
                    select korisnik).FirstOrDefault();

        var rezervacijaCollection = database.GetCollection<Rezervacija>("rezervacija");

        var provera = (from rezervacija in rezervacijaCollection.AsQueryable<Rezervacija>()
                       where rezervacija.KorisnikRezervacijaId == user.Id
                       where rezervacija.Datum == rez.Datum.Date
                       where rezervacija.RestoranRezervacijaId == restourant.Id
                       select rezervacija).FirstOrDefault();

        if (provera != null)
        {
            return BadRequest("Vec ste napravili rezervaciju za dati restoran");
        }

        var test = (from rezervacija in rezervacijaCollection.AsQueryable<Rezervacija>()
                    where rezervacija.Datum == rez.Datum.Date
                    select rezervacija.BrojMesta).ToList();
        var ukupno = 0;

        foreach (string i in test)
        {
            ukupno += Int32.Parse(i);
        }
        if ((ukupno + Int32.Parse(rez.BrojMesta!)) > restourant.Kapacitet)
        {
            return BadRequest("Nemamo slobodnih mesta trenutno!");
        }
        //proveriti da li ima mesta!!!
        Rezervacija r = new Rezervacija();
        var testss = new ObjectId();

        r.Id = testss;
        r.BrojMesta = rez.BrojMesta;
        r.Datum = rez.Datum.Date;
        r.Vreme = rez.Vreme;
        r.KorisnikRezervacijaId = user.Id;
        r.RestoranRezervacijaId = restourant.Id;

        rezervacijaCollection.Insert(r);

        return Ok("Uspesna rezervacija! ");
    }


    // [HttpPost]
    // [Route("BookMarkRestaurant/{email}")]
    // public IActionResult BookMarkRestaurant(string email)
    // {
    //     try
    //     {
    //         MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
    //         MongoServer server = client.GetServer();
    //         var database = server.GetDatabase("Dostavi");
    //         var restoranCollection = database.GetCollection<Restoran>("restoran");
    //         var usersCollection = database.GetCollection<Korisnik>("korisnik");

    //         var userEmail = HttpContext.User.Identity.Name;

    //         var user = (from korisnik in usersCollection.AsQueryable<Korisnik>()
    //                     where korisnik.Email == userEmail
    //                     select korisnik).FirstOrDefault();

    //         var getRestaurantQuery = (from restoran in restoranCollection.AsQueryable<Restoran>()
    //                                   where restoran.Email == email
    //                                   select restoran.Id).FirstOrDefault();
            
    //         if (getRestaurantQuery.ToString() == "000000000000000000000000")
    //         {
    //             return BadRequest("Nepostojeci restoran");
    //         }

    //         int check = 0;
    //         foreach (MongoDBRef mdb in user.RestoranKorisnikIdList)
    //         {
    //             if (mdb.Id == getRestaurantQuery)
    //             {
    //                 check++;
    //             }
    //         }
    //         if (check != 0)
    //         {
    //             return BadRequest("Vec ste bookmarkovali taj restoran");
    //         }

    //         var upit = Query.EQ("_id", user.Id);
    //         var update = Update.PushWrapped("RestoranKorisnikIdList", new MongoDBRef("restoran", getRestaurantQuery));
    //         usersCollection.Update(upit, update);
    //         return Ok("Uspesno bookmarkovanje");
    //     }
    //     catch (Exception exc)
    //     {
    //         return BadRequest(exc.Message);
    //     }
        
    // }

}