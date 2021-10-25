using System.Net.Http;
using Api.Data.Context;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using System;
using Api.CrossCuting.Mappings;
using Api.Domain.Dtos;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Api.Integration.Test
{
  public class BaseIntegration : IDisposable
  {
    public MyContext myContext { get; private set; }
    public HttpClient client { get; private set; }

    public IMapper mapper { get; set; }

    public string hostApi { get; set; }

    public HttpResponseMessage response { get; set; }

    public BaseIntegration()
    {
      hostApi = "http://localhost:5000/api/";

      var builder = new WebHostBuilder()
                    .UseEnvironment("Testing")
                    .UseStartup<Startup>();

      var server = new TestServer(builder);

      myContext = server.Host.Services.GetService(typeof(MyContext)) as MyContext;
      myContext.Database.Migrate();

      mapper = new AutoMapperFixture().GetMapper();

      client = server.CreateClient();
    }

    public async Task AddToken()
    {
      var loginDto = new LoginDto
      {
        Email = "admin@hotmail.com",
      };

      var resultLogin = await PostJsonAsync(loginDto, $"{hostApi}login", client);
      var jsonLogin = await resultLogin.Content.ReadAsStringAsync();
      var loginObject = JsonConvert.DeserializeObject<LoginResponseDto>(jsonLogin);

      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        "Bearer",
        loginObject.accessToken
      );
    }

    public static async Task<HttpResponseMessage> PostJsonAsync(
      object data,
      string url,
      HttpClient client
    )
    {
      return await client.PostAsync(url,
        new StringContent(
          JsonConvert.SerializeObject(data),
          System.Text.Encoding.UTF8,
          "application/json"
        )
      );
    }

    public void Dispose()
    {
      myContext.Dispose();
      client.Dispose();
    }
  }
  public class AutoMapperFixture : IDisposable
  {
    public IMapper GetMapper()
    {
      var config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile(new ModelToEntityProfile());
        cfg.AddProfile(new EntityToDtoProfile());
        cfg.AddProfile(new DtoToModelProfile());
      });

      return config.CreateMapper();
    }

    public void Dispose() { }
  }
}
