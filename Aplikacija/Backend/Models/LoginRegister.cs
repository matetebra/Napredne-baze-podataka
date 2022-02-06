namespace Backend.Models;
using MongoDB.Bson;
public class LoginRegister
{
    public ObjectId Id { get; set; }
    public String? Email { get; set; }
    public String? Password_Hash { get; set; }
    public String? Role { get; set; }
    public String? Salt { get; set; }
}