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
    public class UserService
    {
        private readonly IUserRepo _repo;

        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }

        public bool CreateUser(string username, string password)
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

        public bool UpdateUser(string username, string newPassword)
        {
            // Get the user by Username from the database through a _repo method
            var existingUser = _repo.GetUserByUsername(username);

            // Check if the user exists
            if (existingUser != null)
            {
                // Update the user's password
                _repo.UpdateUser(username, newPassword);
                return true; // User successfully updated
            }
            else
            {
                return false; // User does not exist
            }
        }




    }
}

// Method for signing in

// 1.Method for Creating a user

// Method for Updating a user

