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
    /*[HttpGet]
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
      /*      foreach (ObjectId jeloID in r.JelaIdList)
            {
                var query12 = (from jela in kolekcijaJela.AsQueryable<Jela>()
                               where jela.Id == jeloID
                               select jela).FirstOrDefault();
                svaJela.Add(query12!);
            }
        }}
        return Ok(new
        {
            Naziv = "Cezar",
            Jela = svaJela
        });


        //return Ok(svaJela);
    }*/

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
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
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

        string email = HttpContext.User.Identity!.Name;

        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var jeloCollection = database.GetCollection<Jela>("jela");
        var collection2 = database.GetCollection<Kategorija>("kategorija");
        var restoranCollection = database.GetCollection<Restoran>("restoran");


        var query1 = Query.EQ("Naziv", jelo.Naziv);
        var s = jeloCollection.Find(query1).FirstOrDefault();
        if (s == null)
        {
            jeloCollection.Insert(jelo);
            s = jeloCollection.Find(query1).FirstOrDefault();
        }

        AddKategorija(jelo.Kategorija); // dodajemo kategoriju u kolekciji ako vec ne postoji


        var query = Query.EQ("Naziv", jelo.Kategorija);
        var p = collection2.Find(query).FirstOrDefault();
        var rest = (from restoran in restoranCollection.AsQueryable<Restoran>()
                    where restoran.Email == email
                    select restoran).FirstOrDefault();
        int brojac1 = 0;
        int brojac2 = 0;
        bool flag = false;
        foreach (MongoDBRef mdb in rest.KategorijeIdList)
        {
            if (mdb.Id == p.Id)
            {
                brojac1++;
            }
        }
        foreach (MongoDBRef mdb in rest.JelaIdList)
        {
            if (mdb.Id == s.Id)
            {
                brojac2++;
            }
        }
        if (brojac1 == 0)
        {
            var upit = Query.EQ("_id", rest.Id);
            var update = Update.PushWrapped("KategorijeIdList", new MongoDBRef("kategorija", p.Id));
            restoranCollection.Update(upit, update);
            flag = true;
        }
        if (brojac2 == 0)
        {
            var upit = Query.EQ("_id", rest.Id);
            var update = Update.PushWrapped("JelaIdList", new MongoDBRef("jela", s.Id));
            restoranCollection.Update(upit, update);
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

    [HttpPut]
    [Route("OdobriRestoran/{email}")]
    public ActionResult OdobriRestoran(string email)
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var restoranCollection = database.GetCollection<Restoran>("restoran");
        var rest = (from restoran in restoranCollection.AsQueryable<Restoran>()
                    where restoran.Email == email
                    select restoran).FirstOrDefault();

        rest.odobren = true;
        restoranCollection.Save(rest);
        return Ok();
    }

}