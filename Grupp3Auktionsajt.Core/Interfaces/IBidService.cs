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
        bool DeleteBid(int userId, int bidId);

        int CreateBid(int userId, CreateBidDto createBidDto);

        List<Bid> GetBidsForAuction(int auctionId);
    }
}
