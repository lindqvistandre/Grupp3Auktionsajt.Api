using Dapper;
using Grupp3Auktionsajt.Data.Interfaces;
using Grupp3Auktionsajt.Domain.Models.DTO;
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
       

        public void DeleteAuction(int auctionID)
        {
            using (var db = _context.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AuctionID", auctionID);

                db.Execute("DeleteAuction", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void CreateAuction (CreateAuctionDTO auctionDTO, int UserId)
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
    }
}
