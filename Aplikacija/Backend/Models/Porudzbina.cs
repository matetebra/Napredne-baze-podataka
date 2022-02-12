namespace Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;
public class Porudzbina
{
    public ObjectId Id { get; set; }
    public String? Napomena { get; set; }
    public Boolean Dostavljena { get; set; }
    public DateTime Datum { get; set; }
    public ObjectId? KorisnikPorudzbinaId { get; set; }
    public List<ObjectId>? JeloIdList { get; set; }
    public ObjectId? Restoran { get; set; }
    public List<ObjectId>? Dodaci { get; set; }
    public int UkupnaCena { get; set; }
    public Porudzbina()
    {
        JeloIdList = new List<ObjectId>();

        Dodaci = new List<ObjectId>();
    }
}