using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DissertationArtefact.Shared;

namespace DissertationArtefact.Shared
{

    public partial class PlutoFunctions
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
                goalAmount -= disposableIncome * (allocation / 100);
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

        public decimal DisposableIncome(List<Income> incomes, List<Expense> expenses)
        {
            decimal income = incomes.Sum(i => i.Amount);
            decimal expsenses = expenses.Sum(e => e.Amount);
            // How much is left over after expenses (Consider case for other goals having allocation)
            decimal disposableIncome = (income - expsenses);
            // How much to allocate from disposable income 

            return disposableIncome;
        }

        public decimal GoalProgressPercentage(decimal targetAmount, decimal savedAmount)
        {
            if (savedAmount == 0)
                throw new Exception($"{nameof(savedAmount)} cannot be 0");
            return ((savedAmount / targetAmount) * 100);
        }

        public Dictionary<(int, int), decimal> ExpenseProjection(int months, List<Expense> expenses, Types[] expenseTypes)
        {
            DateTime today = DateTime.Now;
            Dictionary<(int, int), decimal> projectExpenseDict = new Dictionary<(int, int), decimal>();
            int startMonth = DateTime.Now.Month;
            // project a dictionary based on number of months - this creates a key based on the tuple year/month
            for (int m = startMonth; m < months + startMonth; m++)
            {
                projectExpenseDict.Add((today.Year, today.Month), 0);
                today = today.AddMonths(1);
            }

            var expenseMonthly1 = expenses
                .Where(i => i.Frequency == ExpenseFrequencies.Monthly && expenseTypes.Contains(i.Type));
            //var incomeMonthly = incomes.Where(i => i.Frequency == IncomeFrequencies.Monthly).ToList<Income>();
            decimal expenseMonthly = expenses
                .Where(i => i.Frequency == ExpenseFrequencies.Monthly && expenseTypes.Contains(i.Type))
                .Sum(i => i.Amount);
            // some grouping here would aggregate the "income" to simplify the assignment/addition to the dict key "Year / Month"
            // no need to match as we are iterating over each month 
            foreach (var t in projectExpenseDict)
            {
                projectExpenseDict[t.Key] += expenseMonthly;
            }

            // weekly expenses
            int numberOfWeeks = (int)(((decimal)months) * 4.2M);
            var expenseWeekly = expenses
                .Where(i => i.Frequency == ExpenseFrequencies.Weekly && expenseTypes.Contains(i.Type));
            var expenseWeeklySum = expenses
                .Where(i => i.Frequency == ExpenseFrequencies.Weekly && expenseTypes.Contains(i.Type))
                .Sum(i => i.Amount);
            // don't do this .. reusing today
            today = DateTime.Now;
            for (int week = 0; week < numberOfWeeks; week++)
            {
                int year = today.Year;
                int month = today.Month;
                projectExpenseDict[(year, month)] += expenseWeeklySum;
                today = today.AddDays(7);
            }

            // daily expenses
            var expenseDailySum = expenses
               .Where(i => i.Frequency == ExpenseFrequencies.Daily && expenseTypes.Contains(i.Type))
               .Sum(i => i.Amount);
            today = DateTime.Now;
            int numberOfDays = (today.AddMonths(months) - today).Days - today.Day;
            for (int day = 0; day < numberOfDays; day++)
            {
                int year = today.Year;
                int month = today.Month;
                projectExpenseDict[(year, month)] += expenseDailySum;
                today = today.AddDays(1);
            }

            var expensesAnnual = expenses
                .Where(i => i.Frequency == ExpenseFrequencies.Annually && expenseTypes.Contains(i.Type));
            var years = projectExpenseDict.Keys
                .GroupBy(k => k.Item1)
                .Select(i => i.Key)
                .ToList<int>(); //incomeAnnual.Select(a => a.PaymentDate.Year).ToList<int>();
            foreach (var annual in expensesAnnual)
            {
                foreach (int year in years)
                {
                    // aggregation of the years assumes the months are end of year i.e. Dec so possibly the key can "undershoot"
                    if (projectExpenseDict.ContainsKey((year, annual.PaymentDate.Value.Month)))
                    {
                        projectExpenseDict[(year, annual.PaymentDate.Value.Month)] += annual.Amount;
                    }
                }
            }

            return projectExpenseDict;
        }

        // Project forward the amount of income over set number of months from "Now" to Now.Months+months "In the future..."
        public Dictionary<(int, int), decimal> IncomeProjection(int months, List<Income> incomes)

        {
            DateTime today = DateTime.Now;
            int startMonth = DateTime.Now.Month;
            Dictionary<(int, int), decimal> projectIncomeDict = new Dictionary<(int, int), decimal>();

            // project a dictionary based on number of months - this creates a key based on the tuple year/month
            for (int m = DateTime.Now.Month; m < months + startMonth; m++)
            {
                projectIncomeDict.Add((today.Year, today.Month), 0);
                today = today.AddMonths(1);
            }

            // test - expand the dict to test...
            //List<(int, int, decimal)> projectedIncome = new List<(int, int, decimal)>();

            var incomeOneTime = incomes.Where(i => i.Frequency == IncomeFrequencies.OneTime).ToList<Income>();

            // get all of the one time payments and add them to the income for that year month... once only
            foreach (var oneTime in incomeOneTime)
            {
                if (projectIncomeDict.ContainsKey((oneTime.PaymentDate.Year, oneTime.PaymentDate.Month)))
                    projectIncomeDict[(oneTime.PaymentDate.Year, oneTime.PaymentDate.Month)] += oneTime.Amount;
            }

            //var incomeMonthly = incomes.Where(i => i.Frequency == IncomeFrequencies.Monthly).ToList<Income>();
            decimal incomeMonthly = incomes
                .Where(i => i.Frequency == IncomeFrequencies.Monthly)
                .Sum(i => i.Amount);
            // some grouping here would aggregate the "income" to simplify the assignment/addition to the dict key "Year / Month"
            // no need to match as we are iterating over each month 
            foreach (var t in projectIncomeDict)
            {
                projectIncomeDict[t.Key] += incomeMonthly;
            }

            var incomeAnnual = incomes
                .Where(i => i.Frequency == IncomeFrequencies.Annually);
            var years = projectIncomeDict.Keys
                .GroupBy(k => k.Item1)
                .Select(i => i.Key)
                .ToList<int>(); //incomeAnnual.Select(a => a.PaymentDate.Year).ToList<int>();
            foreach (var annual in incomeAnnual)
            {
                foreach (int year in years)
                {
                    if (projectIncomeDict.ContainsKey((year, annual.PaymentDate.Month)))
                        projectIncomeDict[(year, annual.PaymentDate.Month)] += annual.Amount;
                }

            }


            // how to work out the number of weeks in a month???... (Answer - Google of course!)
            // int weeksInAMonth = DateTime.Now.

            return projectIncomeDict;
        }


        public List<(int, int, decimal, decimal, decimal)> IncomeExpenseProjection(int months, List<Income> incomes, List<Expense> expenses, Types[] expenseTypes)
        {
            // Get projected Income for months into the future
            PlutoFunctions f = new PlutoFunctions();
            var ip = f.IncomeProjection(months, incomes);
            var exp = f.ExpenseProjection(months, expenses, expenseTypes);

            List<(int, int, decimal, decimal, decimal)> incomeExpenseProjection = new List<(int, int, decimal, decimal, decimal)>();
            foreach (var t in ip)
            {
                decimal income = ip[t.Key];
                decimal expense = exp[t.Key];
                decimal disposableIncome = ip[t.Key] - exp[t.Key];
                // year, month, incometotal, expensetotal, disposableIncome
                incomeExpenseProjection.Add((t.Key.Item1, t.Key.Item2, ip[t.Key], exp[t.Key], ip[t.Key] - exp[t.Key]));
            }
            // tabulated results
            return incomeExpenseProjection;
        }

        public List<(int, int, decimal, decimal, decimal)> IncomeExpenseProjection(int months, List<Income> incomes, List<Expense> expenses)
        {
            // Get projected Income for months into the future
            PlutoFunctions f = new PlutoFunctions();
            var ip = f.IncomeProjection(months, incomes);
            var expEssential = f.ExpenseProjection(months, expenses, new Types[] { Types.Essential });
            var expDiscretionary = f.ExpenseProjection(months, expenses, new Types[] { Types.Discretionary });

            List<(int, int, decimal, decimal, decimal)> incomeExpenseProjection
                = new List<(int, int, decimal, decimal, decimal)>();

            foreach (var t in ip)
            {
                decimal income = ip[t.Key];
                decimal essentialExpense = expEssential[t.Key];
                decimal discretionaryExpense = expDiscretionary[t.Key];
                //decimal allExpense = essentialExpense + discretionaryExpense;// exp[t.Key];
                //decimal disposableIncome = income - allExpense;
                // year, month, incometotal, expensetotal, disposableIncome
                incomeExpenseProjection.Add((t.Key.Item1, t.Key.Item2, income, essentialExpense, discretionaryExpense));
            }
            // tabulated results
            return incomeExpenseProjection;
        }

        public int NumberOfWeeksInAMonth(DateTime today)
        {
            //extract the month
            int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
            DateTime firstOfMonth = new DateTime(today.Year, today.Month, 1);
            //days of week starts by default as Sunday = 0
            int firstDayOfMonth = (int)firstOfMonth.DayOfWeek;
            int weeksInMonth = ((int)(firstDayOfMonth + daysInMonth) / 7);
            //Console.WriteLine("5 weeks in month");
            return weeksInMonth;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="goalAmount"></param>
        /// <param name="incExptDict"></param>
        /// <param name="allocationPercentage"></param>
        /// <returns></returns>

        public List<Contribution> goalContributions(decimal goalAmount,
            List<(int, int, decimal, decimal, decimal)> incExptDict,
            decimal allocationPercentage = 100)
        {
            List<Contribution> contributions = new List<Contribution>();
            decimal savedAmount = 0; // goal.StartAmount
                                     //First series
                                     //var collection = function goalAmount, m.ed? series=>{ m} dispamountsOverTime(m) for expense types Disc+Estent
            foreach (var incExp in incExptDict)
            {
                //Item5 is the calculated Disposable Income... Ideally we'd probably refactor the Tuple to use a projection from a new object
                decimal contribAmountPerMonth = incExp.Item5 * (allocationPercentage / 100);
                if (savedAmount + contribAmountPerMonth < goalAmount)
                {
                    //Item1 is Year - Item2 is Month
                    contributions.Add(new Contribution { MonthYear = new DateTime(incExp.Item1, incExp.Item2, 1), Amount = contribAmountPerMonth });
                }
                else if (savedAmount + contribAmountPerMonth > goalAmount)
                {
                    contribAmountPerMonth = goalAmount - savedAmount;
                    contributions.Add(new Contribution { MonthYear = new DateTime(incExp.Item1, incExp.Item2, 1), Amount = contribAmountPerMonth });
                }
                savedAmount += contribAmountPerMonth;
                if (savedAmount >= goalAmount)
                {
                    break;
                }
            }

            return contributions;
        }

        public List<Contribution> goalContributions(
            Goal goal,
            List<(int, int, decimal, decimal, decimal)> incExptDict,
            bool accrue = true,
            decimal reductionPercentage = 100)
        {
            List<Contribution> contributions = new List<Contribution>();
            decimal savedAmount = goal.StartAmount;
                                     //First series
                                     //var collection = function goalAmount, m.ed? series=>{ m} dispamountsOverTime(m) for expense types Disc+Estent
            decimal contribAmountPerMonth = 0;

            foreach (var incExp in incExptDict)
            {
                //Item5 is the calculated Disposable Income for discretionary expenses

                // contribAmountPerMonth = incExp.Item5 * (reductionPercentage / 100);

                //DI = Income - (EE+DE*R%)
                decimal di = 0;
                decimal income = incExp.Item3;
                decimal ee = incExp.Item4;
                decimal de = incExp.Item5;

                di = income - (ee + (de * (reductionPercentage / 100)));

                contribAmountPerMonth = di; /*incExp.Item3-incExp.Item4+(incExp.Item5*(reductionPercentage / 100));*/

                if (savedAmount + contribAmountPerMonth < goal.TargetAmount)
                {
                    //Item1 is Year - Item2 is Month
                    contributions.Add(new Contribution { MonthYear = new DateTime(incExp.Item1, incExp.Item2, 1), Amount = accrue ? savedAmount+contribAmountPerMonth : contribAmountPerMonth });
                }
                else if (savedAmount + contribAmountPerMonth > goal.TargetAmount)
                {
                    contribAmountPerMonth += goal.TargetAmount - savedAmount;
                    contributions.Add(new Contribution { MonthYear = new DateTime(incExp.Item1, incExp.Item2, 1), Amount = accrue ? savedAmount+contribAmountPerMonth : contribAmountPerMonth });
                }
                savedAmount += contribAmountPerMonth;
                if (savedAmount >= goal.TargetAmount)
                {
                    break;
                }
            }

            return contributions;
        }
    }

}