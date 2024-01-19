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
        public void UpdateUser(int userId, string username, string password)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                parameters.Add("@UserName", username);
                parameters.Add("@Password", password);
                db.Execute("sp_UpdateUser", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // DeleteUser
        public void DeleteUser(int userId)
        {
            using(var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                db.Execute("sp_DeleteUser", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // UserLogin
        public int UserLogin(string username, string password)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", username);
                parameters.Add("@Password", password);
                return db.Query<int>("sp_UserLogin", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public User GetUserByUsername(string username)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", username);
                return db.Query<User>("sp_GetUserByUsername", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

    }
}
