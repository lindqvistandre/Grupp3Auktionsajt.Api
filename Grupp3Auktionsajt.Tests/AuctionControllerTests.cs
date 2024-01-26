using AutoMapper;
using Moq;
using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;
using Grupp3Auktionsajt.Domain.Models.Profiles;
using Grupp3Auktionsajt.Api.Controllers;
using Grupp3Auktionsajt.Core.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Grupp3Auktionsajt.Tests
{
    [TestClass]
    public class AuctionControllerTests
    {
        private Mock<IAuctionService> _auctionServiceMock;
        private Mock<IMapper> _mapperMock; // If someone manages to mock Mapper
        private AuctionController _auctionController;

        [TestInitialize]
        public void Initialize()
        {
            _auctionServiceMock = new Mock<IAuctionService>();

            // Using real AutoMapper since I didn't manage to mock it
            var mapper = ConfigureMapper();

            var loggerMock = new Mock<ILogger<AuctionController>>();

            _auctionController = new AuctionController(_auctionServiceMock.Object, mapper, loggerMock.Object);

            var customerId = 1;
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, customerId.ToString()),
            }));

            _auctionController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        // Configure IMapper
        private IMapper ConfigureMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GetAuctionProfile>();
                cfg.AddProfile<SearchAuctionsProfile>();
            });

            return configuration.CreateMapper();
        }

        [TestMethod]
        public void CreateAuction_ValidData_ReturnsCreated()
        {
            // Arrange
            var auctionDto = new CreateAuctionDTO
            {
                Title = "Test",
                Description = "Test",
                Price = 1,
                Days = 1
            };

            // Mock the IAuctionService method called CreateAuction
            _auctionServiceMock.Setup(service => service.CreateAuction(It.IsAny<int>(), It.IsAny<CreateAuctionDTO>()));

            // Act
            var result = _auctionController.CreateAuction(auctionDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(((ObjectResult)result).StatusCode, StatusCodes.Status201Created);
        }

        [TestMethod]
        public void CreateAuction_ServiceException_ReturnsInternalServerError()
        {
            // Arrange
            var auctionDto = new CreateAuctionDTO
            {
                Title = "Test",
                Description = "Test",
                Price = 1,
                Days = 1
            };

            // Mock the IAuctionService method called CreateAuction
            _auctionServiceMock.Setup(service => service.CreateAuction(It.IsAny<int>(), It.IsAny<CreateAuctionDTO>()))
                                .Throws(new Exception("I missed the part where that's my problem"));

            // Act
            var result = _auctionController.CreateAuction(auctionDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(((ObjectResult)result).StatusCode, 500);
        }

    }
}