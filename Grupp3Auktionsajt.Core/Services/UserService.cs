using Grupp3Auktionsajt.Core.Interfaces;
using Grupp3Auktionsajt.Data.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;

        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }

        public bool CreateUser(string username, string password)    // Correct
        {

            // Get user by Username from the database through a _repo nethod and save it in a variable
            var existingUser = _repo.GetUserByUsername(username);

            if (existingUser == null)
            {
                _repo.CreateUser(username, password);
                return true;
            }

            else
            {
                return false;
            }
        }

        public bool UpdateUser(int userId, string username, string Password)      // Needs to be updated
        {
            // Get the user by Username from the database through a _repo method
            var existingUser = _repo.GetUserByUsername(username);

            // Check if the user exists
            if (existingUser != null)
            {
                // Update the user's information (You can update one or multiple properties of the user object)
                _repo.UpdateUser(userId, username, Password);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteUser(int userId)      // Correct
        {
            _repo.DeleteUser(userId);
        }

        public int SignIn(string username, string password)
        {
            return _repo.UserLogin(username, password);
        }


        // Generate a temporary 180 min token
        public string GenerateJwtToken(int userId)      // Correct
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()), // User ID claim
                new Claim(ClaimTypes.Role, "User") // Role claim
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretKey12345!#12345555555555555555")); // Probably need to use a secure method to store and retrieve the key
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:5265",
                audience: "http://localhost:5265",
                claims: claims,
                expires: DateTime.Now.AddMinutes(180),
                signingCredentials: signinCredentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}


