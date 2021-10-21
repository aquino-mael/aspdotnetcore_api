using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
  public class UserMapper : BaseTestService
  {
    [Fact(DisplayName = "Transfer data from a model to an Entity")]
    public void CanExecuteMapperFromModelToEntity()
    {
      var model = new UserModel
      {
        Id = Guid.NewGuid(),
        Name = Faker.Name.FullName(),
        Email = Faker.Internet.Email(),
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
      };

      var entity = Mapper.Map<UserEntity>(model);
      Assert.Equal(entity.Id, model.Id);
      Assert.Equal(entity.Name, model.Name);
      Assert.Equal(entity.Email, model.Email);
      Assert.Equal(entity.CreatedAt, model.CreatedAt);
      Assert.Equal(entity.UpdatedAt, model.UpdatedAt);

      var userDto = Mapper.Map<UserDto>(entity);
      Assert.Equal(userDto.Id, entity.Id);
      Assert.Equal(userDto.Name, entity.Name);
      Assert.Equal(userDto.Email, entity.Email);
      Assert.Equal(userDto.CreatedAt, entity.CreatedAt);

      var entitiesList = new List<UserEntity>();
      for (int i = 0; i < 5; i++)
      {
        var item = new UserEntity
        {
          Id = Guid.NewGuid(),
          Name = Faker.Name.FullName(),
          Email = Faker.Internet.Email(),
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow,
        };

        entitiesList.Add(item);
      }

      var dtoList = Mapper.Map<List<UserDto>>(entitiesList);
      Assert.True(dtoList.Count() == entitiesList.Count());

      for (int i = 0; i < dtoList.Count(); i++)
      {
        Assert.Equal(dtoList[i].Id, entitiesList[i].Id);
        Assert.Equal(dtoList[i].Name, entitiesList[i].Name);
        Assert.Equal(dtoList[i].Email, entitiesList[i].Email);
        Assert.Equal(dtoList[i].CreatedAt, entitiesList[i].CreatedAt);
      }

      var userDtoCreateResult = Mapper.Map<UserDtoCreateResult>(entity);
      Assert.Equal(userDtoCreateResult.Id, entity.Id);
      Assert.Equal(userDtoCreateResult.Name, entity.Name);
      Assert.Equal(userDtoCreateResult.Email, entity.Email);
      Assert.Equal(userDtoCreateResult.CreatedAt, entity.CreatedAt);

      var userDtoUpdateResult = Mapper.Map<UserDtoUpdateResult>(entity);
      Assert.Equal(userDtoUpdateResult.Id, entity.Id);
      Assert.Equal(userDtoUpdateResult.Name, entity.Name);
      Assert.Equal(userDtoUpdateResult.Email, entity.Email);
      Assert.Equal(userDtoUpdateResult.UpdatedAt, entity.UpdatedAt);

      var userModel = Mapper.Map<UserModel>(userDto);
      Assert.Equal(userModel.Id, userDto.Id);
      Assert.Equal(userModel.Name, userDto.Name);
      Assert.Equal(userModel.Email, userDto.Email);
      Assert.Equal(userModel.CreatedAt, userDto.CreatedAt);

      var userDtoCreate = Mapper.Map<UserDtoCreate>(userModel);
      Assert.Equal(userDtoCreate.Name, userModel.Name);
      Assert.Equal(userDtoCreate.Email, userModel.Email);

      var userDtoUpdate = Mapper.Map<UserDtoUpdate>(userModel);
      Assert.Equal(userDtoUpdate.Id, userModel.Id);
      Assert.Equal(userDtoUpdate.Name, userModel.Name);
      Assert.Equal(userDtoUpdate.Email, userModel.Email);

    }
  }
}
