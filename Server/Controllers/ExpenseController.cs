//---------new--------------->
using DissertationArtefact.Server.Services;
using DissertationArtefact.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DissertationArtefact.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly ExpenseService expenseService;

        public ExpenseController(
            ILogger<ExpenseController> logger,
            ExpenseService expenseService)
        {
            _logger = logger;
            this.expenseService = expenseService;
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult> GetByMyDef(string id)
        {
            _logger.LogDebug("Got here !");

            //var expenses = new List<Expense>()
            //{
            //    new Expense{Amount=100},
            //    new Expense{Amount=200}
            //};

            List<Expense> expenses = expenseService.Get(new User { Id = id });

            return new OkObjectResult(expenses);
            //return $"you sent me {id}";
        }
    }
}




//------------original--------------->


//using DissertationArtefact.Shared;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text.Json;
//using System.Threading.Tasks;
//using WinOS = System.IO;
//using DissertationArtefact.Server.Services;

//namespace DissertationArtefact.Server.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ExpenseController : ControllerBase
//    {
//        private readonly ILogger<ExpenseController> _logger;

//        private readonly ExpenseService expenseService;


//        public ExpenseController(
//            ILogger<ExpenseController> logger)
//            //,ExpenseService expenseService)
//        {
//            this._logger = logger;
//            this.expenseService = null;
//        }

//        [HttpPost()]
//        public async Task<IActionResult> Post(Expense expense)
//        {
//            if(expense.Id.Length == 0)
//            {
//                expenseService.Create(expense);
//            }
//            else
//            {
//                expenseService.Update(expense.Id, expense);
//            }

//            return new CreatedResult($"expenses/{expense.Id}", expense);
//        }

//[HttpDelete("{id}")]
//public async Task<IActionResult> Delete(string id)
//{
//    string DataFile = @$"d:\temp\expenses.json";

//    if (!WinOS.File.Exists(DataFile))
//    {
//        return BadRequest();
//    }

//    string expensesJSON = WinOS.File.ReadAllText(DataFile);

//    List<Expense> expenses = JsonSerializer.Deserialize<List<Expense>>(expensesJSON);

//    Expense expenseCurrent = expenses.Where(i => i.Id.ToString() == id).FirstOrDefault();
//    if (expenseCurrent is null)
//    {
//        return NotFound();
//    }

// Changes when database is implemented...
//expenses.Remove(expenseCurrent);
//expensesJSON = JsonSerializer.Serialize(expenses);
//WinOS.File.WriteAllText(DataFile, expensesJSON);

//    return Ok();
//}


//    [HttpGet("{id}")]
//    public async Task<IActionResult> GetByID(string id)
//    {
//        Expense expense = expenseService.Get(id);
//        return new OkObjectResult(expense);
//    }

//    [HttpGet("user/{id}")]
//    public async Task<IActionResult> GetByUserID(string id)
//    {
//        List<Expense> expenses = expenseService.Get(new User { Id = id });
//        return new OkObjectResult(expenses);
//    }


//}
//}
