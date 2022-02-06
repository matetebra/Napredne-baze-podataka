namespace Backend.Models;
using MongoDB.Bson;

public class Rezervacija
{
    public ObjectId Id { get; set; }
    public String? BrojMesta { get; set; }
    public String? Vreme { get; set; }
    public DateTime Datum { get; set; }
    public List<ObjectId>? KorisnikRezervacijaIdList { get; set; }
    public List<ObjectId>? RestoranRezervacijaIdList { get; set; }

    public Rezervacija()
    {
        KorisnikRezervacijaIdList = new List<ObjectId>();

        RestoranRezervacijaIdList = new List<ObjectId>();
    }
}