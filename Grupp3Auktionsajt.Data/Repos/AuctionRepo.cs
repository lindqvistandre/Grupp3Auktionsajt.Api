using Dapper;
using Grupp3Auktionsajt.Data.Interfaces;
using Grupp3Auktionsajt.Domain.Models.DTO;
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
    public class AuctionRepo : IAuctionRepo
    {

        private readonly DBContext _context;

        public AuctionRepo(DBContext context)
        {
            _context = context;
        }

        public void CreateAuction(int UserId, CreateAuctionDTO auctionDTO)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
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

       

        public IEnumerable<Auction> SearchAuctions(string SearchTerm)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("SearchTerm", SearchTerm);

                return db.Query<Auction>("sp_SearchAuctions", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // UpdateAuction
    }
}
