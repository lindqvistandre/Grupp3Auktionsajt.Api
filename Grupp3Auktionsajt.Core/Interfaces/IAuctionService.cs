using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Core.Interfaces
{
    public interface IAuctionService
    {
        bool DeleteAuction(int userId, int auctionId);
        void CreateAuction(int UserId, CreateAuctionDTO auctionDTO);
        IEnumerable<Auction> SearchAuction(string keyword);
    }
}
