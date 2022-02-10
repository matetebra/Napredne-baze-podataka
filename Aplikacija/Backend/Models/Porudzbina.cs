namespace Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
public class Porudzbina
{
    public ObjectId Id { get; set; }
    public String? Napomena { get; set; }
    public String? Adresa { get; set; }
    public Boolean Dostavljena { get; set; }
    public DateTime Datum { get; set; }
    public String? Dostavljac { get; set; }
    public MongoDBRef? KorisnikPorudzbinaId { get; set; }
    public MongoDBRef? JeloIdList { get; set; }
    public List<string>? Dodaci { get; set; }
    public int UkupnaCena { get; set; }
}