using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DissertationArtefact.Shared
{

    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class Income
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }
        public string UserId { get; set; }
        //consider changing to IncomeName
        public string LabelName { get; set; }
        public double Amount { get; set; }
        public string SenderName { get; set; }
        public DateTime PaymentDate { get; set; }
        public IncomeFrequencies Frequency { get; set; }
        //optional end date?
    }

    public enum IncomeFrequencies {
        OneTime,
        Weekly,
        Monthly,
        Annually

    }
}