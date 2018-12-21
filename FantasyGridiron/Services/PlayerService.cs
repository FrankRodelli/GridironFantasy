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
            //player was book before see if this is now a problem
            return _players.Find(player => true).ToList();
        }

        public Player Get(string docId)
        {
            return _players.Find<Player>(player => player.Id == docId).FirstOrDefault();
        }

        public Player Create(Player player)
        {
            _players.InsertOne(player);
            return player;
        }

        public void Update(string docId, Player playerIn)
        {

            _players.ReplaceOne(Player => Player.Id == docId, playerIn);
        }

        public void Remove(Player playerIn)
        {
            _players.DeleteOne(player => player.Id == playerIn.Id);
        }

        public void Remove(string id)
        {
            _players.DeleteOne(player => player.Id == id);
        }
    }
}
