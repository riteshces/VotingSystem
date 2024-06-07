using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VotingSystem.Core.Models
{
    public class VotingCounter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string VotingOptionName { get; set; }
        public int VoteCount { get; set; }
        public double VotingPercentage { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
