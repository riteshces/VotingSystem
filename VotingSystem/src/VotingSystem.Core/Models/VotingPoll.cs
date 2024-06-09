using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VotingSystem.Core.Models
{
    public class VotingPoll
    {
        public VotingPoll()
        {
            Counters = new List<VotingCounter>();
        }

        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public List<VotingCounter> Counters { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
