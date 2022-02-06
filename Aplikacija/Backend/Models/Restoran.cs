namespace Backend.Models;
using MongoDB.Bson;

public class Restoran 
{
    public ObjectId Id { get; set; }
    public String? Email { get; set; }
    public String? Naziv { get; set; }
    public String? Adresa { get; set; }
    public String? Grad { get; set; }
    public String? Telefon { get; set; }
    public String? Opis { get; set; }
    public String? RadnoVreme { get; set; }
    public float ProsecnaOcena { get; set; }
    public String? VremeDostave { get; set; }
    public String? CenaDostave { get; set; }
    public String? LimitDostave { get; set; }
    public String? Kapacitet { get; set; }
    public String? SlobodnaMesta { get; set; }
    public List<ObjectId>? KategorijeIdList { get; set; }
    public List<ObjectId>? KomentariIdList { get; set; }
    public List<ObjectId>? JelaIdList { get; set; }
    public List<ObjectId>? DodatakIdList { get; set; }

    public Restoran()
    {
        KategorijeIdList = new List<ObjectId>();

        KomentariIdList = new List<ObjectId>();

        JelaIdList = new List<ObjectId>();
        
        DodatakIdList = new List<ObjectId>();
    }
}