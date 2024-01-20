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


        //[HttpPost("{auctionId}")]
        //[Authorize(Roles = "User")]
        //public IActionResult DeleteAuction(int auctionId)         // Will test this later in postman
        //{            
        //    try
        //    {
        //        // Get User ID from the claims
        //        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        //        // try deleting the auction
        //        var deleteAuction = _service.DeleteAuction(userId, auctionId);

        //        if (deleteAuction == true)
        //        {
        //            return Ok("Delete auction successful");
        //        }
        //        else
        //        {
        //            return BadRequest("Couldn't delete the auction");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while  deleting the auction");
        //    }      
        //}


        [HttpPost("create-auction")]
        [Authorize(Roles = "User")]
        public IActionResult CreateAuction([FromBody] CreateAuctionDTO auctionDTO) // Correct
        {
            try
            {
                // Get user ID from claims
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                // Create the auction
                _service.CreateAuction(userId, auctionDTO);

                return StatusCode(StatusCodes.Status201Created, "Auction created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error couldn't create the auction");
            }  
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public IActionResult SearchAuction([FromQuery] string keyword) // (Kevin)
        {
            try
            {
                // Get a list of the search result
                var auctions = _service.SearchAuction(keyword);

                // Map the entities to DTOs
                var searchAuctionDTOs = _mapper.Map<IEnumerable<SearchAuctionDTO>>(auctions);
                
                return Ok(searchAuctionDTOs);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error couldn't search auctions");
            }
        }

        // GetAuctionDetails
        [HttpGet("{auctionId}")]
        [AllowAnonymous]
        public IActionResult GetAuctionDetails(int auctionId) // (Kevin)
        {
            try
            {
                // try get the auction
                var auction = _service.GetAuctionDetailsById(auctionId);

                // Map the entity to DTO
                var auctionDto = _mapper.Map<GetAuctionDTO>(auction);

                if (auctionDto != null)
                {
                    return Ok(auctionDto);
                }
                else
                {
                    return NotFound("Auction could not be found");
                }
 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error couldn't get auction");
            }
        }
    }
}