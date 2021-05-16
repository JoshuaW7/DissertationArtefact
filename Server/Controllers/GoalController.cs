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
    public class GoalController : ControllerBase
    {
        private readonly ILogger<GoalController> _logger;
        private readonly GoalService goalService;

        public GoalController(
            ILogger<GoalController> logger,
            GoalService goalService)
        {
            _logger = logger;
            this.goalService = goalService;
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult> GetByMyDef(string id)
        {
            _logger.LogDebug("Got here !");



            List<Goal> goals = goalService.Get(new User { Id = id });

            return new OkObjectResult(goals);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            Goal goal = goalService.Get(id);
            return new OkObjectResult(goal);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            goalService.Remove(id);

            return NoContent();
        }

        [HttpPost()]
        public async Task<IActionResult> Post(Goal goal)
        {
            if (string.IsNullOrEmpty(goal.Id))
            {
                goalService.Create(goal);
            }
            else
            {
                goalService.Update(goal.Id, goal);
            }

            return new CreatedResult($"goal/{goal.Id}", goal);
        }
    }
}


