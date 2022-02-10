using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
<<<<<<< HEAD
using MongoDB.Bson.Serialization.Attributes;
=======
using Backend.Models.DTO;
>>>>>>> b2027b2779b6c2b5d02f870e7c48984500f5dd12

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
[BsonIgnoreExtraElements]
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
    [Route("AddReservation/{imeRestoran}")]
<<<<<<< HEAD
    public ActionResult AddReservation(string imeRestoran)
    { 
=======
    public IActionResult AddReservation([FromBody] RezervacijaDTO rez, string imeRestoran)
    {
        Restoran restoran = new Restoran();
        Korisnik user = new Korisnik();
>>>>>>> b2027b2779b6c2b5d02f870e7c48984500f5dd12
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        Rezervacija rezervacija = new Rezervacija();
        Korisnik user = new Korisnik();
        Restoran restoran = new Restoran();
        var database = server.GetDatabase("Dostavi");
        var email = HttpContext.User.Identity!.Name;

        var collection = database.GetCollection<Korisnik>("korisnik");
        var query = Query.EQ("Email", email);
        user = collection.Find(query).First();

        var collection2 = database.GetCollection<Restoran>("restoran");
        var query2 = Query.EQ("Naziv", imeRestoran);
        restoran = collection2.Find(query2).First();

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
