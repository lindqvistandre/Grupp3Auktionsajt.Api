using Dapper;
using Grupp3Auktionsajt.Data.Interfaces;
using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Grupp3Auktionsajt.Data.Repos
{
    public class AuctionRepo : IAuctionRepo
    {

        private readonly DBContext _context;

        public AuctionRepo(DBContext context)
        {
            _context = context;
        }

        public void CreateAuction(int UserId, CreateAuctionDTO auctionDTO) // Correct
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", UserId);
                parameters.Add("@Title", auctionDTO.Title);
                parameters.Add("@Description", auctionDTO.Description);
                parameters.Add("@Price", auctionDTO.Price);
                parameters.Add("@Days", auctionDTO.Days);

                db.Execute("sp_CreateAuction", parameters, commandType: CommandType.StoredProcedure);
            }
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


        public void DeleteAuction(int auctionID)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AuctionID", auctionID);

                db.Execute("sp_DeleteAuction", parameters, commandType: CommandType.StoredProcedure);
            }
        }

       

        public IEnumerable<Auction> SearchAuctions(string SearchTerm) // (Kevin)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SearchTerm", SearchTerm);

                return db.Query<Auction>("sp_SearchAuctions", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public Auction GetAuctionById(int auctionId)    // (Kevin) Perhaps will remove this later
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AuctionId", auctionId);

                return db.Query<Auction>("sp_GetAuctionById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public Auction GetAuctionDetailsById(int auctionId) // (Kevin) Shows details for an auction and together with a join with the highest bid
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AuctionId", auctionId);

                var result = db.Query<Auction, Bid, Auction>(
                    "sp_GetAuctionDetailsById",
                    (auction, bid) =>
                    {
                        if (bid != null)
                        {
                            auction.Bids = new List<Bid> { bid };
                        }
                        return auction;
                    },
                    parameters,
                    splitOn: "BidId",
                    commandType: CommandType.StoredProcedure
                )
                .Distinct() // We only want to return one Auction object
                .FirstOrDefault();

                return result;
            }
        }

        public Auction GetBidById(int auctionId) // This is in the wrong place
        {
            throw new NotImplementedException();
        }
    }
}
