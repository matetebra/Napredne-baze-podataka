namespace Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class Rezervacija
{
    public ObjectId Id { get; set; }
    public String? BrojMesta { get; set; }
    public String? Vreme { get; set; }
    public DateTime Datum { get; set; }
    public ObjectId? KorisnikRezervacijaId { get; set; }
    public ObjectId? RestoranRezervacijaId { get; set; }
    public Rezervacija()
    {
    }
}