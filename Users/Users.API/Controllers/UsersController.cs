using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Users.API.Exceptions;
using Users.API.Model;
using Users.API.Services;

namespace Users.API.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUsersService _usersService;
        public UsersController(IUsersService usersServices)
        {
            _usersService = usersServices;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_usersService.FetchAllUsers());
        }

        // GET api/<UsersController>/5
        [HttpGet("{email}")]
        public string GetUser(string email)
        {
            return "Hola";
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                _usersService.AddUser(user);
                return Created(Request.GetDisplayUrl(), user);
            }
            catch (UserAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
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
