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

        // How much disposable income to be set aside for goal (0-100%)
        public decimal Allocation { get; set; }
        public string GoalName { get; set; }
        public GoalCategories GoalCategory { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal StartAmount { get; set; }
        // To be calculated
        public TimeSpan? TimeToGoal { get; set; }

    }
    public enum GoalCategories
    {
        Holiday,
        HouseDeposit,
        Wedding,
        TuitionFees
    }
}