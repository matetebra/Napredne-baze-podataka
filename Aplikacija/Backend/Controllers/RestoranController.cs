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
        //restoran.DodatakIdList = restoran.JelaIdList = restoran.KategorijeIdList = restoran.KomentariIdList = null;
        collection.InsertOne(restoran);
        return Ok();
    }

    [HttpDelete]
    [Route("DeleteAcc")]
    public ActionResult DeleteAcc(string email)
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var collection = database.GetCollection<LoginRegister>("login_register");

        var query = Query.EQ("Email", email);

        collection.Remove(query);

        return Ok();
    }

    [HttpDelete]
    [Route("DeleteRestoran/{email}")]
    public ActionResult DeleteRestoran(string email)
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var collection = database.GetCollection<Restoran>("restoran");

        var query = Query.EQ("Email", email);
        DeleteAcc(email);
        collection.Remove(query);

        return Ok();
    }

    [HttpPost]
    [Route("AddKategorija")]
    public ActionResult AddKategorija(string naziv)
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");
        Kategorija kat = new Kategorija();
        kat.Naziv = naziv;
        var collection = database.GetCollection<Kategorija>("kategorija");
        collection.Insert(kat);

        return Ok();
    }

    [HttpPut]
    [Route("UpdateRestoran/{naziv}")]
    public ActionResult UpdateRestoran(string naziv)
    {
        
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");
        return Ok();
    }

    [HttpPost]
    [Route("AddMeal")]
    public ActionResult CreateMeal([FromBody] Jela jelo, string naziv)
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var collection = database.GetCollection<Jela>("jela");
        collection.Insert(jelo);
        var query1 = Query.EQ("Naziv", jelo.Naziv);
        var s = collection.Find(query1).First();
        AddKategorija(jelo.Kategorija);

        var collection2 = database.GetCollection<Kategorija>("kategorija");
        var query = Query.EQ("Naziv", jelo.Kategorija);    
        var p = collection2.Find(query).First();

        var collection3 = database.GetCollection<Restoran>("restoran");
        var query2 = Query.EQ("Naziv", naziv);
        var update = Update.PushWrapped("KategorijeIdList", p.Id);
        collection3.Update(query2, update);
        return Ok();
    }

    [HttpGet]
    [Route("GetMeals")]
    public List<Jela> GetMeals()
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");
        var collection = database.GetCollection<Jela>("jela");

        MongoCursor<Jela> jelo = collection.FindAll();
        List<Jela> jela = jelo.ToList();
        return jela;
    }

    [HttpGet]
    [Route("GetMealByName/{name}")]
    public ActionResult GetMealByName(string name)
    {
        Jela jelo = new Jela();
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var collection = database.GetCollection<Jela>("jela");

        var query = Query.EQ("Naziv", name);
        jelo = collection.Find(query).First();

        return Ok(jelo);
    }

    [HttpDelete]
    [Route("DeleteJelo/{naziv}")]
    public ActionResult DeleteJelo(string naziv)
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var collection = database.GetCollection<Jela>("jela");

        var query = Query.EQ("Naziv", naziv);
        collection.Remove(query);

        return Ok();
    }

}