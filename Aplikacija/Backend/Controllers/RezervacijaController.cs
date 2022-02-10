using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Backend.Models.DTO;
namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class RezervacijaController : ControllerBase
{
    [HttpGet]
    [Route("GetReservations")]
    public List<Rezervacija> GetReservations()
    {

        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");
        var collection = database.GetCollection<Rezervacija>("rezervacija");

        MongoCursor<Rezervacija> rezervacije = collection.FindAll();
        List<Rezervacija> rez = rezervacije.ToList();
        return rez;
    }


    [HttpPost]
    [Route("AddReservation/{emailRestoran}")]
    public IActionResult AddReservation([FromBody] RezervacijaDTO rez, string emailRestoran)
    {
        Restoran restoran = new Restoran();
        Korisnik user = new Korisnik();
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");
        var email = HttpContext.User.Identity!.Name;

        var collection = database.GetCollection<Korisnik>("korisnik");
        var query = Query.EQ("Email", email);
        user = collection.Find(query).FirstOrDefault();

        var collection2 = database.GetCollection<Restoran>("restoran");
        var query2 = Query.EQ("Email", emailRestoran);
        restoran = collection2.Find(query2).FirstOrDefault();

        Rezervacija rezervacija = new Rezervacija();

        rezervacija.BrojMesta = rez.BrojMesta;
        rezervacija.Datum = rez.Datum;
        rezervacija.Vreme = rez.Vreme;

        var collection3 = database.GetCollection<Rezervacija>("rezervacija");
        rezervacija.KorisnikRezervacijaId = new MongoDBRef("korisnik", user.Id);
        rezervacija.RestoranRezervacijaId = new MongoDBRef("restoran", restoran.Id);
        collection3.Insert(rezervacija);

        return Ok();
    }
}
