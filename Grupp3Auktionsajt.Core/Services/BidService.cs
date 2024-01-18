using Grupp3Auktionsajt.Core.Interfaces;
using Grupp3Auktionsajt.Data.Interfaces;
using Grupp3Auktionsajt.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Core.Services
{
    public class BidService: IBidService
    {
        private readonly IBidRepo _repo;
      
        public BidService(IBidRepo repo)
        {
            _repo = repo;
        }

        public void DeleteBid(int userId, int bidID)
        {
            /// You need check If the user who made the Bid deletes it.
            
            // 1. call a stored procedure that gets the Bid you want to delete based on BidId
            // 2. check if userId of the user who tries to delete the bid matches the userId of the one who made the bid.


            // check that you can't delete a bid after an auction is over

            
            _repo.DeleteBid(bidID);
        }

        public void CreateBid(CreateBidDto createBidDto)
        {
            _repo.CreateBid(createBidDto);
        }
    }
}
