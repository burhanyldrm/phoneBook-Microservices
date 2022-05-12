using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Report.Api.Entities.Enums;
using System;

namespace Report.Api.Entities
{
    public class ContactReport
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public State Status { get; set; }
    }
}
