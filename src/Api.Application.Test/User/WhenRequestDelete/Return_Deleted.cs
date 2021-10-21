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
  public class Return_Deleted
  {
    private UsersController _controller;
    [Fact(DisplayName = "When request update.")]
    public async Task CanRequestUpdate()
    {
      var _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

      _controller = new UsersController(_serviceMock.Object);

      var result = await _controller.Delete(Guid.NewGuid());
      Assert.True(result is OkObjectResult);

      var returnedValue = ((OkObjectResult)result).Value;
      Assert.NotNull(returnedValue);

      var resultValue = ((OkObjectResult)result).Value;
      Assert.NotNull(resultValue);
      Assert.True((bool)resultValue);
    }
  }
}
