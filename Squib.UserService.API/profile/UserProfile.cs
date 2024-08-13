using AutoMapper;
using Squib.UserService.API.model;
using Squib.UserService.API.Model;

namespace Squib.UserService.API.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, UserRDto>()
                .ForMember(dest => dest.FullName, 
                           opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
