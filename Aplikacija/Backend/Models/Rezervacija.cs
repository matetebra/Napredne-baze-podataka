namespace Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;

public class Rezervacija
{
    public ObjectId Id { get; set; }
    public String? BrojMesta { get; set; }
    public String? Vreme { get; set; }
    public DateTime Datum { get; set; }
    public MongoDBRef? KorisnikRezervacijaId { get; set; }
    public MongoDBRef? RestoranRezervacijaId { get; set; }
}