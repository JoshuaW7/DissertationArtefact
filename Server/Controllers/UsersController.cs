using DissertationArtefact.Server.Services;
using DissertationArtefact.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DissertationArtefact.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _usersService;

        public UsersController(UserService usersService)
        {
            _usersService = usersService;
        }


        // GET: api/<UsersController>
        [HttpGet]
        public ActionResult<User> Get()
        {
            return new User { Name = "Anonymous", CreatedOn = DateTime.Now, Email = "" };
        }


        [HttpGet("{id}")]
        public ActionResult<User> GetById(string id)
        {
            User user = _usersService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        //[HttpGet("{id}")]
        //public User Get(string id)
        //{
        //    return new User 
        //    { 
        //        Name="Test User", 
        //        CreatedOn=DateTime.Now, 
        //        Email="test.user@plutodomain2021.com" 
        //    };
        //}

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            if (user.Id.Length == 0)
                _usersService.Create(user);
            else
                _usersService.Update(user.Id, user);

            return CreatedAtRoute("PostUser", new { id = user.Id.ToString() }, user);
        }

        //// PUT api/<UsersController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UsersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
