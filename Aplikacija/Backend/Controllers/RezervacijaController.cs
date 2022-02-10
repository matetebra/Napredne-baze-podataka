using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

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
    [Route("AddReservation/{imeRestoran}")]
    public ActionResult AddReservation([FromBody] Rezervacija rezervacija, string imeRestoran)
    {
        Korisnik user = new Korisnik();
        Restoran restoran = new Restoran();
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");
        var email = HttpContext.User.Identity!.Name;

        var collection = database.GetCollection<Korisnik>("korisnik");
        var query = Query.EQ("Email", email);
        user = collection.Find(query).First();

        var collection2 = database.GetCollection<Restoran>("restoran");
        var query2 = Query.EQ("Naziv", imeRestoran);
        restoran = collection2.Find(query2).First();

        var collection3 = database.GetCollection<Rezervacija>("rezervacija");
        rezervacija.KorisnikRezervacijaId = new MongoDBRef("korisnik", user.Id);
        rezervacija.RestoranRezervacijaId = new MongoDBRef("restoran", restoran.Id);
        collection3.Insert(rezervacija);

        return Ok();
    }
}
