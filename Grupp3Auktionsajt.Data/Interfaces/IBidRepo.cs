using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Data.Interfaces
{
    public interface IBidRepo
    {
        void DeleteBid(int bidID);
        // Metod för att skapa ett nytt bud
        void CreateBid(int userId, CreateBidDto createBidDto); // Updated the in-parameters here aswell, Kevin

        // Metod för att hämta alla bud för en specifik auktion
        List<Bid> GetBidsForAuction(int auctionId);

        Bid GetBidById(int bidId);
        Bid GetHighestBidForAuction(int auctionId);
        Auction GetAuctionById(int auctionId);
    }
}
