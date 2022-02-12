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
                       where rezervacija.KorisnikRezervacijaId.Id == user.Id
                       where rezervacija.Datum == rez.Datum
                       where rezervacija.RestoranRezervacijaId.Id == restourant.Id
                       select rezervacija).FirstOrDefault();

        if (provera != null)
        {
            return BadRequest("Vec ste napravili rezervaciju za dati restoran");
        }

        var test = (from rezervacija in rezervacijaCollection.AsQueryable<Rezervacija>()
                    where rezervacija.Datum == rez.Datum
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
        r.Datum = rez.Datum;
        r.Vreme = rez.Vreme;
        r.KorisnikRezervacijaId = new MongoDBRef("korisnik", user.Id);
        r.RestoranRezervacijaId = new MongoDBRef("restoran", restourant.Id);

        rezervacijaCollection.Insert(r);

        return Ok("Uspesna rezervacija! ");
    }






}
