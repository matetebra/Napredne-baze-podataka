namespace Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;

public class Lajk
{
    public ObjectId Id { get; set; }
    public MongoDBRef? KorisnikLajkIdList { get; set; }
    public MongoDBRef? KomentarLajkIdList { get; set; }
}