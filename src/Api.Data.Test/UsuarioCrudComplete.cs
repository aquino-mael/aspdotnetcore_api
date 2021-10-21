using System;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
  public class UsuarioCrudComplete : BaseTest, IClassFixture<DbTest>
  {
    private ServiceProvider _serviceProvide;

    public UsuarioCrudComplete(DbTest dbTeste)
    {
      _serviceProvide = dbTeste.ServiceProvider;
    }

    [Fact(DisplayName = "CRUD de usuários.")]
    [Trait("CRUD", "UserEntity")]
    public async Task E_Possivel_Realizar_Crud_Usuario()
    {
      using (var context = _serviceProvide.GetService<MyContext>())
      {
        UserImplementation _repository = new UserImplementation(context);
        UserEntity _entity = new UserEntity
        {
          Email = Faker.Internet.Email(),
          Name = Faker.Name.FullName(),
        };

        var _registroCriado = await _repository.InsertAsync(_entity);
        Assert.NotNull(_registroCriado);
        Assert.Equal(_entity.Email, _registroCriado.Email);
        Assert.Equal(_entity.Name, _registroCriado.Name);
        Assert.False(_registroCriado.Id == Guid.Empty);
      }
    }
  }
}
