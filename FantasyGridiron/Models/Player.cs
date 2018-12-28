using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FantasyGridiron.Models
{
    public class Player
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("YHId")]
        public string YHId { get; set; }

        [BsonElement("Name")]
        public string PlayerName { get; set; }

        [BsonElement("Position")]
        public string PlayerPosition { get; set; }

        [BsonElement("Team")]
        public string PlayerTeam { get; set; }

        //Might need to change this to bson int if any errors
        [BsonElement("Number")]
        public string PlayerNumber { get; set; }

        [BsonElement("Drafted")]
        public string PlayerDrafted { get; set; }

        [BsonElement("College")]
        public string PlayerCollege { get; set; }

        [BsonElement("Weight")]
        public string PlayerWeight { get; set; }

        [BsonElement("Height")]
        public string PlayerHeight { get; set; }

        [BsonElement("Picture")]
        public string PlayerImage { get; set; }
    }
}
