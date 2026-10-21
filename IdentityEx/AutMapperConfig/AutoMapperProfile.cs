using AutoMapper;
using IdentityEx.Dtos;
using IdentityEx.Models;

namespace IdentityEx.AutMapperConfig
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, User>().ForMember(p=>p.UserName,q=>q.MapFrom(p=>p.Email))
                .ReverseMap();

            CreateMap<Product, ProductDto>().ForMember(p=>p.CategoryName,q=>q.MapFrom(p=>p.Category.CategoryName)).ReverseMap();
            
        }
    }
}
