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
    public class BidService : IBidService
    {
        private readonly IBidRepo _repo;
        private readonly IAuctionRepo _auctionRepo; // AuctionRepo are at most only used for the "public bool DeleteBid(int userId, int bidId)" method. If you decide to to update or create a new stored procedure (following what I've written bellow in Method: 2), we don't need this dependency injection.

        public BidService(IBidRepo repo, IAuctionRepo auctionRepo)
        {
            _repo = repo;
            _auctionRepo = auctionRepo;
        }


        // This method looks mostly fine. The only thing missing is to hinder the user to remove their bid after an auction is over.    Kevin

        // Method 1:
        // One way is by extracting the "AuctionId" from the "var bid" variable bellow and then call the "_auctionRepo.GetAuctionById()" method with this AuctionId. After that you can compare the Endate for the auction and compare it to the current date.

        // Method 2:
        // You can skip having to inject the "IAuctionRepo auctionRepo" in the contructor if you update your "_repo.GetBidById(bidId)" method or make a new one. What you have to do is to inner join the bid with its corresponding 
        public bool DeleteBid(int userId, int bidId) 
        {
            // 1. Call a stored procedure to get the bid based on BidId
            var bid = _repo.GetBidById(bidId);

            int auctionId = bid.AuctionId;

            // Auction object
            var auction = _auctionRepo.GetAuctionById(auctionId);

            if(DateTime.Now > auction.EndDate)
            {
                return false;
            }

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

        public List<Bid> GetBidsForAuction(int auctionId) // You need to check if the auction is still open. If it's closed you should hinder the user from getting the bids. You can read the comments above for more ideas, Kevin
        {
            return _repo.GetBidsForAuction(auctionId);
        }


        //public void CreateBid(int userId, CreateBidDto createBidDto)
        //{
        //    var auction = _repo.GetAuctionById(createBidDto.AuctionId);
        //    if (auction == null || auction.EndDate <= DateTime.Now)
        //    {
        //        throw new InvalidOperationException("Auction isn't open anymore for bids");
        //    }

        //    // Checking if auction isnt created by user.
        //    if (auction.CreatorUserId == userId)
        //    {
        //        throw new InvalidOperationException("You cant bid on your own auction.");
        //    }

        //    // Check if bid is higher then new one.
        //    var highestBid = _repo.GetHighestBidForAuction(createBidDto.AuctionId);
        //    if (highestBid != null && createBidDto.BidPrice <= highestBid.BidPrice)
        //    {
        //        throw new InvalidOperationException("Bid must be higher then previous bid.");
        //    }

        //    // Skapa budet
        //    _repo.CreateBid(userId, createBidDto);
        //}




        // This method is mostly correct. The issue is that the method "GetAuctionById" is found in the Bid Repository.
        // The Bid repository should only contain methods related to getting, creating, updating and deleting bids.
        // It should not contain a method to retrieve an auction. Read the comments above that I wrote on the DeleteBid method for more information, Kevin
        public int CreateBid(int userId, CreateBidDto createBidDto)
        {
            var auction = _auctionRepo.GetAuctionById(createBidDto.AuctionId);
            if (auction == null || auction.EndDate <= DateTime.Now)
            {
                return 1;
            }

            // Checking if auction isnt created by user.
            if (auction.CreatorUserId == userId)
            {
                return 2;
            }

            // Check if bid is higher then new one.
            var highestBid = _repo.GetHighestBidForAuction(createBidDto.AuctionId);
            if (highestBid != null && createBidDto.BidPrice <= highestBid.BidPrice)
            {
                return 3;
            }

            // Create the bid
            _repo.CreateBid(userId, createBidDto);
            return 0;

        }

    }
}
