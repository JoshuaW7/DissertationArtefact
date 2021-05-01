using DissertationArtefact.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DissertationArtefact.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ILogger<ExpenseController> _logger;

        public ExpenseController(ILogger<ExpenseController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Expense> Get()
        {
            var expenses = new List<Expense>
            {
                new Expense {Amount = 30, LabelName = "Internet", Frequency = ExpenseFrequencies.Monthly, PaymentDate = new DateTime(2020, 12, 15), Type = Types.Essential, Category = Categories.Bill},
                new Expense {Amount = 400, LabelName = "Rent", Frequency = ExpenseFrequencies.Monthly, PaymentDate = new DateTime(2020, 12, 15), Type = Types.Essential, Category = Categories.Bill },
                new Expense {Amount = 80, LabelName = "Energy", Frequency = ExpenseFrequencies.Monthly, PaymentDate = new DateTime(2020, 12, 15), Type = Types.Essential, Category = Categories.Bill },
                new Expense {Amount = 1000, LabelName = "Council Tax", Frequency = ExpenseFrequencies.Monthly, PaymentDate = new DateTime(2020, 12, 15), Type = Types.Essential, Category = Categories.Bill },
                new Expense {Amount = 9.99, LabelName = "Netflix", Frequency = ExpenseFrequencies.Monthly, PaymentDate = new DateTime(2020, 12, 15), Type = Types.Discretionary, Category = Categories.Entertainment },

                new Expense {Amount = 200, LabelName = "Clothes", Frequency = ExpenseFrequencies.OneTime, PaymentDate = new DateTime(2020, 12, 21), Type = Types.Discretionary, Category = Categories.Shopping },
                new Expense {Amount = 360, LabelName = "British Airways", Frequency = ExpenseFrequencies.OneTime, PaymentDate = new DateTime(2020, 12, 20), Type = Types.Discretionary, Category = Categories.Travel },
                new Expense {Amount = 50, LabelName = "Food Shop", Frequency = ExpenseFrequencies.OneTime, PaymentDate = new DateTime(2020, 12, 15), Type = Types.Essential, Category = Categories.Groceries },
                new Expense {Amount = 1000, LabelName = "Entertainment", Frequency = ExpenseFrequencies.Monthly, PaymentDate = new DateTime(2020, 12, 15), Type = Types.Discretionary, Category = Categories.Entertainment },
            };
            return expenses;
        }
    }
}
