namespace Backend.Models;
using MongoDB.Bson;

public class Komentar
{
    public ObjectId Id { get; set; }
    public String? Tekst { get; set; }
    public String? BrojLajkova { get; set; }
    public ObjectId KorisnikKomentarId { get; set; }
    public ObjectId RestoranKomentarId { get; set; }
}