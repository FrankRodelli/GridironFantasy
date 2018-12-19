using System.Collections.Generic;
using System.Linq;
using FantasyGridiron.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FantasyGridiron.Services
{
    public class LeagueService
    {
        private readonly IMongoCollection<League> _leagues;

        public LeagueService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("GridironFantasyDB"));
            var database = client.GetDatabase("GridironFantasyDB");
            _leagues = database.GetCollection<League>("Leagues");
        }

        public List<League> Get()
        {
            //player was book before see if this is now a problem
            return _leagues.Find(league => true).ToList();
        }

        public League Get(string id)
        {
            var docId = new ObjectId(id);
            return _leagues.Find<League>(league => league.Id == docId).FirstOrDefault();
        }

        public League Create(League league)
        {
            _leagues.InsertOne(league);
            return league;
        }

        public void Update(string id, League leagueIn)
        {
            var docId = new ObjectId(id);

            _leagues.ReplaceOne(League => League.Id == docId, leagueIn);
        }

        public void Remove(League leagueIn)
        {
            _leagues.DeleteOne(league => league.Id == leagueIn.Id);
        }

        public void Remove(ObjectId id)
        {
            _leagues.DeleteOne(league => league.Id == id);
        }
    }
}
