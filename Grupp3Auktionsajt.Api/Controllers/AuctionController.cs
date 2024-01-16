using AutoMapper;
using Grupp3Auktionsajt.Core.Interfaces;
using Grupp3Auktionsajt.Domain.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Grupp3Auktionsajt.Api.Controllers
{
    [Route("api/auction")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<AuctionController> _logger;

        public AuctionController(IAuctionService service, IMapper mapper, ILogger<AuctionController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("{auctionId}")]
        [Authorize]
        public IActionResult DeleteAuction(int auctionId) 
        {
            return Ok();
            
        }
        [HttpPost]
        [Authorize("create-auction")]
        public IActionResult CreateAuction([FromBody] CreateAuctionDTO auctionDTO)
        {
            try
            {
                // Get user ID from claims
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                // Create the auction
                _service.CreateAuction(auctionDTO, userId);

                return StatusCode(StatusCodes.Status201Created, "Auction created successfully");
            }
            catch (Exception ex)
            {
                //// Log if any exceptions occur
                //_logger.LogError(ex, "Error during auction creation");

                return StatusCode(StatusCodes.Status500InternalServerError, "Error during auction creation");
            }
        }
    }
}