using System;
using Api.CrossCuting.Mappings;
using AutoMapper;
using Xunit;

namespace Api.Service.Test
{
  public class BaseTestService
  {
    public IMapper Mapper { get; set; }
    public BaseTestService()
    {
      Mapper = new AutoMapperFixture().GetMapper();
    }

    public class AutoMapperFixture : IDisposable
    {
      public void Dispose() { }

      public IMapper GetMapper()
      {
        var config = new MapperConfiguration(cfg =>
        {
          cfg.AddProfile(new ModelToEntityProfile());
          cfg.AddProfile(new DtoToModelProfile());
          cfg.AddProfile(new EntityToDtoProfile());
        });

        return config.CreateMapper();
      }
    }
  }
}
