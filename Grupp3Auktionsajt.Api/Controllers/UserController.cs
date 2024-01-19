using AutoMapper;
using Grupp3Auktionsajt.Core.Interfaces;
using Grupp3Auktionsajt.Domain.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grupp3Auktionsajt.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService service, IMapper mapper, ILogger<UserController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        // Method for creating a user
        [HttpPost("create")]
        public ActionResult CreateUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                // You might want to validate the DTO here before proceeding

                _service.CreateNewUser(createUserDto.Username, createUserDto.Password);

                // If the creation is successful, return a success response
                return Ok("User created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");

                // If there's an error, return an appropriate response
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        // Method called UpdateUser
        [HttpPut("update/{userId}")]
        public ActionResult UpdateUser(int userId, [FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                // You might want to validate the DTO here before proceeding

                var result = _service.UpdateExistingUser(userId, updateUserDto.NewUsername, updateUserDto.NewPassword);

                if (result)
                {
                    // If the update is successful, return a success response
                    return Ok("User updated successfully.");
                }
                else
                {
                    // If the update is not successful, handle accordingly
                    return BadRequest("User could not be updated.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user");

                // If there's an error, return an appropriate response
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }

        // Method called LogIn
        [HttpPost("login")]
        public ActionResult<UserDto> LogIn([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                // You might want to validate the DTO here before proceeding

                var userDto = _service.SignInUser(userLoginDto.Username, userLoginDto.Password);

                if (userDto != null)
                {
                    // If the sign in is successful, return the user data (consider what data should be returned)
                    return Ok(userDto);
                }
                else
                {
                    // If the sign in is not successful, return an unauthorized response
                    return Unauthorized("Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login.");

                // If there's an error, return an appropriate response
                return StatusCode(500, "An error occurred while attempting to log in.");
            }
        }



        // Method called LogIn

        //1. Method called CreateUser

        // Method called UpdateUser
    }
}
