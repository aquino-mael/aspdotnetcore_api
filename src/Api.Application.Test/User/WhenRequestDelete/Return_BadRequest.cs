using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.WhenRequestDelete
{
  public class Return_BadRequest
  {
    private UsersController _controller;
    [Fact(DisplayName = "When request Delete.")]
    public async Task CanRequestDelete()
    {
      var _serviceMock = new Mock<IUserService>();

      _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);

      _controller = new UsersController(_serviceMock.Object);
      _controller.ModelState.AddModelError("Id", "Formato inválido.");

      var result = await _controller.Delete(default(Guid));
      Assert.True(result is BadRequestObjectResult);
      Assert.False(_controller.ModelState.IsValid);
    }
  }
}
