using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Users.API.Exceptions;
using Users.API.Model;
using Users.API.Model.DTOs;
using Users.API.Services;

namespace Users.API.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UsersController(IUsersService usersServices, IMapper mapper)
        {
            _usersService = usersServices;
            _mapper = mapper;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _usersService.FetchAllUsers();

            var usersDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);

            return Ok(usersDTOs);
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal error ocurred. Please contact support" });
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
