using MongoDB.Bson;
using MongoDB.Driver;

namespace Backend.Models;

public class Komentar
{
    public ObjectId Id { get; set; }
    public String? Tekst { get; set; }
    public String? BrojLajkova { get; set; }
    public MongoDBRef? KorisnikKomentarId { get; set; }
    public MongoDBRef? RestoranKomentarId { get; set; }
}