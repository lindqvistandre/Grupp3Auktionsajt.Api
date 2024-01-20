using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;

namespace Grupp3Auktionsajt.Domain.Models.Profiles
{
    public class GetAuctionProfile : Profile
    {
        public GetAuctionProfile()
        {
            CreateMap<Auction, GetAuctionDTO>()
                .ForMember(dest => dest.AuctionId, opt => opt.MapFrom(src => src.AuctionId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.CreatorUserId, opt => opt.MapFrom(src => src.CreatorUserId))
                .ForMember(dest => dest.BidId, opt => opt.MapFrom(src => src.Bids.Count > 0 ? src.Bids[0].BidId : (int?)null))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Bids.Count > 0 ? src.Bids[0].UserId : (int?)null))
                .ForMember(dest => dest.BidPrice, opt => opt.MapFrom(src => src.Bids.Count > 0 ? src.Bids[0].BidPrice : (decimal?)null))
                .ForMember(dest => dest.BidTimeStamp, opt => opt.MapFrom(src => src.Bids.Count > 0 ? src.Bids[0].BidTimeStamp : (DateTime?)null));
        }
    }
}
