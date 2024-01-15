using Grupp3Auktionsajt.Domain.Models.DTO;
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


    }
}
