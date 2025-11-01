using AutoMapper;
using Sozeris.Server.Domain.Entities;
using Sozeris.Server.Api.DTO;
using Sozeris.Server.Api.DTO.User;

namespace Sozeris.Server.Api.AutoMapping;

public class UserProfile : Profile
{    
    public UserProfile()
    {
        CreateMap<User, UserResponseDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();
    }
}