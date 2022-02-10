namespace Backend.Models;
using MongoDB.Bson;
using MongoDB.Driver;

public class Restoran
{
    public ObjectId Id { get; set; }
    public String? Email { get; set; }
    public String? Naziv { get; set; }
    public String? Adresa { get; set; }
    public String? Grad { get; set; }
    public String? Telefon { get; set; }
    public String? Opis { get; set; }
    public int? pocetakRV { get; set; }
    public int? krajRV { get; set; }
    public float ProsecnaOcena { get; set; }
    public String? VremeDostave { get; set; }
    public int CenaDostave { get; set; }
    public int LimitDostave { get; set; }
    public int Kapacitet { get; set; }
    public int SlobodnaMesta { get; set; }
    public List<MongoDBRef>? KategorijeIdList { get; set; }
    public List<MongoDBRef>? KomentariIdList { get; set; }
    public List<MongoDBRef>? JelaIdList { get; set; }
    public List<MongoDBRef>? DodatakIdList { get; set; }

    public Restoran()
    {
        KategorijeIdList = new List<MongoDBRef>();

        KomentariIdList = new List<MongoDBRef>();

        JelaIdList = new List<MongoDBRef>();

        DodatakIdList = new List<MongoDBRef>();
    }
}