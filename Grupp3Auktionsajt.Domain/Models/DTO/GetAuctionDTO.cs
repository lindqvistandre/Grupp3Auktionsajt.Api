using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Domain.Models.DTO
{
    public class GetAuctionDTO
    {
        public int AuctionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatorUserId { get; set; }

        // joined properties with the bid table bellow. If there's no bid for the auction the properties will be default.
        public int? BidId { get; set; }
        public int? UserId { get; set; }
        public decimal? BidPrice { get; set; }
        public DateTime? BidTimeStamp { get; set; }
    }
}
