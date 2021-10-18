using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;

namespace Api.Service.services
{
  public class UserService : IUserService
  {
    private IRepository<UserEntity> _repository;

    public async Task<bool> Delete(Guid id)
    {
      return await _repository.DeleteAsync(id);
    }

    public async Task<UserEntity> Get(Guid id)
    {
      return await _repository.SelectAsync(id);
    }

    public Task<IEnumerable<UserEntity>> GetAll()
    {
      throw new NotImplementedException();
    }

    public Task<UserEntity> Post(UserEntity entity)
    {
      throw new NotImplementedException();
    }

    public Task<UserEntity> Put(UserEntity entity)
    {
      throw new NotImplementedException();
    }
  }
}
