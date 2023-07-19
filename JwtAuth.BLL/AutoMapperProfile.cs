using AutoMapper;
using JwtAuth.BLL.DTOs.Requests;
using JwtAuth.BLL.DTOs.Responses;
using JwtAuth.DAL.Entities;

namespace JwtAuth.BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region User
            CreateMap<UserRegistrationRequestDto, User>();

            CreateMap<User, UserResponseDto>()
                .ForMember(destinationObject => destinationObject.Id, options => options.MapFrom(sourceObject => sourceObject.UserId));
            #endregion
        }
    }
}