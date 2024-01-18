using Dapper;
using Grupp3Auktionsajt.Data.Interfaces;
using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;
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
    public class UserRepo : IUserRepo
    {
        private readonly DBContext _context;

        public UserRepo(DBContext context)
        {
            _context = context;
        }

        // Methods for the following 3 classes needs to be made, Kevin

        // UserLogin


        // CreateUser
        public void CreateUser(string username, string password)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", username);
                parameters.Add("@Password", password);
                db.Execute("sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
            }
        }


        // UpdateUser
        public void UpdateUser(string username, string password)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", username);
                parameters.Add("@Password", password);
                db.Execute("sp_UpdateUser", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // UserLogin
        public void UserLogin(string username, string password)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", username);
                parameters.Add("@Password", password);
                var user = db.Execute("sp_UserLogin", parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
