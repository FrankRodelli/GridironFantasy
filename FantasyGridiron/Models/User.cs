using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace FantasyGridiron.Models
{
    public class User
    {
        public ObjectId Id { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Photo")]
        public string Photo { get; set; }
    }
}
