using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Driver.Linq;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class RestoranController : ControllerBase
{
    [HttpGet]
    [Route("GetRestaurants")]
    public IActionResult GetRestaurants()
    {

        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");
        var collection = database.GetCollection<Restoran>("restoran");
        var kolekcijaJela = database.GetCollection<Jela>("jela");

        List<Jela> svaJela = new List<Jela>();

        MongoCursor<Restoran> restorani = collection.FindAll();
        foreach (Restoran r in restorani)
        {
            foreach (ObjectId jeloID in r.JelaIdList)
            {
                var query12 = (from jela in kolekcijaJela.AsQueryable<Jela>()
                               where jela.Id == jeloID
                               select jela).FirstOrDefault();
                svaJela.Add(query12!);
            }
        }
        return Ok(new
        {
            Naziv = "Cezar",
            Jela = svaJela
        });


        //return Ok(svaJela);
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
        var query = Query.EQ("Naziv", naziv);
        var check = collection.Find(query).FirstOrDefault();
        if (check == null)
        {
            collection.Insert(kat);
        }

        return Ok();
    }

    [HttpPost]
    [Authorize(Roles = "Restoran")]
    [Route("AddMeal")]
    public ActionResult CreateMeal([FromBody] Jela jelo)
    {

        string restoran = HttpContext.User.Identity!.Name;

        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var collection = database.GetCollection<Jela>("jela");
        var query1 = Query.EQ("Naziv", jelo.Naziv);
        var s = collection.Find(query1).FirstOrDefault();
        if (s == null)
        {
            collection.Insert(jelo);
            s = collection.Find(query1).FirstOrDefault();
        }

        AddKategorija(jelo.Kategorija); // dodajemo kategoriju u kolekciji ako vec ne postoji

        var collection2 = database.GetCollection<Kategorija>("kategorija");
        var query = Query.EQ("Naziv", jelo.Kategorija);
        var p = collection2.Find(query).FirstOrDefault();

        var collection3 = database.GetCollection<Restoran>("restoran");
        var query2 = Query.EQ("Email", restoran);
        var provera = collection3.Find(Query.EQ("KategorijeIdList", p.Id)); // moramo da proverimo da se vec ta kategorija ne nalazi u listi da se ne bi dupliralo
        var provera2 = collection3.Find(Query.EQ("JelaIdList", s.Id));
        bool flag = false;
        if (provera.Count() == 0)
        {
            var update3 = Update.PushWrapped("KategorijeIdList", p.Id);
            collection3.Update(query2, update3);
            flag = true;
        }
        if (provera2.Count() == 0)
        {
            var update2 = Update.PushWrapped("JelaIdList", s.Id); // upisujemo id jela u listu kod restorana     
            collection3.Update(query2, update2); // azuriramo
            flag = true;
        }
        if (flag == false)
        {
            return BadRequest("Ne mozete dodati isto jelo/kategoriju");
        }

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