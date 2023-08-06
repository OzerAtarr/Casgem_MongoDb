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
    public class EstateService : IEstateService
    {
        private readonly IMongoCollection<Estate> _estate;

        public EstateService(IDataBaseSettings dbSettings, IMongoClient mongoClient)
        {

            var database = mongoClient.GetDatabase(dbSettings.DatabaseName);
            _estate = database.GetCollection<Estate>(dbSettings.EstateCollectionName);
        }


        public Estate Add(Estate estate)
        {
            estate.Id = ObjectId.GenerateNewId().ToString();
            _estate.InsertOne(estate);
            return estate;
        }

        public void Delete(string id)
        {
            _estate.DeleteOne(estate => estate.Id == id);
        }

        public List<Estate> GetByFilter(string? city, string? type, int? room, string? title, int? price, string? buildYear)
        {
            var filterBuilder = Builders<Estate>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(city))
            {
                filter = filter & filterBuilder.Where(estate => estate.City.ToLower().Contains(city.ToLower()));
            }

            if (!string.IsNullOrEmpty(type))
            {
                filter = filter & filterBuilder.Where(estate => estate.Type.ToLower().Contains(type.ToLower()));
            }

            if (room.HasValue)
            {
                filter = filter & filterBuilder.Eq(estate => estate.Room, room.Value);
            }

            if (!string.IsNullOrEmpty(title))
            {
                filter = filter & filterBuilder.Where(estate => estate.Title.ToLower().Contains(title.ToLower()));
            }

            if (price.HasValue)
            {
                filter = filter & filterBuilder.Eq(estate => estate.Price, price.Value);
            }

            if (!string.IsNullOrEmpty(buildYear))
            {
                filter = filter & filterBuilder.Eq(estate => estate.BuildYear, buildYear);
            }

            return _estate.Find(filter).ToList();
        }

        public Estate Get(string id)
        {
            return _estate.Find(estate => estate.Id == id).FirstOrDefault();
        }

        public List<Estate> Get()
        {
            return _estate.Find(estate => true).ToList();
        }

        public void Update(string id, Estate estate)
        {
            _estate.ReplaceOne(estate => estate.Id == id, estate);
        }
    }
    
}
