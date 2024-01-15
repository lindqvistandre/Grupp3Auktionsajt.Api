using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupp3Auktionsajt.Core.Interfaces;
using Grupp3Auktionsajt.Data.Interfaces;


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
    }