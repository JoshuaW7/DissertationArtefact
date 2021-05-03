using DissertationArtefact.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using WinOS = System.IO;

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
          
            string DataFile = @$"d:\temp\incomes.json";

            if (!WinOS.File.Exists(DataFile))
            {
                return null;
            }

            string incomesJSON = WinOS.File.ReadAllText(DataFile);

            List<Income> incomes = JsonSerializer.Deserialize<List<Income>>(incomesJSON);

            // Consider a default sort... date

            return incomes;

        }

        [HttpPost()]
        public async Task<IActionResult> Post(Income income)
        {
            string DataFile = @$"d:\temp\incomes.json";

            if (!WinOS.File.Exists(DataFile))
            {
                return BadRequest();
            }

            string IncomesJSON = WinOS.File.ReadAllText(DataFile);

            List<Income> incomes = JsonSerializer.Deserialize<List<Income>>(IncomesJSON);

            if (income.IncomeID is null || income.IncomeID.ToString().Length == 0)
            {
                income.IncomeID = Guid.NewGuid();
                incomes.Add(income);
            }
            else
            {
                Income incomeCurrent = incomes.Where(i => i.IncomeID == income.IncomeID).FirstOrDefault();

                if (incomeCurrent is null)
                {
                    return NotFound();
                }

                incomes.Remove(incomeCurrent);
                incomes.Add(income);

            }

            IncomesJSON = JsonSerializer.Serialize(incomes);
            WinOS.File.WriteAllText(DataFile, IncomesJSON);

            // Returns 200
            return new CreatedResult($"income/{income.IncomeID}", income);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            string DataFile = @$"d:\temp\incomes.json";

            if (!WinOS.File.Exists(DataFile))
            {
                return BadRequest();
            }

            string incomesJSON = WinOS.File.ReadAllText(DataFile);

            List<Income> incomes = JsonSerializer.Deserialize<List<Income>>(incomesJSON);

            Income incomeCurrent = incomes.Where(i => i.IncomeID.ToString() == id).FirstOrDefault();
            if (incomeCurrent is null)
            {
                return NotFound();
            }

            // Changes when database is implemented...
            incomes.Remove(incomeCurrent);
            incomesJSON = JsonSerializer.Serialize(incomes);
            WinOS.File.WriteAllText(DataFile, incomesJSON);

            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            // User field needed at some point...
            // Extract to file (Read & Write)
            //string s = WinOS.File.ReadAllText("");

            string DataFile = @$"d:\temp\incomes.json";

            if (!WinOS.File.Exists(DataFile))
            {
                return BadRequest();
            }

            string IncomesJSON = WinOS.File.ReadAllText(DataFile);

            List<Income> incomes = JsonSerializer.Deserialize<List<Income>>(IncomesJSON);

            var income = incomes.Where(i => i.IncomeID == new Guid(id)).FirstOrDefault();

            return new OkObjectResult(income);

        }

    }
}
