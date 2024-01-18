using Grupp3Auktionsajt.Core.Interfaces;
using Grupp3Auktionsajt.Data.Interfaces;
using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
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


        public bool DeleteBid(int userId, int bidId)        // Liknar hur ni ska göra i "DeleteAuction"
        {
            // 1. Call a stored procedure to get the bid based on BidId
            var bid = _repo.GetBidById(bidId);

            // 2. Check if the userId of the user who tries to delete the bid matches the userId of the one who made the bid
            if (bid != null && bid.UserId == userId)
            {
                // Proceed with deleting the bid if the user is the owner
                _repo.DeleteBid(bidId);

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Bid> GetBidsForAuction(int auctionId)
        {
            return _repo.GetBidsForAuction(auctionId);
        }


        public void CreateBid(int userId, CreateBidDto createBidDto) // Correct
        {
            _repo.CreateBid(userId, createBidDto);
        }
    }
}
