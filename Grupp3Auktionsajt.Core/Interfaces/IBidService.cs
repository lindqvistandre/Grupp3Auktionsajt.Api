using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Core.Interfaces
{
    public interface IBidService
    {
        void DeleteBid(int userId, int bidId);

        // Metod för att skapa ett nytt bud
        void CreateBid(int UserId, CreateBidDto createBidDto);

        // Metod för att hämta alla bud för en specifik auktion
        List<Bid> GetBidsForAuction(int auctionId);
    }
}
