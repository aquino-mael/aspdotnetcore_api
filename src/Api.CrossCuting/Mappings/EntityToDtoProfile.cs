using Api.Domain.Dtos.County;
using Api.Domain.Dtos.Uf;
using Api.Domain.Dtos.User;
using Api.Domain.Dtos.ZipCode;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCuting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            #region User
            CreateMap<UserDto, UserEntity>()
              .ReverseMap();
            CreateMap<UserDtoCreateResult, UserEntity>()
              .ReverseMap();
            CreateMap<UserDtoUpdateResult, UserEntity>()
              .ReverseMap();
            #endregion

            #region Uf
            CreateMap<UfDto, UfEntity>()
              .ReverseMap();
            #endregion

            #region County
            CreateMap<CountyDto, CountyEntity>()
              .ReverseMap();
            CreateMap<CountyDtoComplete, CountyEntity>()
              .ReverseMap();
            CreateMap<CountyDtoCreateResult, CountyEntity>()
              .ReverseMap();
            CreateMap<CountyDtoUpdateResult, CountyEntity>()
              .ReverseMap();
            #endregion

            #region ZipCode
            CreateMap<ZipCodeDto, ZipCodeEntity>()
              .ReverseMap();
            CreateMap<ZipCodeDtoCreateResult, ZipCodeEntity>()
              .ReverseMap();
            CreateMap<ZipCodeDtoUpdateResult, ZipCodeEntity>()
              .ReverseMap();
            #endregion
        }
    }
}
