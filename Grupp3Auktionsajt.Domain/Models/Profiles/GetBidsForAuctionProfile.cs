using Grupp3Auktionsajt.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Grupp3Auktionsajt.Domain.Models.DTO;

namespace Grupp3Auktionsajt.Domain.Models.Profiles
{
    public class GetBidsForAuctionProfile : Profile
    {
        public GetBidsForAuctionProfile()
        {
            CreateMap<Bid, BidDTO>()
                .ForMember(dest => dest.BidId, opt => opt.MapFrom(src => src.BidId))
                .ForMember(dest => dest.AuctionId, opt => opt.MapFrom(src => src.AuctionId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.BidPrice, opt => opt.MapFrom(src => src.BidPrice))
                .ForMember(dest => dest.BidTimeStamp, opt => opt.MapFrom(src => src.BidTimeStamp));
        }
    }
}
