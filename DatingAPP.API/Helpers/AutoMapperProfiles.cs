using System.Linq;
using AutoMapper;
using DatingAPP.API.Dtos;
using DatingAPP.API.Models;
using DatingAPP.API.Modules;
namespace DatingAPP.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember( destination => destination.PhotoUrl, option =>  {
                    option.MapFrom( source => source.Photos.FirstOrDefault( p => p.IsMain).Url); 
                })
                .ForMember(destination => destination.Age, option => {
                    option.ResolveUsing( d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<User, UserForDetailedDto>()
                .ForMember( destination => destination.PhotoUrl, option =>  {
                    option.MapFrom( source => source.Photos.FirstOrDefault( p => p.IsMain).Url); 
                })
                .ForMember(destination => destination.Age, option => {
                    option.ResolveUsing( d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>()
                .ForMember( m => m.SenderPhotoUrl, 
                    opt => opt.MapFrom(u => u.Sender.Photos.FirstOrDefault( p => p.IsMain).Url))
                .ForMember( m => m.RecipientPhotoUrl, 
                    opt => opt.MapFrom(u => u.Recipient.Photos.FirstOrDefault( p => p.IsMain).Url));
        }
    }
}