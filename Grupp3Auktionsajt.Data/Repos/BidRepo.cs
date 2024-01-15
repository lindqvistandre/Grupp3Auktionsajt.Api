using Dapper;
using Grupp3Auktionsajt.Domain.Models.Entities;
using Microsoft.Data.SqlClient;
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

        public List<Bid> GetBidsForAuction(int auctionId)
        {
            using (var db = _context.GetConnection())
            {
                return db.Query<Bid>("GetBidsForAuction", new { AuctionId = auctionId }, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public void CreateBid(int auctionId, int userId, decimal bidPrice)
        {
            using (var db = _context.GetConnection())
            {
                try
                {
                    // Anropa den lagrade proceduren "CreateBid"
                    db.Execute("CreateBid", new { AuctionId = auctionId, UserId = userId, BidPrice = bidPrice }, commandType: CommandType.StoredProcedure);

                   
                }
                catch (SqlException ex)
                {
                    // Hantera specifika fel som genereras av RAISERROR i SQL
                    if (ex.Number == 50000) // Anpassade fel för användardefinierade fel
                    {
                       
                    }
                    else
                    {
                        
                    }

                   
                    throw;

                    
                }
            }
        }
    }
}
