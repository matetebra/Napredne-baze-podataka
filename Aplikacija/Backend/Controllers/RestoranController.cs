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
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var restaurantsCollection = database.GetCollection<Restoran>("restoran");

        int localtime = DateTime.Now.Hour;

        if (HttpContext.User.Identity.Name == null)
        {
            var allRestaunats = (from restoran in restaurantsCollection.FindAll().SetLimit(12).AsQueryable<Restoran>()
                                 where restoran.pocetakRV < localtime
                                 where restoran.krajRV > localtime
                                 where restoran.Naziv.ToLower().Contains(name.ToLower())
                                 where restoran.odobren == true
                                 select new { restoran.Email, restoran.Naziv, restoran.Adresa, restoran.Telefon }).ToList();
            return Ok(allRestaunats);
        }
        else
        {
            var usersCollection = database.GetCollection<Korisnik>("korisnik");

            var userGrad = (from korisnik in usersCollection.AsQueryable<Korisnik>()
                            where korisnik.Email == HttpContext.User.Identity.Name
                            select korisnik.Grad).FirstOrDefault();
            var allRestaunats = (from restoran in restaurantsCollection.AsQueryable<Restoran>()
                                 where restoran.Grad == userGrad
                                 where restoran.pocetakRV < localtime
                                 where restoran.krajRV > localtime
                                 where restoran.Naziv.ToLower().Contains(name.ToLower())
                                 where restoran.odobren == true
                                 select new { restoran.Email, restoran.Naziv, restoran.Adresa, restoran.Telefon }).ToList();
            return Ok(allRestaunats);
        }
    }

    [HttpGet]
    [Route("GetRestaurantByCategory/{category}")]
    public ActionResult GetRestaurantByCategory(string category)
    {
        try 
        {
            MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
            MongoServer server = client.GetServer();
            var database = server.GetDatabase("Dostavi");

            var collection = database.GetCollection<Restoran>("restoran");
            var collectionKorisnik = database.GetCollection<Korisnik>("korisnik");
            var collectionKategorija = database.GetCollection<Kategorija>("kategorija");
            var user = HttpContext.User.Identity.Name;

            var kor = (from korisnik in collectionKorisnik.AsQueryable<Korisnik>()
                        where korisnik.Email == user
                        select korisnik).FirstOrDefault();

            var kat = (from kategorija in collectionKategorija.AsQueryable<Kategorija>()
                        where kategorija.Naziv == category
                        select kategorija).FirstOrDefault();

            var check = (from restoran in collection.AsQueryable<Restoran>()
                        where restoran.Grad == kor.Grad
                        select restoran).ToList();
            
            List<Object> rn = new List<Object>();

            foreach(var r in check)
            {
                foreach (MongoDBRef mdbr in r.KategorijeIdList)
                {
                    if (mdbr.Id == kat.Id)
                    {
                        var upis = (from restoran in collection.AsQueryable<Restoran>()
                        where restoran.Grad == kor.Grad
                        where restoran.Id == r.Id
                        select new { restoran.Email, restoran.Naziv, restoran.Adresa, restoran.Telefon, restoran.ProsecnaOcena}).FirstOrDefault();
                        rn.Add(upis!);
                    }
                }
            }
            if (rn.Count() == 0)
            {
                return BadRequest("Nema rezultata pretrage");
            }

            return Ok(rn);
        }

        catch (Exception)
        {
            return BadRequest("Nema rezultata pretrage");
        }
    }

    [HttpPost]
    [Route("AddRestaurants")]
    public ActionResult CreateRestaurants([FromBody] Restoran restoran)
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        var database = client.GetDatabase("Dostavi");

        var collection = database.GetCollection<Restoran>("restoran");
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

    [Authorize(Roles = "Admin")]
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

    [HttpPost]
    [Route("OceniRestoran/{email}/{ocenica}")]
    public ActionResult OceniRestoran(string email, int ocenica)
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var restoranCollection = database.GetCollection<Restoran>("restoran");
        var korisnikCollection = database.GetCollection<Korisnik>("korisnik");
        var ocenaCollection = database.GetCollection<Ocena>("ocena");

        var userEmail = HttpContext.User.Identity.Name;

        var rest = (from restoran in restoranCollection.AsQueryable<Restoran>()
                    where restoran.Email == email
                    select restoran).FirstOrDefault();

        var kor = (from korisnik in korisnikCollection.AsQueryable<Korisnik>()
                   where korisnik.Email == userEmail
                   select korisnik).FirstOrDefault();

        var check = (from ocena in ocenaCollection.AsQueryable<Ocena>()
                     where ocena.OcenaRestoranId.Id == rest.Id
                     where ocena.OcenaKorisnikId.Id == kor.Id
                     select ocena).FirstOrDefault();


        if (check != null)
        {
            return BadRequest("Vec ste ocenili!");
        }

        Ocena oc = new Ocena();
        oc.Oceni = ocenica;
        oc.OcenaRestoranId = new MongoDBRef("restoran", rest.Id);
        oc.OcenaKorisnikId = new MongoDBRef("korisnik", kor.Id);
        ocenaCollection.Save(oc);

        var ocene = (from ocena in ocenaCollection.AsQueryable<Ocena>()
                     where ocena.OcenaRestoranId.Id == rest.Id
                     select ocena).ToList();

        float novaProsecnaOcena = (rest.ProsecnaOcena * (ocene.Count() - 1) + ocenica) / ocene.Count();
        rest.ProsecnaOcena = (float)Math.Round(novaProsecnaOcena * 100f) / 100f;
        restoranCollection.Save(rest);
        return Ok("Uspesno ste ocenili");
    }

    [HttpGet]
    [Route("SoryByLowest")]
    public ActionResult SoryByLowest()
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var restoranCollection = database.GetCollection<Restoran>("restoran");
        var korisnikCollection = database.GetCollection<Korisnik>("korisnik");

        var userEmail = HttpContext.User.Identity.Name;

        var kor = (from korisnik in korisnikCollection.AsQueryable<Korisnik>()
                   where korisnik.Email == userEmail
                   select korisnik).FirstOrDefault();

        var rest = (from restoran in restoranCollection.AsQueryable<Restoran>()
                    where restoran.Grad == kor.Grad
                    orderby restoran.ProsecnaOcena descending
                    select new { restoran.Email, restoran.Naziv, restoran.Adresa, restoran.Telefon, restoran.ProsecnaOcena }).ToList();
        rest.Reverse();
        return Ok(rest);
    }

    [HttpGet]
    [Route("SoryByGreatest")]
    public ActionResult SoryByGreatest()
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var restoranCollection = database.GetCollection<Restoran>("restoran");
        var korisnikCollection = database.GetCollection<Korisnik>("korisnik");

        var userEmail = HttpContext.User.Identity.Name;

        var kor = (from korisnik in korisnikCollection.AsQueryable<Korisnik>()
                   where korisnik.Email == userEmail
                   select korisnik).FirstOrDefault();

        var rest = (from restoran in restoranCollection.AsQueryable<Restoran>()
                    where restoran.Grad == kor.Grad
                    orderby restoran.ProsecnaOcena descending
                    select new { restoran.Email, restoran.Naziv, restoran.Adresa, restoran.Telefon, restoran.ProsecnaOcena }).ToList();

        return Ok(rest);
    }


}