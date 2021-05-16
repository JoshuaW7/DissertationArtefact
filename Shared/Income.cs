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
        public string LabelName { get; set; }
        public decimal Amount { get; set; }
        public string SenderName { get; set; }
        public DateTime PaymentDate { get; set; }
        public IncomeFrequencies Frequency { get; set; }
        public int? NumberOfPayments { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public enum IncomeFrequencies {
        OneTime,
        Weekly,
        Monthly,
        Annually

    }

}