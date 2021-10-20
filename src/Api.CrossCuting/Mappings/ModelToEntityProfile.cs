using Api.Domain.Entities;
using Api.Domain.models;
using AutoMapper;

namespace Api.CrossCuting.Mappings
{
  public class ModelToEntityProfile : Profile
  {
    public ModelToEntityProfile()
    {
      CreateMap<UserModel, UserEntity>().ReverseMap();
    }
  }
}
