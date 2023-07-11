using AutoMapper;
using JwtAuth.BLL.DTOs.Requests;
using JwtAuth.BLL.DTOs.Responses;
using JwtAuth.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region User
            CreateMap<UserRequestDto, User>()
                .ForMember(destinationObject => destinationObject.UserId, options => options.MapFrom(sourceObject => sourceObject.Id));


            CreateMap<User, UserResponseDto>()
                .ForMember(destinationObject => destinationObject.Id, options => options.MapFrom(sourceObject => sourceObject.UserId));
            #endregion
        }
    }
}
