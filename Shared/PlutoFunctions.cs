using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DissertationArtefact.Shared;

namespace DissertationArtefact.Shared
{
    public class PlutoFunctions
    {

        public int MonthsToGoal(Goal goal, List<Income> incomes, List<Expense> expenses, decimal allocation)
        {
            //date  1   2   3   4   5   6   7
            //inc   100 100 100 100 100 100 100
            //exp   90  90  90  90  90  90  90
            //disp  10  10  10  10  10  10  10
            //accru 10  20  30  40  50  60  20
            //goal                          50
            //mtg    4  3   2   1   1   
            //realiz                    50

            //List<object> months = new List<object>();
            //for(int m = 1; m < 7; m++)
            //{

            //}

            int months = 0;

            //int timeToGoal = amount of time => number of months takes to reduce goal amount to zero from allocation from disposable income
            // 

            decimal income = incomes.Sum(i => i.Amount);
            decimal expsenses = expenses.Sum(e => e.Amount);
            decimal disposableIncome = income - expsenses;
            decimal goalAmount = goal.TargetAmount - goal.StartAmount;

            while (goalAmount > 0)
            {
                months++;
                goalAmount -= disposableIncome * (allocation/100);
            }

            return months;
        }

        public List<(int, decimal, decimal)> Intervals(Goal goal, List<Income> incomes, List<Expense> expenses, decimal allocationPercentage = 100)
        {
            List<(int, decimal, decimal)> intervals = new List<(int, decimal, decimal)>();

            int months = 0;

            //int timeToGoal = amount of time => number of months takes to reduce goal amount to zero from allocation from disposable income
            // 

            decimal income = incomes.Sum(i => i.Amount);
            decimal expsenses = expenses.Sum(e => e.Amount);
            // How much is left over after expenses (Consider case for other goals having allocation)
            decimal disposableIncome = (income - expsenses);
            // How much to allocate from disposable income 
            decimal goalContribution = disposableIncome * (allocationPercentage / 100);
            decimal goalAmount = goal.TargetAmount - goal.StartAmount;
            decimal saved = 0;
            int month = 0;

            while (saved < goalAmount)
            {
                month++;
                saved += goalContribution;
                intervals.Add((month, goalContribution, saved));
            }

            return intervals;
        }
    }

}
