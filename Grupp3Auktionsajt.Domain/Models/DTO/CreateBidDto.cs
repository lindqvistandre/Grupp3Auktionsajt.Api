﻿using System;
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
        public int AuctionId { get; set; }
        public decimal BidPrice { get; set; }
    }
}
