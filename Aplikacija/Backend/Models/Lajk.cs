namespace Backend.Models;
using MongoDB.Bson;

public class Lajk
{
    public ObjectId Id { get; set; }
    public List<ObjectId>? KorisnikLajkIdList { get; set; }
    public List<ObjectId>? KomentarLajkIdList { get; set; }

    public Lajk()
    {
        KorisnikLajkIdList = new List<ObjectId>();

        KomentarLajkIdList = new List<ObjectId>();
    }
}