using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class KorisnikController : ControllerBase
{
    [HttpGet]
    [Route("GetUsers")]
    public List<Korisnik> GetUsers()
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Donesi");

        var collection = database.GetCollection<Korisnik>("korisnik");

        MongoCursor<Korisnik> korisnici = collection.FindAll();

        List<Korisnik> users = korisnici.ToList();
        return users;
    }

    [HttpPost]
    [Route("AddUsers")]

    public ActionResult CreateUser([FromBody] Korisnik korisnik)
    {
        //MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        //var server = MongoServer.Create(client);
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var collection = database.GetCollection<Korisnik>("korisnik");

        collection.Insert(korisnik);

        return Ok();
    }
}
