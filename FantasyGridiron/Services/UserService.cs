using System.Collections.Generic;
using System.Linq;
using FantasyGridiron.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FantasyGridiron.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("GridironFantasyDB"));
            var database = client.GetDatabase("GridironFantasyDB");
            _users = database.GetCollection<User>("Users");
        }

        public List<User> Get()
        {
            //user was book before. see if this is now a problem
            return _users.Find(user => true).ToList();
        }

        public User Get(string id)
        {
            var docId = new ObjectId(id);
            return _users.Find<User>(user => user.Id == docId).FirstOrDefault();
        }

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn)
        {
            var docId = new ObjectId(id);

            _users.ReplaceOne(User => User.Id == docId, userIn);
        }

        public void Remove(User userIn)
        {
            _users.DeleteOne(user => user.Id ==userIn.Id);
        }

        public void Remove(ObjectId id)
        {
            _users.DeleteOne(user => user.Id == id);
        }
    }
}
