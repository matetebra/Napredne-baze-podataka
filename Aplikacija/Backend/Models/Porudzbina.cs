namespace Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
public class Porudzbina
{
    public ObjectId Id { get; set; }
    public String? Napomena { get; set; }
    public Boolean Dostavljena { get; set; }
    public DateTime Datum { get; set; }
    public MongoDBRef? KorisnikPorudzbinaId { get; set; }
    public List<MongoDBRef>? JeloIdList { get; set; }
    public MongoDBRef? Restoran { get; set; }
    public List<MongoDBRef>? Dodaci { get; set; }
    public int UkupnaCena { get; set; }
}