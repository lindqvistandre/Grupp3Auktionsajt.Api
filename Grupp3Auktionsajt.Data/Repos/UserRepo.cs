using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Data.Repos
{
    internal class UserRepo
    {
        public class UserLoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        //[HttpPost]
        //[Route("login")]
        //public IActionResult Login(UserLoginModel user)
        //{
        //    // Här ska du lägga till koden för att ansluta till databasen
        //    // och anropa din Stored Procedure med användaruppgifterna.

        //    // Låt oss anta att du har en metod som heter `AuthenticateUser` som gör detta och returnerar UserId
        //    var userId = AuthenticateUser(user.Username, user.Password);

        //    if (userId != null)
        //    {
        //        // Användaren är autentiserad
        //        return Ok(userId);
        //    }
        //    else
        //    {
        //        // Autentisering misslyckades
        //        return UnauthorizedResult();
        //    }
        //}

        
    }
}
