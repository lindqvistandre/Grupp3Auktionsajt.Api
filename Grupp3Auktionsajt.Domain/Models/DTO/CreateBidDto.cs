using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Domain.Models.DTO
{
    public class CreateBidDto
    {
        [Required]
        public int AuctionId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(1, 1000000)] // Antag att budet måste vara inom ett rimligt prisintervall
        public decimal BidPrice { get; set; }
    }

    public class BidDto
    {
        public int BidId { get; set; }
        public int UserId { get; set; }
        public decimal BidPrice { get; set; }
        public DateTime BidTimeStamp { get; set; }

       
    }


}
