using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.WhenRequestGetAll
{
  public class Return_GetAll
  {
    private UsersController _controller;

    [Fact(DisplayName = "Can Request get all.")]
    public async Task CanRequestGetAll()
    {
      var _serviceMock = new Mock<IUserService>();

      _serviceMock.Setup(x => x.GetAll()).ReturnsAsync(new List<UserDto> {
          new UserDto {
              Id = Guid.NewGuid(),
              Name = Faker.Name.FullName(),
              Email = Faker.Internet.Email(),
              CreatedAt = DateTime.UtcNow,
          },
          new UserDto {
              Id = Guid.NewGuid(),
              Name = Faker.Name.FullName(),
              Email = Faker.Internet.Email(),
              CreatedAt = DateTime.UtcNow,
          }
      });

      _controller = new UsersController(_serviceMock.Object);

      var result = await _controller.GetAll();
      Assert.True(result is OkObjectResult);

      var resultValues = ((OkObjectResult)result).Value as IEnumerable<UserDto>;
      Assert.True(resultValues.Count() == 2);
    }
  }
}
