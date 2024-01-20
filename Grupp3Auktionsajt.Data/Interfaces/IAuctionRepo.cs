using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Data.Interfaces
{
    public interface IAuctionRepo
    {
        void CreateAuction(int UserId, CreateAuctionDTO auctionDTO);

        void UpdateAuctionPrice(int auctionId, decimal newPrice);
        void DeleteAuction(int auctionID);

        IEnumerable<Auction> SearchAuctions(string SearchTerm);

        Auction GetAuctionById(int auctionId);

        Auction GetAuctionDetailsById(int auctionId);

        Auction GetBidById(int auctionId); // This is at the wrong place
    }
}
