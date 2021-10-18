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

    public UserService(IRepository<UserEntity> repository)
    {
      _repository = repository;
    }

    Task<bool> IUserService.Delete(Guid id)
    {
      throw new NotImplementedException();
    }

    Task<UserEntity> IUserService.Get(Guid id)
    {
      throw new NotImplementedException();
    }

    Task<IEnumerable<UserEntity>> IUserService.GetAll()
    {
      throw new NotImplementedException();
    }

    Task<UserEntity> IUserService.Post(UserEntity entity)
    {
      throw new NotImplementedException();
    }

    Task<UserEntity> IUserService.Put(UserEntity entity)
    {
      throw new NotImplementedException();
    }
  }
}
