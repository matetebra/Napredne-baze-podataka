using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class RestoranController : ControllerBase
{
    [HttpGet]
    [Route("GetRestaurants")]
    public List<Restoran> GetRestaurants()
    {
  
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");
        var collection = database.GetCollection<Restoran>("restoran");

        MongoCursor<Restoran> restorani = collection.FindAll();
        List<Restoran> restorans = restorani.ToList();
        return restorans;
    }

    [HttpGet]
    [Route("GetRestaurantByName/{name}")]
    public ActionResult GetRestaurantByName(string name)
    {
        Restoran restoran = new Restoran();

        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var collection = database.GetCollection<Restoran>("restoran");

        var query = Query.EQ("Naziv", name);
        restoran = collection.Find(query).First();

        return Ok(restoran);
    }

    [HttpPost]
    [Route("AddRestaurants")]
    public ActionResult CreateRestaurants([FromBody] Restoran restoran)
    {
        //MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        //var server = MongoServer.Create(client);
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
       // MongoServer server = client.GetServer();
        var database = client.GetDatabase("Dostavi");

        var collection = database.GetCollection<Restoran>("restoran");
        collection.InsertOne(restoran);
        return Ok();
    }
}