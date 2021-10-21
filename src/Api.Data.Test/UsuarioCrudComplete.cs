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

        var _createRegister = await _repository.InsertAsync(_entity);
        Assert.NotNull(_createRegister);
        Assert.Equal(_entity.Email, _createRegister.Email);
        Assert.Equal(_entity.Name, _createRegister.Name);
        Assert.False(_createRegister.Id == Guid.Empty);

        _entity.Name = Faker.Name.First();
        var _registerUpdate = await _repository.UpdateAsync(_entity);
        Assert.NotNull(_registerUpdate);
        Assert.Equal(_entity.Email, _registerUpdate.Email);
        Assert.Equal(_entity.Name, _registerUpdate.Name);

        var _existRegister = await _repository.ExistsAsync(_entity.Id);
        Assert.True(_existRegister);

        var _selectedRegister = await _repository.SelectAsync(_entity.Id);
        Assert.NotNull(_existRegister);
        Assert.Equal(_entity.Email, _selectedRegister.Email);
        Assert.Equal(_entity.Name, _selectedRegister.Name);

        var _allRegisters = await _repository.SelectAsync();
        Assert.NotNull(_allRegisters);
        Assert.NotEmpty(_allRegisters);

        var _isRemovedRegister = await _repository.DeleteAsync(_selectedRegister.Id);
        Assert.True(_isRemovedRegister);

        var _defaultUser = await _repository.FindByLogin("admin@hotmail.com");
        Assert.NotNull(_defaultUser);
        Assert.Equal("admin@hotmail.com", _defaultUser.Email);
        Assert.Equal("Administrador", _defaultUser.Name);
      }
    }
  }
}
