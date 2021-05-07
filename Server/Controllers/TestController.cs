using DissertationArtefact.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DissertationArtefact.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("user/{id}")]
        public async Task<ActionResult> GetByMyDef(string id)
        {
            var expenses = new List<Expense>()
            {
                new Expense{Amount=100},
                new Expense{Amount=200}
            };

            return new OkObjectResult(expenses);
            //return $"you sent me {id}";
        }
    }
}