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
        /*private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        */
        private readonly ILogger<IncomeController> _logger;

        public IncomeController(ILogger<IncomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Income> Get()
        {
            var incomes = new Array<Income>
            {
                new Income {Amount = "2000", LabelName = "Salary", Occurance = Occurances.monthly, PaymentDate = "15/12/2020",  SenderName = "Job"},
                new Income {Amount = "2000", LabelName = "Salary", Occurance = Occurances.monthly, PaymentDate = "15/12/2020",  SenderName = "Job"},
                new Income {Amount = "2000", LabelName = "Salary", Occurance = Occurances.monthly, PaymentDate = "15/12/2020",  SenderName = "Job"},
                new Income {Amount = "2000", LabelName = "Salary", Occurance = Occurances.monthly, PaymentDate = "15/12/2020",  SenderName = "Job"},
                new Income {Amount = "2000", LabelName = "Salary", Occurance = Occurances.monthly, PaymentDate = "15/12/2020",  SenderName = "Job"},
                new Income {Amount = "2000", LabelName = "Salary", Occurance = Occurances.monthly, PaymentDate = "15/12/2020",  SenderName = "Job"}

            };
            return incomes;
        }
    }
}
