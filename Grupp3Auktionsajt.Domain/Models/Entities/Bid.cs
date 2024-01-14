using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Domain.Models.Entities
{
    public class Bid // klar
    {
        public int BidId { get; set; }
        public int AuctionId { get; set; }
        public int UserId { get; set; }
        public decimal BidPrice { get; set; }
        public DateTime BidTimeStamp { get; set; } //BidDate

        // Relationships
        public Auction Auction { get; set; }
        public User User { get; set; }
    }
}
