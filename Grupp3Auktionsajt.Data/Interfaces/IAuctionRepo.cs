﻿using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Data.Interfaces
{
    public interface IAuctionRepo
    {
        void DeleteAuction(int auctionID);


        void CreateAuction(CreateAuctionDTO auctionDTO, int UserId);

        void UpdateAuctionPrice(int auctionId, decimal newPrice);

        IEnumerable<Auction> SearchAuctions(string SearchTerm);
    }
}
