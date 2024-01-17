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
        int UserLogin(LoginDTO LoginDTO);
        void CreateUser(CreateUserDTO createUserDTO);
        void UpdateUser(int customerId, UpdateUserDTO updateUserDTO);
    }
}
