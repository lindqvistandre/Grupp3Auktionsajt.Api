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

        public void UpdateAuctionPrice(int auctionId, decimal newPrice)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();

                parameters.Add("@AuctionId", auctionId);
                parameters.Add("@NewPrice", newPrice);

                db.Execute("sp_UpdateAuctionPrice", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Create User


    }
}
