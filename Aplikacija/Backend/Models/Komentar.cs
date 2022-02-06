namespace Backend.Models;
using MongoDB.Bson;

public class Komentar
{
    public ObjectId Id { get; set; }
    public String? Tekst { get; set; }
    public String? BrojLajkova { get; set; }
    public List<ObjectId>? KorisnikKomentarIdList { get; set; }
    public List<ObjectId>? RestoranKomentarIdList { get; set; }

    public Komentar()
    {
        KorisnikKomentarIdList = new List<ObjectId>();

        RestoranKomentarIdList = new List<ObjectId>();
    }
}