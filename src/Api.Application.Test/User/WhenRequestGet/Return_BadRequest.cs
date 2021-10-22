using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.WhenRequestGet
{
  public class Return_BadRequest
  {
    private UsersController _controller;

    [Fact(DisplayName = "Request get with id and return BadRequest")]
    public async Task CanRequestGet()
    {
      var _serviceMock = new Mock<IUserService>();
      var name = Faker.Name.FullName();
      var email = Faker.Internet.Email();

      _serviceMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(
          new UserDto
          {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            CreatedAt = DateTime.UtcNow,
          }
      );

      _controller = new UsersController(_serviceMock.Object);
      _controller.ModelState.AddModelError("Id", "Formato inválido.");

      var result = await _controller.Get(Guid.NewGuid());
      Assert.True(result is BadRequestObjectResult);
    }
  }
}
