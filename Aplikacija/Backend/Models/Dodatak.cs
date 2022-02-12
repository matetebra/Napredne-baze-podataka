namespace Backend.Models;
using MongoDB.Bson;
public class Dodatak
{
    public ObjectId Id { get; set; }
    public String? Naziv { get; set; }
    public int Cena { get; set; }
}