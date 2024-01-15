using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupp3Auktionsajt.Core.Interfaces;
using Grupp3Auktionsajt.Data.Interfaces;
using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;


namespace Grupp3Auktionsajt.Core.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepo _repo;

        public AuctionService(IAuctionRepo repo)
        {
            _repo = repo;
        }

        public void DeleteAuction(int auctionID)
        {
            _repo.DeleteAuction(auctionID);
        }

        public void CreateAuction(CreateAuctionDTO auctionDTO, int UserId)
        {
            _repo.CreateAuction(auctionDTO, UserId);
        }
    }

}