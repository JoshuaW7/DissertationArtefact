using System;

namespace DissertationArtefact.Shared
{

    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class Expense
    {
        public Expense()
        {

        }

        public string LabelName { get; set; }
        public double Amount { get; set; }
        public Types Type { get; set; }
        public DateTime PaymentDate { get; set; }
        public ExpenseFrequencies Frequency { get; set; }
        public Categories Category { get; set; }

    }

    public enum Types
    {
        Essential,
        Discretionary
    }
    
    
    public enum ExpenseFrequencies
    {
        OneTime,
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
        Transfer
    }
}