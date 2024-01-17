using Dapper;
using Grupp3Auktionsajt.Domain.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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

        public List<Bid> GetBidsForAuction(int auctionId)
        {
            using (var db = _context.GetConnection())
            {
                return db.Query<Bid>("sp_GetBidsForAuction", new { AuctionId = auctionId }, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public void CreateBid(int auctionId, int userId, decimal bidPrice)
        {
            using (var db = _context.GetConnection())
            {
                // Kontrollera om auktionen är öppen
                var isOpen = db.QueryFirstOrDefault<bool>("SELECT IsOpen FROM Auctions WHERE AuctionId = @AuctionId", new { AuctionId = auctionId });
                if (!isOpen)
                {
                    // Anropa den lagrade proceduren "CreateBid"
                    db.Execute("sp_CreateBid", new { AuctionId = auctionId, UserId = userId, BidPrice = bidPrice }, commandType: CommandType.StoredProcedure);

                    // Kontrollera att användaren inte är skaparen av auktionen
                    var creatorId = db.QueryFirstOrDefault<int>("SELECT UserId FROM Auctions WHERE AuctionId = @AuctionId", new { AuctionId = auctionId });
                    if (userId == creatorId)
                    {
                        throw new InvalidOperationException("Du kan inte lägga bud på din egen auktion.");
                    }

                    // Kontrollera att budet är högre än det nuvarande högsta budet
                    var highestBid = db.QueryFirstOrDefault<decimal>("SELECT MAX(BidPrice) FROM Bids WHERE AuctionId = @AuctionId", new { AuctionId = auctionId });
                    if (bidPrice <= highestBid)
                    {
                        throw new InvalidOperationException("Budet är för lågt.");
                    }

                    // Lägg till budet
                    db.Execute("CreateBid", new { AuctionId = auctionId, UserId = userId, BidPrice = bidPrice }, commandType: CommandType.StoredProcedure);
                }
            }
        }
        public void DeleteBid(int bidID)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BidID", bidID);

                db.Execute("sp.DeleteBid", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}

