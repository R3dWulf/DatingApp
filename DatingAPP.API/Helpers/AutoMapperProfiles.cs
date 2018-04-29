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
        }
    }
}