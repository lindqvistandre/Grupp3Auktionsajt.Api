using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Data.Repos
{
    public class UserRepo
    {
        private readonly DBContext _context;

        public UserRepo(DBContext context)
        {
            _context = context;
        }

        // User Login

        // Create User

        // Update User


    }
}
