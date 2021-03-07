using AutoMapper;
using ci_cd.DTOs;
using ci_cd.Models;

namespace ci_cd.Utils
{
  class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<WishlistGameDto, WishlistGameModel>();
    }
  }
}
