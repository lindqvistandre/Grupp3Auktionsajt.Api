using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Domain.Models.Entities
{
    public class User // klar
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Relationships
        public List<Auction> Auctions { get; set; } // Could use ICollection instead of list.
        public List<Bid> Bids { get; set; }
    }
}
