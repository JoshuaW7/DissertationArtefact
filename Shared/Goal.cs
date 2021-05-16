using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DissertationArtefact.Shared
{

    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class Goal
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }
        public string UserId { get; set; }

        // How much disposable income to be set aside for goal (0-100%)
        public decimal Allocation { get; set; }
        public string GoalName { get; set; }
        public GoalCategories GoalCategory { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal StartAmount { get; set; }
        // To be calculated
        public TimeSpan? TimeToGoal { get; set; }
        public List<BookedAmount> BookedAmounts { get; set; }
        public DateTime CreationDate { get; set; }
        
    }
    public enum GoalCategories
    {
        Holiday,
        HouseDeposit,
        Wedding,
        TuitionFees,
        General,
        Pension,

    }

    public class BookedAmount
    {
        public decimal Amount { get; set; }
        public DateTime BookedOn { get; set; }
        public string Currency { get; set; }

    }
}