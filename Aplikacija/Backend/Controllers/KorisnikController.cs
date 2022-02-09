using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class KorisnikController : ControllerBase
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Restoran")]
    [HttpGet]
    
    [Route("GetUsers")]
    public List<Korisnik> GetUsers()
    {
  
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");
        var collection = database.GetCollection<Korisnik>("korisnik");

        MongoCursor<Korisnik> korisnici = collection.FindAll();
        List<Korisnik> users = korisnici.ToList();
        return users;
    }

    [HttpGet]
    [Route("GetUserById/{idUser}")]
    public ActionResult GetUserById(string idUser)
    {
        Korisnik user = new Korisnik();

        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var collection = database.GetCollection<Korisnik>("korisnik");

        var query = Query.EQ("_id", new ObjectId(idUser));
        user = collection.Find(query).First();
        return Ok(user);
    }

    [HttpGet]
    [Route("GetUserByName/{nicknameUser}")]
    public ActionResult GetUserByName(string nicknameUser)
    {
        Korisnik user = new Korisnik();

        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var collection = database.GetCollection<Korisnik>("korisnik");

        var query = Query.EQ("Ime", nicknameUser);
        user = collection.Find(query).First();


        return Ok(user);
    }

    [HttpPost]
    [Route("AddUsers")]
    public ActionResult CreateUser([FromBody] Korisnik korisnik)
    {
        //MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        //var server = MongoServer.Create(client);
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
       // MongoServer server = client.GetServer();
        var database = client.GetDatabase("Dostavi");

        var collection = database.GetCollection<Korisnik>("korisnik");
        collection.InsertOne(korisnik);
        return Ok();
    }

    [HttpDelete]
    [Route("DeleteAccKorisnik")]
    public ActionResult DeleteAccKorisnik(string email)
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
    [Route("DeleteUser/{email}")]
    public ActionResult DeleteUser(string email)
    {
        MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
        MongoServer server = client.GetServer();
        var database = server.GetDatabase("Dostavi");

        var collection = database.GetCollection<Korisnik>("korisnik");
        
        var query = Query.EQ("Email", email);
        DeleteAccKorisnik(email);
        collection.Remove(query);

        return Ok();
    }
}
