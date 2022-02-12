using MongoDB.Bson;
using MongoDB.Driver;

namespace Backend.Models;

public class Komentar
{
    public ObjectId Id { get; set; }
    public String? Tekst { get; set; }
    public ObjectId? KorisnikKomentarId { get; set; }
    public ObjectId? RestoranKomentarId { get; set; }
}