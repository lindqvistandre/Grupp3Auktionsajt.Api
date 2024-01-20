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
        [AllowAnonymous]
        public IActionResult CreateUser([FromBody] CreateUserDTO createUserDto)     // Correct
        {
            try
            {
                // Try creating the account
                var result = _service.CreateUser(createUserDto.Username, createUserDto.Password);

                if (result)
                    return Ok("User created successfully.");
                else
                    return BadRequest("Username already taken");
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
        public IActionResult UpdateUser([FromBody] UpdateUserDTO updateUserDto) // Correct
        {
            try
            {
                // Get User ID from the claims
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                // Try updating the user
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

        // Delete a User
        [HttpDelete("delete/{deleteUserId}")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteUser(int deleteUserId)   // Correct
        {
            try
            {
                // Get User ID from the claims
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                if (userId != deleteUserId)
                {
                    return BadRequest("Not authorized or User does not not exist. User could not be deleted");
                }
                else
                {
                    // Proceed to delete the user
                    _service.DeleteUser(userId);
                    return Ok("User Deleted");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during deletion.");
                return StatusCode(500, "An error occurred while attempting to delete a user.");
            }
        }

        // Method called LogIn
        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult LogIn([FromBody] LoginDTO userLoginDto) // Correct
        {
            try
            {
                // Try signing in
                var userId = _service.SignIn(userLoginDto.Username, userLoginDto.Password);

                if (userId > 0) // Valid credentials
                {
                    string token = _service.GenerateJwtToken(userId);
                    return Ok(new { Token = token });
                }
                else // Invalid credentials
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
