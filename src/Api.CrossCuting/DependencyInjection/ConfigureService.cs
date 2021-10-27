using Api.Domain.Interfaces.Services.County;
using Api.Domain.Interfaces.Services.Uf;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Interfaces.Services.ZipCode;
using Api.Service.services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCuting.DependencyInjection
{
  public class ConfigureService
  {
    public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
    {
      serviceCollection.AddTransient<IUserService, UserService>();
      serviceCollection.AddTransient<ILoginService, LoginService>();
      serviceCollection.AddTransient<IUfService, UfService>();
      serviceCollection.AddTransient<ICountyService, CountyService>();
      serviceCollection.AddTransient<IZipCodeService, ZipCodeService>();
    }
  }
}
