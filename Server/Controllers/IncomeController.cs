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
    public class IncomeController : ControllerBase
    {
        private readonly ILogger<IncomeController> _logger;

        public IncomeController(ILogger<IncomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Income> Get()
        {
            var incomes = new List<Income>
            {
                new Income {Amount = 2000, LabelName = "Salary", Frequency = IncomeFrequencies.Monthly, PaymentDate = new DateTime(2020, 12, 15),  SenderName = "Job"},
                new Income {Amount = 1200, LabelName = "Property", Frequency = IncomeFrequencies.Monthly, PaymentDate = new DateTime(2020, 12, 15),  SenderName = "Tenant"},
                new Income {Amount = 200, LabelName = "Present", Frequency = IncomeFrequencies.OneTime, PaymentDate = new DateTime(2020, 12, 23),  SenderName = "Parents"},
                new Income {Amount = 360, LabelName = "Salary", Frequency = IncomeFrequencies.OneTime, PaymentDate = new DateTime(2020, 12, 22),  SenderName = "British Airways"},
                new Income {Amount = 25, LabelName = "Salary", Frequency = IncomeFrequencies.OneTime, PaymentDate = new DateTime(2020, 12, 15),  SenderName = "Friend"},
                new Income {Amount = 7.50, LabelName = "Salary", Frequency = IncomeFrequencies.OneTime, PaymentDate = new DateTime(2020, 12, 15),  SenderName = "Friend"}

            };
            return incomes;
        }
    }
}
