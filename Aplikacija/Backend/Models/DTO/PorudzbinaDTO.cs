namespace Backend.Models.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
public class PorudzbinaDTO
{
    public String? Napomena { get; set; }
    public List<String>? JelaID { get; set; }
    public String? EmailRestoran { get; set; }
    public List<String>? DodaciID { get; set; }
}