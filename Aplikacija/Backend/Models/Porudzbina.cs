namespace Backend.Models;
using MongoDB.Bson;

public class Porudzbina
{
    public ObjectId Id { get; set; }
    public String? Napomena { get; set; }
    public String? Adresa { get; set; }
    public Boolean Dostavljena { get; set; }
    public DateTime Datum { get; set; }
    public String? Dostavljac { get; set; }
    public ObjectId KorisnikPorudzbinaIdList { get; set; }
    public ObjectId JeloIdList { get; set; }
}