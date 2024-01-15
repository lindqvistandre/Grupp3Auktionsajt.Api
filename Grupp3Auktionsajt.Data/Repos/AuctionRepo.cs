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

        public static void DeleteAuction(int auctionID)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AuctionID", auctionID);

                db.Execute("DeleteAuction", parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
