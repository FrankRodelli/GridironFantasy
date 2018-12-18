using System.Collections.Generic;
using System.Linq;
using FantasyGridiron.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FantasyGridiron.Services
{
    public class PlayerService
    {
        private readonly IMongoCollection<Player> _players;

        public PlayerService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("GridironFantasyDB"));
            var database = client.GetDatabase("GridironFantasyDB");
            _players = database.GetCollection<Player>("Players");
        }

        public List<Player> Get()
        {
            return _players.Find(book => true).ToList();
        }

        public Player Get(string id)
        {
            var docId = new ObjectId(id);
            return _players.Find<Player>(player => player.Id == docId).FirstOrDefault();
        }

        public Player Create(Player player)
        {
            _players.InsertOne(player);
            return player;
        }

        public void Update(string id, Player playerIn)
        {
            var docId = new ObjectId(id);

            _players.ReplaceOne(Player => Player.Id == docId, playerIn);
        }

        public void Remove(Player playerIn)
        {
            _players.DeleteOne(player => player.Id == playerIn.Id);
        }

        public void Remove(ObjectId id)
        {
            _players.DeleteOne(player => player.Id == id);
        }
    }
}
