using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom
                    (src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom
                    (src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<Produto, ProdutoDto>()
                .ForMember(dest => dest.FotoUrl, opt => opt.MapFrom
                    (src => src.Fotos.FirstOrDefault().Url))
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom
                    (src => src.AppUser.UserName));
            CreateMap<ProdutoDto, Produto>();
            CreateMap<RegisterDto, AppUser>();
        }
    }
}