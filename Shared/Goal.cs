using System;

namespace DissertationArtefact.Shared
{

    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class Goal
    {
        public Goal()
        {

        }

        public string GoalName { get; set; }
        public double GoalAmount { get; set; }
        public double StartingAmount { get; set; }
        public GoalCategories GoalCategory { get; set; }
        

    }
    public enum GoalCategories
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