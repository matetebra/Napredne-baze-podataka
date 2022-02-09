using MongoDB.Bson;
using MongoDB.Driver;

namespace Backend.Models;

public class Korisnik
{
    public ObjectId Id { get; set; }
    public String? Email { get; set; }
    public String? Ime { get; set; }
    public String? Prezime { get; set; }
    public String? Adresa { get; set; }
    public String? Grad { get; set; }
    public String? Telefon { get; set; }
    public List<MongoDBRef>? RestoranKorisnikIdList { get; set; }
    public List<MongoDBRef>? PorudzbinaKorisnikIdList { get; set; }

    public Korisnik()
    {
        RestoranKorisnikIdList = new List<MongoDBRef>();

        PorudzbinaKorisnikIdList = new List<MongoDBRef>();
    }
}