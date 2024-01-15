using Grupp3Auktionsajt.Data.Interfaces;
using Grupp3Auktionsajt.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Core.Services
{
    public class BidService
    {
        private readonly IBidRepo _repo;

        public BidService(IBidRepo repo)
        {
            _repo = repo;
        }

        public void DeleteBid(int bidID)
        {
            _repo.DeleteBid(bidID);
        }
    }
}
