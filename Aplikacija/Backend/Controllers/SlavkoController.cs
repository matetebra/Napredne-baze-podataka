using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization.Attributes;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
[BsonIgnoreExtraElements]
public class SlavkoController : ControllerBase
{
    // [HttpPost]
    // [Route("BookMarkRestaurant/{email}")]
    // public IActionResult BookMarkRestaurant(string email)
    // {
    //     try
    //     {
    //         MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
    //         MongoServer server = client.GetServer();
    //         var database = server.GetDatabase("Dostavi");
    //         var restoranCollection = database.GetCollection<Restoran>("restoran");
    //         var usersCollection = database.GetCollection<Korisnik>("korisnik");

    //         var userEmail = HttpContext.User.Identity.Name;

    //         var user = (from korisnik in usersCollection.AsQueryable<Korisnik>()
    //                     where korisnik.Email == userEmail
    //                     select korisnik).FirstOrDefault();



    //         var getRestaurantQuery = (from restoran in restoranCollection.AsQueryable<Restoran>()
    //                                   where restoran.Email == email
    //                                   select restoran.Id).FirstOrDefault();

    //         if (getRestaurantQuery.ToString() == "000000000000000000000000")
    //         {
    //             return BadRequest("Vec ste bookmarkovali taj restoran");
    //         }

    //         user.RestoranKorisnikIdList.Add(new MongoDBRef("restoran", getRestaurantQuery));
    //         usersCollection.Save(user);
    //     }
    //     catch (Exception exc)
    //     {
    //         return BadRequest(exc.Message);
    //     }
    //     return Ok("Uspesno bookmarkovanje");
    // }

    [HttpGet]
    [Route("GetAllRestourantInformations/{email}")]
    public IActionResult getAllRestourantInformations(string email)
    {
        try
        {
            MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
            MongoServer server = client.GetServer();
            var database = server.GetDatabase("Dostavi");

            var restaurantsCollection = database.GetCollection<Restoran>("restoran");
            var userCollection = database.GetCollection<Korisnik>("korisnik");

            int localtime = DateTime.Now.Hour;

            var exactRestoran = (from restoran in restaurantsCollection.AsQueryable<Restoran>()
                                 where restoran.Email == email
                                 select restoran).FirstOrDefault();
            if (exactRestoran.pocetakRV > localtime && exactRestoran.krajRV < localtime)
            {
                return BadRequest("Restoran trenutno ne radi!");
            }

            List<Jela> svaJela = new List<Jela>();
            foreach (MongoDBRef jela in exactRestoran.JelaIdList)
            {
                svaJela.Add(database.FetchDBRefAs<Jela>(jela));
            }
            List<object> jelaToReturn = new List<object>();
            foreach (Jela j in svaJela)
            {
                jelaToReturn.Add(new
                {
                    Id = j.Id.ToString(),
                    Naziv = j.Naziv,
                    Kategorija = j.Kategorija,
                    Gramaza = j.Gramaza,
                    Cena = j.Cena,
                    Opis = j.Opis,
                    Slika = j.Slika,
                    NazivNamirnica = j.NazivNamirnica
                });
            }

            List<Kategorija> sveKategorije = new List<Kategorija>();
            foreach (MongoDBRef k in exactRestoran.KategorijeIdList)
            {
                sveKategorije.Add(database.FetchDBRefAs<Kategorija>(k));
            }

            List<Komentar> sviKomentari = new List<Komentar>();
            foreach (MongoDBRef k in exactRestoran.KomentariIdList)
            {
                sviKomentari.Add(database.FetchDBRefAs<Komentar>(k));
            }
            List<object> komentariToReturn = new List<object>();
            foreach (Komentar k in sviKomentari)
            {
                var emailKorisnikaKomentara = (from korisnik in userCollection.AsQueryable()
                                               where korisnik.Id == k.KorisnikKomentarId
                                               select korisnik.Email).FirstOrDefault();
                komentariToReturn.Add(new
                {
                    Tekst = k.Tekst,
                    Email = emailKorisnikaKomentara

                });
            }

            List<Dodatak> sviDodaci = new List<Dodatak>();
            foreach (MongoDBRef k in exactRestoran.DodatakIdList)
            {
                sviDodaci.Add(database.FetchDBRefAs<Dodatak>(k));
            }
            List<object> dodaciToReturn = new List<object>();
            foreach (Dodatak d in sviDodaci)
            {
                jelaToReturn.Add(new
                {
                    Id = d.Id.ToString(),
                    Naziv = d.Naziv,
                    Cena = d.Cena
                });
            }
            return Ok(new
            {
                Id = exactRestoran.Id.ToString(),
                Email = exactRestoran.Email,
                Naziv = exactRestoran.Naziv,
                Adresa = exactRestoran.Adresa,
                Grad = exactRestoran.Grad,
                Telefon = exactRestoran.Telefon,
                Opis = exactRestoran.Opis,
                PocetakRV = exactRestoran.pocetakRV,
                KrajRv = exactRestoran.krajRV,
                ProsecnaOcena = exactRestoran.ProsecnaOcena,
                VremeDostave = exactRestoran.VremeDostave,
                CenaDostave = exactRestoran.CenaDostave,
                LimitDostave = exactRestoran.LimitDostave,
                Kapacitet = exactRestoran.Kapacitet,
                SlobodnaMesta = exactRestoran.SlobodnaMesta,
                Kategorije = sveKategorije,
                Komentari = komentariToReturn,
                Jela = jelaToReturn,
                Dodaci = dodaciToReturn
            });
        }
        catch (Exception exc)
        {
            return BadRequest(exc.Message);
        }
    }
    [HttpGet]
    [Route("GetRestourants")]
    public IActionResult getRestourants()// Vraca naziv adresu i telefon prvih 6  restorana, ukoliko je registrovan onda samo restorane za taj grad
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
                                 where restoran.odobren == true
                                 select new { restoran.Email, restoran.Naziv, restoran.Adresa, restoran.Telefon }).ToList();
            return Ok(allRestaunats);
        }
    }
    [HttpGet]
    [Route("GetRestourantsNeodobreni")]
    public IActionResult getRestourantsNeodobreni()
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var restaurantsCollection = database.GetCollection<Restoran>("restoran");

        var allRestaunats = (from restoran in restaurantsCollection.AsQueryable<Restoran>()
                             where restoran.odobren == false
                             select new { restoran.Email, restoran.Naziv, restoran.Adresa, restoran.Telefon }).ToList();
        return Ok(allRestaunats);
    }
    [HttpGet]
    [Route("GetBookmarked")]
    public IActionResult getBookmarked()
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var email = HttpContext.User.Identity.Name;

        var usersCollection = database.GetCollection<Korisnik>("korisnik");

        var bookMarked = (from korisnik in usersCollection.AsQueryable<Korisnik>()
                          where korisnik.Email == email
                          select korisnik.RestoranKorisnikIdList).FirstOrDefault();

        List<Restoran> sviRestorani = new List<Restoran>();
        foreach (MongoDBRef r in bookMarked)
        {
            sviRestorani.Add(database.FetchDBRefAs<Restoran>(r));
        }
        return Ok(sviRestorani);
    }
    [HttpGet]
    [Route("PrethodnePorudzbine")]
    public IActionResult prethodnePorudzbine()//MOZDA NE RADI NE MOGU SAD TRENUTNO DA PROVERIM
                                              //DODAJ NEKOME IZ NISA NESTO IZ CEZAR NIS I TESTIRAJ
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var email = HttpContext.User.Identity.Name;

        var usersCollection = database.GetCollection<Korisnik>("korisnik");

        var porudzbine = (from korisnik in usersCollection.AsQueryable<Korisnik>()
                          where korisnik.Email == email
                          select korisnik.PorudzbinaKorisnikIdList).FirstOrDefault();

        List<Porudzbina> porudzbinas = new List<Porudzbina>();
        foreach (MongoDBRef r in porudzbine)
        {
            porudzbinas.Add(database.FetchDBRefAs<Porudzbina>(r));
        }
        List<object> toReturn = new List<object>();

        List<Dodatak> dodaci = new List<Dodatak>();
        List<Jela> jelo = new List<Jela>();

        foreach (Porudzbina p in porudzbinas)
        {
            dodaci.Clear();
            jelo.Clear();
            foreach (MongoDBRef r in p.JeloIdList)
            {
                jelo.Add(database.FetchDBRefAs<Jela>(r));
            }
            foreach (MongoDBRef r in p.Dodaci)
            {
                dodaci.Add(database.FetchDBRefAs<Dodatak>(r));
            }
            toReturn.Add(new
            {
                Jela = jelo,
                Dodaci = dodaci,
                Naziv = database.FetchDBRefAs<Restoran>(p.Restoran).Naziv,
                Cena = p.UkupnaCena
            });
        }
        return Ok(toReturn);
    }
    [HttpPost]
    [Route("dodajNarudzbinu")]
    public IActionResult dodajNarudzbinu([FromBody] PorudzbinaDTO porudzba)
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var email = HttpContext.User.Identity.Name;

        var usersCollection = database.GetCollection<Korisnik>("korisnik");
        var jelaCollection = database.GetCollection<Jela>("jela");
        var dodaciCollection = database.GetCollection<Dodatak>("dodatak");
        var restoranCollection = database.GetCollection<Restoran>("restoran");
        var porudzbinaCollection = database.GetCollection<Porudzbina>("porudzbina");

        var restoran1 = (from restoran in restoranCollection.AsQueryable<Restoran>()
                         where restoran.Email == porudzba.EmailRestoran
                         select restoran).FirstOrDefault();

        var user = (from korisnik in usersCollection.AsQueryable<Korisnik>()
                    where korisnik.Email == email
                    select korisnik).FirstOrDefault();

        List<Jela> narucenaJela = new List<Jela>();
        foreach (String s in porudzba.JelaID)
        {
            narucenaJela.Add((from jela in jelaCollection.AsQueryable<Jela>()
                              where jela.Id == MongoDB.Bson.ObjectId.Parse(s)
                              select jela).FirstOrDefault()!);
        }
        List<Dodatak> dodaci = new List<Dodatak>();
        if (porudzba.DodaciID != null)
        {
            foreach (String s in porudzba.DodaciID)
            {
                dodaci.Add((from dodatak in jelaCollection.AsQueryable<Dodatak>()
                            where dodatak.Id == MongoDB.Bson.ObjectId.Parse(s)
                            select dodatak).FirstOrDefault()!);
            }
        }
        Porudzbina p = new Porudzbina();

        p.Datum = DateTime.Now.Date;
        p.Napomena = porudzba.Napomena;
        p.Dostavljena = false;
        p.KorisnikPorudzbinaId = new MongoDBRef("korisnik", user.Id);
        p.Restoran = new MongoDBRef("restoran", restoran1.Id);

        int cena = 0;
        foreach (Jela j in narucenaJela)
        {
            cena += j.Cena;
            p.JeloIdList.Add(new MongoDBRef("jela", j.Id));


        }
        if (porudzba.DodaciID! != null)
        {
            foreach (Dodatak j in dodaci)
            {
                cena += j.Cena;
                p.JeloIdList.Add(new MongoDBRef("dodatak", j.Id));
            }
        }
        p.UkupnaCena = cena;


        porudzbinaCollection.Insert(p);

        var upit = Query.EQ("_id", user.Id);
        var update = Update.PushWrapped("PorudzbinaKorisnikIdList", new MongoDBRef("porudzbina", p.Id));
        usersCollection.Update(upit, update);


        // user.PorudzbinaKorisnikIdList.Add(new MongoDBRef("porudzbina", p.Id));
        // usersCollection.Save(user);
        return Ok();
    }
    [HttpGet]
    [Authorize(Roles = "Restoran")]
    [Route("VratiRezervacije")]
    public IActionResult returnReservation()
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");
        var email = HttpContext.User.Identity.Name;

        var restoranCollection = database.GetCollection<Restoran>("restoran");
        var rezervacijaCollection = database.GetCollection<Rezervacija>("rezervacija");
        var korisnikCollection = database.GetCollection<Korisnik>("korisnik");

        var restourant = (from restoran in restoranCollection.AsQueryable<Restoran>()
                          where restoran.Email == email
                          select restoran).FirstOrDefault();

        var todayDate = DateTime.UtcNow.Date;

        var rez = (from rezervacija in rezervacijaCollection.AsQueryable<Rezervacija>()
                   where rezervacija.RestoranRezervacijaId == restourant.Id
                   where rezervacija.Datum == todayDate
                   select rezervacija).ToList();

        List<object> toReturn = new List<object>();
        foreach (Rezervacija r in rez)
        {
            var emailTelefonKorisnika = (from korisnik in korisnikCollection.AsQueryable<Korisnik>()
                                         where korisnik.Id == r.KorisnikRezervacijaId
                                         select new { korisnik.Email, korisnik.Telefon }).FirstOrDefault();
            toReturn.Add(new
            {
                IDRezervacije = r.Id.ToString(),
                EmailKorisnika = emailTelefonKorisnika.Email,
                TelefonKorisnika = emailTelefonKorisnika.Telefon,
                Vreme = r.Vreme,
                BrojMesta = r.BrojMesta,
            });
        }
        return Ok(toReturn);
    }
    [HttpDelete]
    // [Authorize(Roles = "Admin")]
    [Route("obrisiRezervaciju/{id}")]
    public IActionResult deleteReservation(string id)
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var rezervacijaCollection = database.GetCollection<Rezervacija>("rezervacija");

        var deleteFilter = Query.EQ("_id", MongoDB.Bson.ObjectId.Parse(id));
        rezervacijaCollection.Remove(deleteFilter);

        return Ok();
    }
    [HttpGet]
    [Route("getUserInformaiton")]
    public IActionResult getUserInformation()
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var korisnikCollection = database.GetCollection<Korisnik>("korisnik");

        var toReturn = (from korisnik in korisnikCollection.AsQueryable<Korisnik>()
                        where korisnik.Email == HttpContext.User.Identity.Name
                        select new
                        {
                            Ime = korisnik.Ime,
                            Prezime = korisnik.Prezime,
                            Telefon = korisnik.Telefon,
                            Email = korisnik.Email
                        }).FirstOrDefault();
        return Ok(toReturn);
    }
    [HttpPost]
    [Authorize(Roles = "Korisnik")]
    [Route("AddComment/{rEmail}")]
    public IActionResult addCommment([FromBody] KomentarDTO kom, string rEmail)
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var komentarCollection = database.GetCollection<Komentar>("komentar");
        var userCollection = database.GetCollection<Korisnik>("korisnik");
        var restourantCollection = database.GetCollection<Restoran>("restoran");


        var korisnikID = (from korisnik in userCollection.AsQueryable<Korisnik>()
                          where korisnik.Email == HttpContext.User.Identity.Name
                          select korisnik.Id).FirstOrDefault();
        var restourant = (from restoran in restourantCollection.AsQueryable<Restoran>()
                          where restoran.Email == rEmail
                          select restoran).FirstOrDefault();

        Komentar komentar = new Komentar();

        komentar.Tekst = kom.Tekst;
        komentar.KorisnikKomentarId = korisnikID;
        komentar.RestoranKomentarId = restourant.Id;


        komentarCollection.Insert(komentar);
        return Ok();
    }



}