using AutoMapper;
using Grupp3Auktionsajt.Core.Interfaces;
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
        public IActionResult DeleteBid(int  bidId)
        {

            return Ok();

        }
    }
}
