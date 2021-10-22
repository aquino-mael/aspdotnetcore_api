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
  public class Return_BadRequest
  {
    private UsersController _controller;

    [Fact(DisplayName = "Verify if get all throws a bad request.")]
    public async Task CanRequestGetAllAndThrowBadRequest()
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
      _controller.ModelState.AddModelError("Id", "Formato inválido.");

      var result = await _controller.GetAll();
      Assert.True(result is BadRequestObjectResult);
    }
  }
}
