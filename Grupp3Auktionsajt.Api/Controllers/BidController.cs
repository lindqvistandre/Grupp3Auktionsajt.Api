using AutoMapper;
using Grupp3Auktionsajt.Core.Interfaces;
using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Grupp3Auktionsajt.Api.Controllers
{
    [Route("api/bid")]
    [ApiController]
    public class BidController : ControllerBase
    {

        private readonly IBidService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<BidController> _logger;

        public BidController(IBidService service, IMapper mapper, ILogger<BidController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("{bidId}")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteBid(int bidId)
        {

            try
            {
                // Get User ID from the claims
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                _service.DeleteBid(userId, bidId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the bid");
            }

        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult CreateBid([FromBody] CreateBidDto createBidDto)
        {
            try
            {
                var bid = _mapper.Map<Bid>(createBidDto); // Mappar från CreateBidDto till Bid
                _service.CreateBid(bid);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating a new bid");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the bid");
            }
        }

        [HttpGet("auction/{auctionId}")]
        [AllowAnonymous]
        public IActionResult GetBidsForAuction(int auctionId)
        {
            try
            {
                var bids = _service.GetBidsForAuction(auctionId);
                var bidDtos = _mapper.Map<List<BidDTO>>(bids); // Antag att detta mappar från Bid till BidDto
                return Ok(bidDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving bids for auction with id {AuctionId}", auctionId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving bids");
            }
        }


    }
}
