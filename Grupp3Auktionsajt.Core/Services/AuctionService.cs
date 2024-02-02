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
        private readonly IBidRepo _bidRepo;

        public AuctionService(IAuctionRepo repo, IBidRepo bidRepo)
        {
            _repo = repo;
            _bidRepo = bidRepo;
        }

        public bool DeleteAuction(int userId, int auctionId)
        {
            var auction = _repo.GetAuctionById(auctionId);
            var bids = _bidRepo.GetBidsForAuction(auctionId);

            if (bids.Count == 0 && auction.CreatorUserId == userId)
            {
                // Proceed with deleting the bid if the user is the owner
                _repo.DeleteAuction(auctionId);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void CreateAuction(int UserId, CreateAuctionDTO auctionDTO) // Correct
        {
            _repo.CreateAuction(UserId, auctionDTO);
        }

        public IEnumerable<Auction> SearchAuction(string keyword) // (Kevin)
        {
            return _repo.SearchAuctions(keyword);
        }

        public Auction GetAuctionDetailsById(int auctionId) // (Kevin)
        {
            return _repo.GetAuctionDetailsById(auctionId);
        }
    }

}