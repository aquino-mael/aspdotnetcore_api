using System;
using System.Collections.Generic;
using Api.Domain.Dtos.User;

namespace Api.Service.Test.User
{
  public class UserTest
  {
    public static string UserName { get; set; }
    public static string UserEmail { get; set; }
    public static string AlteredUserName { get; set; }
    public static string AlteredUserEmail { get; set; }
    public static Guid UserId { get; set; }
    public List<UserDto> userDtoList = new List<UserDto>();
    public UserDto userDto;
    public UserDtoCreate userDtoCreate;
    public UserDtoCreateResult userDtoCreateResult;
    public UserDtoUpdate userDtoUpdate;
    public UserDtoUpdateResult userDtoUpdateResult;

    public UserTest()
    {
      UserId = Guid.NewGuid();
      UserName = Faker.Name.FullName();
      UserEmail = Faker.Internet.Email();
      AlteredUserName = Faker.Name.FullName();
      AlteredUserEmail = Faker.Internet.Email();

      for (int i = 0; i < 10; i++)
      {
        UserDto dto = new UserDto
        {
          Id = Guid.NewGuid(),
          Name = Faker.Name.FullName(),
          Email = Faker.Internet.Email(),
        };

        userDtoList[i] = dto;
      };

      userDto = new UserDto
      {
        Id = UserId,
        Name = UserName,
        Email = UserEmail,
      };

      userDtoCreate = new UserDtoCreate
      {
        Email = UserEmail,
        Name = UserName,
      };

      userDtoCreateResult = new UserDtoCreateResult
      {
        Id = UserId,
        Name = UserName,
        Email = UserEmail,
        CreatedAt = DateTime.UtcNow,
      };

      userDtoUpdate = new UserDtoUpdate
      {
        Id = UserId,
        Name = UserName,
        Email = UserEmail,
      };

      userDtoUpdateResult = new UserDtoUpdateResult
      {
        Id = UserId,
        Name = UserName,
        Email = UserEmail,
        UpdatedAt = DateTime.UtcNow,
      };
    }
  }
}
