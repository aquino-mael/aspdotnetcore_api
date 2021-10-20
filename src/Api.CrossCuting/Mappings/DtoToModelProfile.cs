using Api.Domain.Dtos.User;
using Api.Domain.models;
using AutoMapper;

namespace Api.CrossCuting.Mappings
{
  public class DtoToModelProfile : Profile
  {
    public DtoToModelProfile()
    {
      CreateMap<UserModel, UserDto>().ReverseMap();
    }
  }
}
