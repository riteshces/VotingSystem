using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using VotingSystem.Core.Models;

namespace VotingSystem.Database
{
    public class AppDbContext : DbContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        public AppDbContext(IMongoClient mongoClient)
        {
            string strConnectionString = "mongodb://localhost:27017/VotingSystem";
            MongoUrl mongoUrl = new MongoUrl(strConnectionString);
            mongoClient = new MongoClient(mongoUrl);
            _mongoDatabase = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoCollection<VotingCounter> VotingCounters => _mongoDatabase.GetCollection<VotingCounter>("votingcounter");
        public IMongoCollection<VotingPoll> VotingPolls => _mongoDatabase.GetCollection<VotingPoll>("votingpoll");
        public IMongoCollection<Vote> Votes => _mongoDatabase.GetCollection<Vote>("votes");
    }
}