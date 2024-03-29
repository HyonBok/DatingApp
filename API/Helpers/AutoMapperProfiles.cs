using API.Commands;
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
            CreateMap<Foto, FotoDto>();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<Produto, ProdutoDto>()
                .ForMember(dest => dest.FotoUrl, opt => opt.MapFrom
                    (src => src.Fotos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom
                    (src => src.AppUser.UserName));
            CreateMap<ProdutoDto, Produto>();
            CreateMap<ProdutoAtualizarRequest, Produto>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<Message, MessageDto>()
                .ForMember(d => d.SenderPhotoUrl, o => o.MapFrom(
                    s => s.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.RecipientPhotoUrl, o => o.MapFrom(
                    s => s.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}