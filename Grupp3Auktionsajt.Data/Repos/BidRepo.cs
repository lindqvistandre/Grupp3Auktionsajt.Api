using Dapper;
using Grupp3Auktionsajt.Data.Interfaces;
using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Data.Repos
{
    public class BidRepo : IBidRepo
    {
        private readonly IDBContext _context;

        public BidRepo(IDBContext context)
        {
            _context = context;
        }

        public List<Bid> GetBidsForAuction(int auctionId)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AuctionId", auctionId);

                return db.Query<Bid>("sp_GetBidsForAuction", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public Bid GetBidById(int bidId)        // Perhaps don't have stored procedure for this one
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BidId", bidId);

                return db.Query<Bid>("sp_GetBidById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public void CreateBid(int userId, CreateBidDto createBidDto) // added userId as an in-parameter since it was missing, Kevin
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BidPrice", createBidDto.BidPrice);
                parameters.Add("@UserId", userId);
                parameters.Add("@AuctionId", createBidDto.AuctionId);
                
                db.Execute("sp_CreateBid", parameters, commandType: CommandType.StoredProcedure);

            }
        }

        public void DeleteBid(int bidID)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BidId", bidID);

                db.Execute("sp_DeleteBid", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public Auction GetAuctionById(int auctionId)    // Seems to be in the wrong place
        {
            using (var db = _context.GetConnection())
            {
                return db.QueryFirstOrDefault<Auction>("sp_GetAuctionById", new { AuctionId = auctionId }, commandType: CommandType.StoredProcedure);
            }
        }

        public Bid GetHighestBidForAuction(int auctionId)
        {
            using (var db = _context.GetConnection())
            {
                return db.QueryFirstOrDefault<Bid>("sp_GetHighestBidForAuction", new { AuctionId = auctionId }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}

