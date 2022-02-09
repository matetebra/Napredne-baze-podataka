using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
[BsonIgnoreExtraElements]
public class SlavkoController : ControllerBase
{
    [HttpPost]
    [Route("BookMarkRestaurant/{naziv}")]
    public IActionResult BookMarkRestaurant(string naziv)
    {
        try
        {


            MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
            MongoServer server = client.GetServer();
            var database = server.GetDatabase("Dostavi");
            var restoranCollection = database.GetCollection<Restoran>("restoran");
            var usersCollection = database.GetCollection<Korisnik>("korisnik");

            var email = HttpContext.User.Identity.Name;

            var user = (from korisnik in usersCollection.AsQueryable<Korisnik>()
                        where korisnik.Email == email
                        select korisnik).FirstOrDefault();



            var getRestaurantQuery = (from restoran in restoranCollection.AsQueryable<Restoran>()
                                      where restoran.Grad == user.Grad
                                      where restoran.Naziv == naziv
                                      select restoran.Id).FirstOrDefault();

            if (getRestaurantQuery.ToString() == "000000000000000000000000")
            {
                return BadRequest("Vec ste bookmarkovali taj restoran");
            }

            user.RestoranKorisnikIdList.Add(new MongoDBRef("restoran", getRestaurantQuery));
            usersCollection.Save(user);
        }
        catch (Exception exc)
        {
            return BadRequest(exc.Message);
        }
        return Ok("Uspesno bookmarkovanje");
    }






}