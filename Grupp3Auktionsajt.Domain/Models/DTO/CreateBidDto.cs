using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Domain.Models.DTO
{
    // There were 2 classes in this file so I deleted the unnecessary one, Kevin
    public class CreateBidDto
    {
        [Required]              
        public int AuctionId { get; set; }

        [Required]
        [Range(1, 1000000)] // Antag att budet måste vara inom ett rimligt prisintervall
        public decimal BidPrice { get; set; }
    }
}
