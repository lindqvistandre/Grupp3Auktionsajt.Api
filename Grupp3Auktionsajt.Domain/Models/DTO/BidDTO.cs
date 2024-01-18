using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Domain.Models.DTO
{
    public class BidDTO
    {
        public int BidId { get; set; }
        public int AuctionId { get; set; }
        public int UserId { get; set; }
        public decimal BidPrice { get; set; }
        public DateTime BidTimeStamp { get; set; }
    }
}
