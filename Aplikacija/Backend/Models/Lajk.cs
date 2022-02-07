namespace Backend.Models;
using MongoDB.Bson;

public class Lajk
{
    public ObjectId Id { get; set; }
    public ObjectId KorisnikLajkIdList { get; set; }
    public ObjectId KomentarLajkIdList { get; set; }
}