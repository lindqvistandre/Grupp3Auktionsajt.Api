using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Data.Repos
{
    internal class BidRepo
    {
        private readonly DBContext _context;

        public BidRepo(DBContext context)
        {
            _context = context;
        }
        public void DeleteBid(int bidID)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BidID", bidID);

                db.Execute("DeleteBid", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
