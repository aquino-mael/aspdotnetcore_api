using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
  public abstract class BaseTest
  {
    public BaseTest() { }
  }

  public class DbTest : IDisposable
  {
    private string databaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";

    public ServiceProvider ServiceProvider { get; private set; }

    public DbTest()
    {
      var serviceCollection = new ServiceCollection();
      serviceCollection.AddDbContext<MyContext>(
        o => o.UseSqlServer($"Persist Security Info=true;Server=localhost;Initial Catalog={databaseName};MultipleActiveResultSets=true;User ID=sa;Password=DockerSql2017!"),
        ServiceLifetime.Transient
      );

      ServiceProvider = serviceCollection.BuildServiceProvider();

      using (var context = ServiceProvider.GetService<MyContext>())
      {
        context.Database.EnsureCreated();
      }
    }

    public void Dispose()
    {
      using (var context = ServiceProvider.GetService<MyContext>())
      {
        context.Database.EnsureDeleted();
      }
    }
  }
}
