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
        void CreateBid(int auctionId, int userId, decimal bidPrice);

        // Metod för att hämta alla bud för en specifik auktion
        List<Bid> GetBidsForAuction(int auctionId);
    }
}
