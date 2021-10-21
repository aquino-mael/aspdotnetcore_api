using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.WhenRequestUpdate
{
  public class Return_BadRequest
  {
    private UsersController _controller;
    [Fact(DisplayName = "When request update.")]
    public async Task CanRequestUpdate()
    {
      var _serviceMock = new Mock<IUserService>();
      var name = Faker.Name.FullName();
      var email = Faker.Internet.Email();

      _serviceMock.Setup(m => m.Put(It.IsAny<UserDtoUpdate>())).ReturnsAsync(
          new UserDtoUpdateResult
          {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            UpdatedAt = DateTime.UtcNow,
          }
      );

      _controller = new UsersController(_serviceMock.Object);
      _controller.ModelState.AddModelError("Id", "É um campo obrigatório.");

      Mock<IUrlHelper> _urlHelper = new Mock<IUrlHelper>();
      _urlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");

      _controller.Url = _urlHelper.Object;

      var userDtoUpdate = new UserDtoUpdate
      {
        Id = Guid.NewGuid(),
        Email = email,
        Name = name,
      };

      var result = await _controller.Put(userDtoUpdate);
      Assert.True(result is BadRequestObjectResult);
    }
  }
}
