namespace Backend.Models;
using MongoDB.Bson;

public class Kategorija
{
    public ObjectId Id { get; set; }
    public String? Naziv { get; set; }
}