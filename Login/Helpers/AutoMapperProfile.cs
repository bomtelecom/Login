using AutoMapper;
using Login.Entities;
using Login.Models;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, AuthenticateResponse>();
            CreateMap<RegisterRequest, User>();        }
    }
}