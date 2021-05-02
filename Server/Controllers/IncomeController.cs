﻿using DissertationArtefact.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

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
        public List<Income> Get(string ID)
        {
            // User field needed at some point...
            // Extract to file (Read & Write)

            var incomes = new List<Income>
            {
                new Income {IncomeID = Guid.NewGuid(), Amount = 2000, LabelName = "Salary", Frequency = IncomeFrequencies.Monthly, PaymentDate = new DateTime(2020, 12, 15),  SenderName = "Job"},
                new Income {IncomeID = Guid.NewGuid(), Amount = 1200, LabelName = "Property", Frequency = IncomeFrequencies.Monthly, PaymentDate = new DateTime(2020, 12, 15),  SenderName = "Tenant"},
                new Income {IncomeID = Guid.NewGuid(), Amount = 200, LabelName = "Present", Frequency = IncomeFrequencies.OneTime, PaymentDate = new DateTime(2020, 12, 23),  SenderName = "Parents"},
                new Income {IncomeID = Guid.NewGuid(), Amount = 360, LabelName = "Salary", Frequency = IncomeFrequencies.OneTime, PaymentDate = new DateTime(2020, 12, 22),  SenderName = "British Airways"},
                new Income {IncomeID = Guid.NewGuid(), Amount = 25, LabelName = "Salary", Frequency = IncomeFrequencies.OneTime, PaymentDate = new DateTime(2020, 12, 15),  SenderName = "Friend"},
                new Income {IncomeID = Guid.NewGuid(), Amount = 7.50, LabelName = "Salary", Frequency = IncomeFrequencies.OneTime, PaymentDate = new DateTime(2020, 12, 15),  SenderName = "Friend"}

            };
           // File.
            return incomes;
        }

        // POST
        // To append the new "Income" object to your existing JSON....
        // ... Read all of the file/JSON into your LIST
        // ... Use List.Append(income);
        // ... Write out the JSON of the LIST..

        // PUT / POST with string ID
        // ... Read all of the file/JSON into your LIST
        // ... iterate over the list find the object - assign the values to the object
        // ... Write out the JSON of the LIST..
/*
        [HttpPost]
        public void PostIncome (Income Income) 
        {
            
            List<Income> incomes = new List<Income>()
            {
                new Income {IncomeID = Guid.NewGuid(), Amount = 2000, LabelName = "Salary", Frequency = IncomeFrequencies.Monthly, PaymentDate = new DateTime(2020, 12, 15),  SenderName = "Job"},
                new Income {IncomeID = Guid.NewGuid(), Amount = 1200, LabelName = "Property", Frequency = IncomeFrequencies.Monthly, PaymentDate = new DateTime(2020, 12, 15),  SenderName = "Tenant"},
                new Income {IncomeID = Guid.NewGuid(), Amount = 200, LabelName = "Present", Frequency = IncomeFrequencies.OneTime, PaymentDate = new DateTime(2020, 12, 23),  SenderName = "Parents"},
                new Income {IncomeID = Guid.NewGuid(), Amount = 360, LabelName = "Salary", Frequency = IncomeFrequencies.OneTime, PaymentDate = new DateTime(2020, 12, 22),  SenderName = "British Airways"},
                new Income {IncomeID = Guid.NewGuid(), Amount = 25, LabelName = "Salary", Frequency = IncomeFrequencies.OneTime, PaymentDate = new DateTime(2020, 12, 15),  SenderName = "Friend"},
                new Income {IncomeID = Guid.NewGuid(), Amount = 7.50, LabelName = "Salary", Frequency = IncomeFrequencies.OneTime, PaymentDate = new DateTime(2020, 12, 15),  SenderName = "Friend"}

            };

            string incomeJSON = JsonSerializer.Serialize(incomes);

           // File.WriteAllText(@$"c:\temp\{ nameof(incomes)}.json", incomeJSON);


        }*/
    }
}
