using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Grupp3Auktionsajt.Data.Repos
{
    public class AuctionRepo
    {

        private readonly DBContext _context;

        public AuctionRepo(DBContext context)
        {
            _context = context;
        }
       

        public static void DeleteBid(int bidID)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BidID", bidID);

                db.Execute("DeleteBid", parameters, commandType: CommandType.StoredProcedure);
            }
        }


        public static void DeleteAuction(int auctionID)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AuctionID", auctionID);

                db.Execute("DeleteAuction", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
