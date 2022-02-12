namespace Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;

public class Ocena 
{
    public ObjectId Id { get; set; }
    public int Oceni { get; set; }
    public MongoDBRef? OcenaKorisnikId { get; set; }
    public MongoDBRef? OcenaRestoranId { get; set; }
}