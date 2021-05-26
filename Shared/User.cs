using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DissertationArtefact.Shared
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string SubjectId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Email { get; set; }
        public string? CurrencyPref { get; set; } = "£0.00";
        public bool InRelationship { get; set; }
        public AgeRanges AgeRange { get; set; }
        public bool IsTester { get; set; } = false;

    }

    public enum AgeRanges
    {
        below_18,
        between18to25,
        between25to45,
        between45to55,
        between55to65,
        above_65,
    }
}
