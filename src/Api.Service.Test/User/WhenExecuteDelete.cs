using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
  public class WhenExecuteDelete : UserTest
  {
    private IUserService _service;
    private Mock<IUserService> _serviceMock;

    [Fact(DisplayName = "When execute delete method.")]
    public async Task CanExcuteDelete()
    {
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Delete(UserId)).ReturnsAsync(true);
      _service = _serviceMock.Object;

      var _excludeResult = await _service.Delete(UserId);
      Assert.True(_excludeResult);

      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);
      _service = _serviceMock.Object;

      _excludeResult = await _service.Delete(UserId);
      Assert.False(_excludeResult);
    }
  }
}
