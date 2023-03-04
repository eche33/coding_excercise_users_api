using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all users")]
        [SwaggerResponse(200, "All users returned", typeof(IEnumerable<UserDTO>))]
        [SwaggerResponse(500, "Internal error", typeof(string))]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _usersService.FetchAllUsers();

                var usersDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);

                return Ok(usersDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal error ocurred. Please contact support" });
            }
        }

        [HttpGet("{email}")]
        [SwaggerOperation(Summary = "Retrieves user with email")]
        [SwaggerResponse(200, "User returned", typeof(UserDTO))]
        [SwaggerResponse(404, "User not found", typeof(string))]
        [SwaggerResponse(500, "Internal error", typeof(string))]
        public IActionResult GetUser(string email)
        {
            try
            {
                var user = _usersService.FetchUser(email);
                var userDTO = _mapper.Map<UserDTO>(user);
                return Ok(userDTO);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal error ocurred. Please contact support" });
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new user")]
        [SwaggerResponse(201, "User created", typeof(UserForCreationDTO))]
        [SwaggerResponse(400, "Missing information to create user", typeof(IDictionary<string, string>))]
        [SwaggerResponse(500, "Internal error", typeof(string))]
        public IActionResult Post([FromBody] UserForCreationDTO userForCreation)
        {
            var user = _mapper.Map<User>(userForCreation);

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
