namespace Backend.Models.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
public class RezervacijaDTO
{
    public String? BrojMesta { get; set; }
    public String? Vreme { get; set; }
    public DateTime Datum { get; set; }
}