using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Core.Interfaces
{
    public interface IUserService
    {
        bool CreateUser(string username, string password);
        bool UpdateUser(int userId, string username, string Password);
        void DeleteUser(int userId);
        int SignIn(string username, string password);
        string GenerateJwtToken(int userId);
    }
}
