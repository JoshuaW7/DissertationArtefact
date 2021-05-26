using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DissertationArtefact.Shared
{

    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class Expense
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }
        public string UserId { get; set; }
        //ExpenseName
        public string LabelName { get; set; }
        public decimal Amount { get; set; }
        public Types Type { get; set; }
        public DateTime? PaymentDate { get; set; }
        public ExpenseFrequencies Frequency { get; set; }
        public Categories Category { get; set; }
        //PaymentEndDate and StartDate?
        //Currency? ...account setting

    }

    public enum Types
    {
        Essential,
        Discretionary
    }
    
    
    public enum ExpenseFrequencies
    {
        OneTime,
        Daily,
        Weekly,
        Monthly,
        Annually
    }

    public enum Categories 
    { 
        General, 
        Shopping, 
        Groceries, 
        FoodandDrink, 
        Entertainment, 
        Travel, 
        Bill, 
        Transfer,
        Investment,
        Cleaning,
        MedicalHealth,
        Housing,
        Insurance
    }
}