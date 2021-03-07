using System.Linq;
using AutoMapper;
using ci_cd.DTOs;
using ci_cd.Models;

namespace ci_cd.Utils
{
  class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<WishlistGameDto, WishlistGameModel>()
        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Subs.Select(sub => sub.Price).DefaultIfEmpty(0).First() / 100m));
    }
  }
}
