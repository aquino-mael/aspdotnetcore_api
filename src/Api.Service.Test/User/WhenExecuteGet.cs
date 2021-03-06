using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
  public class WhenExecuteGet : UserTest
  {
    private IUserService _service;
    private Mock<IUserService> _serviceMock;
    [Fact(DisplayName = "When execute get method.")]
    public async Task CanExecuteGetMethod()
    {
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Get(UserId)).ReturnsAsync(userDto);
      _service = _serviceMock.Object;

      var result = await _service.Get(UserId);
      Assert.NotNull(result);
      Assert.True(result.Id == UserId);
      Assert.Equal(UserName, result.Name);

      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(Task.FromResult((UserDto)null));
      _service = _serviceMock.Object;

      var _record = await _service.Get(UserId);
      Assert.Null(_record);
    }
  }
}
