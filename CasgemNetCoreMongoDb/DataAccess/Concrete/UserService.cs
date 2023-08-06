using DataAccess.Abstract;
using Entity.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _user;

        public UserService(IDataBaseSettings dbSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(dbSettings.DatabaseName);
            _user = database.GetCollection<User>(dbSettings.UserCollectionName);
        }

        public User Add(User user)
        {
            user.Id = ObjectId.GenerateNewId().ToString();
            _user.InsertOne(user);
            return user;
        }

        public void Delete(string id)
        {
            _user.DeleteOne(user => user.Id == id);
        }

        public User Get(string id)
        {
            return _user.Find(user => user.Id == id).FirstOrDefault();
        }

        public List<User> GetByFilter(string? userName)
        {
            var filterBuilder = Builders<User>.Filter;
			var filter = filterBuilder.Empty;

			if (!string.IsNullOrEmpty(userName))
			{
				filter = filter & filterBuilder.Where(user => user.UserName.ToLower().Contains(userName.ToLower()));
			}

			return _user.Find(filter).ToList();
        }

        public List<User> Get()
        {
            return _user.Find(user => true).ToList();
        }

        public void Update(string id, User user)
        {
            _user.ReplaceOne(user => user.Id == id, user);
        }
    }
}
