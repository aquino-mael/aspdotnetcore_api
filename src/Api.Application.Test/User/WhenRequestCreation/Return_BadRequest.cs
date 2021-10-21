using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.WhenRequestCreation
{
  public class Return_BadRequest
  {
    private UsersController _controller;
    [Fact(DisplayName = "When execute controller post request return bad request.")]
    public async Task CanExecuteControllerPostRequestAndReturnBadRequest()
    {
      var _serviceMock = new Mock<IUserService>();
      string name = Faker.Name.FullName();
      string email = Faker.Internet.Email();

      _serviceMock.Setup(m => m.Post(It.IsAny<UserDtoCreate>())).ReturnsAsync(new UserDtoCreateResult
      {
        Id = Guid.NewGuid(),
        Name = name,
        Email = email,
        CreatedAt = DateTime.UtcNow,
      });

      _controller = new UsersController(_serviceMock.Object);
      _controller.ModelState.AddModelError("Name", "É um campo obrigatório.");

      Mock<IUrlHelper> _urlHelper = new Mock<IUrlHelper>();
      _urlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");

      _controller.Url = _urlHelper.Object;

      var userDtoCreate = new UserDtoCreate
      {
        Email = email,
        Name = name,
      };

      var result = await _controller.Post(userDtoCreate);
      Assert.True(result is BadRequestObjectResult);
    }
  }
}
