using AutoMapper;
using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupp3Auktionsajt.Domain.Models.Profiles
{
    public class SearchAuctionsProfile : Profile
    {
        public SearchAuctionsProfile()
        {
            // I'm pretty sure we're gonna use all properties so we don't need to mapp properties individually
            CreateMap<Auction, SearchAuctionDTO>();
        }
    }
}
