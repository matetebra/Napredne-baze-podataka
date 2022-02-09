using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Backend.Models;
using Backend.Models.LoginRegisters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
[BsonIgnoreExtraElements]
public class AccountController : ControllerBase
{
    private readonly IConfiguration Configuration;

    public AccountController(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("Login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        if (ModelState.IsValid)
        {
            MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
            MongoServer server = client.GetServer();
            var database = server.GetDatabase("Dostavi");


            LoginRegister account = new LoginRegister();
            var collection = database.GetCollection<LoginRegister>("login_register");
            var query = Query.EQ("Email", model.Email);
            account = collection.Find(query).FirstOrDefault();
            var hash = HashPassword(model.Password!, account.Salt!, 10101, 70);
            if (account == null)
            {
                return Unauthorized();
            }
            if (account.Password_Hash != hash)
            {
                return BadRequest("Password doesn't match");
            }
            var authClaims = new List<Claim>
                            {
                               new Claim(ClaimTypes.Name,model.Email!),
                               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                               new Claim(ClaimTypes.Role, account.Role!)
                            };
            SymmetricSecurityKey authSiginKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:Secret"]));
            JwtSecurityToken token = new JwtSecurityToken(
            issuer: Configuration["JWT:ValidIssuer"],
            audience: Configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSiginKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo.ToString("yyyy-MM-ddThh:mm:ss"),
                role = account.Role
            });
        }
        return Unauthorized();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("registerKorisnik")]
    public object Registration(RegistrationModel model)
    {
        if (ModelState.IsValid)
        {
            MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
            MongoServer server = client.GetServer();
            var database = server.GetDatabase("Dostavi");

            var korisnik = new Korisnik()
            {
                Email = model.Email,
                Ime = model.Ime,
                Prezime = model.Prezime,
                Adresa = model.Adresa,
                Grad = model.Grad,
                Telefon = model.Telefon
            };
            var salt = GenerateSalt(70);
            var hashPass = HashPassword(model.Password!, salt, 10101, 70);
            var register = new LoginRegister()
            {
                Email = model.Email,
                Role = "Korisnik",
                Password_Hash = hashPass,
                Salt = salt
            };

            try
            {
                var collection = database.GetCollection<LoginRegister>("login_register");
                var collection2 = database.GetCollection<Korisnik>("korisnik");
                var query = Query.EQ("Email", model.Email);
                var check = collection.Find(query).FirstOrDefault();

                if (check != null)
                {
                    return BadRequest("Postoji osoba sa tim emailom");
                }
                collection.Insert(register);
                collection2.Insert(korisnik);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        else
        {
            return BadRequest();
        }
    }
    [HttpPost]
    [AllowAnonymous]
    [Route("registerRestoran")]
    public object RestoranRegistration(RestoranRegistrationModel model)
    {
        if (ModelState.IsValid)
        {
            MongoClient client = new MongoClient("mongodb+srv://mongo:sifra123@cluster0.ewwnh.mongodb.net/test");
            MongoServer server = client.GetServer();
            var database = server.GetDatabase("Dostavi");

            var restoran = new Restoran()
            {
                Email = model.Email,
                Naziv = model.Naziv,
                Adresa = model.Adresa,
                Grad = model.Grad,
                Telefon = model.Telefon,
                Opis = model.Opis,
                RadnoVreme = model.RadnoVreme,
                VremeDostave = model.VremeDostave,
                CenaDostave = model.CenaDostave,
                LimitDostave = model.LimitDostave,
                SlobodnaMesta = 0,
                ProsecnaOcena = 0,
                Kapacitet = 0
            };
            var salt = GenerateSalt(70);
            var hashPass = HashPassword(model.Password!, salt, 10101, 70);
            var register = new LoginRegister()
            {
                Email = model.Email,
                Role = "Restoran",
                Password_Hash = hashPass,
                Salt = salt
            };

            try
            {
                var collection = database.GetCollection<LoginRegister>("login_register");
                var collection2 = database.GetCollection<Restoran>("restoran");
                var query = Query.EQ("Email", model.Email);
                var check = collection.Find(query).FirstOrDefault();

                if (check != null)
                {
                    return BadRequest("Postoji osoba sa tim emailom");
                }
                collection.Insert(register);
                collection2.Insert(restoran);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        else
        {
            return BadRequest();
        }
    }
    public static string GenerateSalt(int nSalt)
    {
        var saltBytes = new byte[nSalt];

        using (var provider = new RNGCryptoServiceProvider())
        {
            provider.GetNonZeroBytes(saltBytes);
        }

        return Convert.ToBase64String(saltBytes);
    }

    public static string HashPassword(string password, string salt, int nIterations, int nHash)
    {
        var saltBytes = Convert.FromBase64String(salt);

        using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, nIterations))
        {
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(nHash));
        }
    }


}




