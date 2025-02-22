using AutoMapper;
using Sozeris.Server.Models.Entities;
using Sozeris.Server.Models.DTO;

namespace Sozeris.Server.Models.AutoMapping;

public class UserProfile : Profile
{    
    public UserProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
    }
}