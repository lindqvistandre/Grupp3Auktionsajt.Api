using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Data.Interfaces
{
    public interface IUserRepo // Correct, Kevin
    {
        void CreateUser(string username, string password);
        void UpdateUser(int userId, string username, string password);
        int UserLogin(string username, string password);
        User GetUserByUsername(string username);

    }
}
