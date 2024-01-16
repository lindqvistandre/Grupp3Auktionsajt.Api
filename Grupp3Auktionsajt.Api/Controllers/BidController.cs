using AutoMapper;
using Grupp3Auktionsajt.Core.Interfaces;
using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grupp3Auktionsajt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {

        private readonly IAuctionService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<BidController> _logger;

        public BidController(IAuctionService service, IMapper mapper, ILogger<BidController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("{bidId}")]
        public IActionResult DeleteBid(int bidId)
        {

            return Ok();

        }

        [HttpPost]
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
        public IActionResult GetBidsForAuction(int auctionId)
        {
            try
            {
                var bids = _service.GetBidsForAuction(auctionId);
                var bidDtos = _mapper.Map<List<BidDto>>(bids); // Antag att detta mappar från Bid till BidDto
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
