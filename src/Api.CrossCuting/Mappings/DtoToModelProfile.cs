using Api.Domain.Dtos.County;
using Api.Domain.Dtos.Uf;
using Api.Domain.Dtos.User;
using Api.Domain.Dtos.ZipCode;
using Api.Domain.models;
using AutoMapper;

namespace Api.CrossCuting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            #region User
            CreateMap<UserModel, UserDto>()
                .ReverseMap();
            CreateMap<UserModel, UserDtoCreate>()
                .ReverseMap();
            CreateMap<UserModel, UserDtoUpdate>()
                .ReverseMap();
            #endregion

            #region Uf
            CreateMap<UfModel, UfDto>()
                .ReverseMap();
            #endregion

            #region County
            CreateMap<CountyModel, CountyDto>()
                .ReverseMap();
            CreateMap<CountyModel, CountyDtoComplete>()
                .ReverseMap();
            CreateMap<CountyModel, CountyDtoCreate>()
                .ReverseMap();
            CreateMap<CountyModel, CountyDtoUpdate>()
                .ReverseMap();
            #endregion

            #region ZipCode
            CreateMap<ZipCodeModel, ZipCodeDto>()
                .ReverseMap();
            CreateMap<ZipCodeModel, ZipCodeDtoCreate>()
                .ReverseMap();
            CreateMap<ZipCodeModel, ZipCodeDtoUpdate>()
                .ReverseMap();
            #endregion
        }
    }
}
