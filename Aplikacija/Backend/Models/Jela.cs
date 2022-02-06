namespace Backend.Models;
using MongoDB.Bson;

public class Jela 
{
    public ObjectId Id { get; set; }
    public String? Naziv { get; set; }
    public String? Kategorija { get; set; }
    public String? Gramaza { get; set; }
    public String? Opis { get; set; }
    public String? Slika { get; set; }
    public List<string>? NazivNamirnica { get; set; } 

    public Jela()
    {
        NazivNamirnica = new List<string>();
    }
}
