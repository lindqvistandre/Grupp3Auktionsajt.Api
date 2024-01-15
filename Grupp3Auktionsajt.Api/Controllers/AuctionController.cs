using AutoMapper;
using Grupp3Auktionsajt.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grupp3Auktionsajt.Api.Controllers
{
    [Route("api/auction")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _service;
        private readonly IMapper _mapper;

        public AuctionController(IAuctionService service)
        {
            _service = service;
        }
    }
}
