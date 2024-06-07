﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VotingSystem.Core.Models
{
    public class Vote
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CounterId { get; set; }
    }
}
