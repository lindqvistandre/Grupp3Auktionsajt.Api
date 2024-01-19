using AutoMapper;
using Grupp3Auktionsajt.Core.Interfaces;
using Grupp3Auktionsajt.Domain.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize(Roles = "User")]
        public IActionResult CreateUser([FromBody] CreateUserDTO createUserDto)
        {
            try
            {
                _service.CreateUser(createUserDto.Username, createUserDto.Password);
                return Ok("User created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        // Method called UpdateUser
        [HttpPut("update")]
        [Authorize(Roles = "User")]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO updateUserDto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var result = _service.UpdateUser(userId, updateUserDto.Username, updateUserDto.Password);

                if (result)
                {
                    return Ok("User updated successfully.");
                }
                else
                {
                    return BadRequest("User could not be updated.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user");
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }

        // Method called LogIn
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult LogIn([FromBody] LoginDTO userLoginDto)
        {
            try
            {
                var userId = _service.SignIn(userLoginDto.Username, userLoginDto.Password);

                if (userId > 0)
                {
                    string token = _service.GenerateJwtToken(userId);
                    return Ok(new { Token = token });
                }
                else
                {
                    return Unauthorized("Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login.");
                return StatusCode(500, "An error occurred while attempting to log in.");
            }
        }
    }
}
