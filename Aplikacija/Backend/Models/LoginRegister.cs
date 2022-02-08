using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models;
[BsonIgnoreExtraElements]
public class LoginRegister
{
    public String? Email { get; set; }
    public String? Password_Hash { get; set; }
    public String? Role { get; set; }
    public String? Salt { get; set; }
}