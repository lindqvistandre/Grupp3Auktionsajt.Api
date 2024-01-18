using Grupp3Auktionsajt.Domain.Models.DTO;
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
        void UpdateUser(string username, string password);
        void UserLogin(string username, string password);
    }
}
