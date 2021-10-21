using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
  public class WhenExecutePut : UserTest
  {
    private IUserService _service;
    private Mock<IUserService> _serviceMock;
    [Fact(DisplayName = "When execute put method.")]
    public async Task CanExecutePut()
    {
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Put(userDtoUpdate)).ReturnsAsync(userDtoUpdateResult);
      _service = _serviceMock.Object;

      var _resultUpdate = await _service.Put(userDtoUpdate);
      Assert.NotNull(_resultUpdate);
      Assert.Equal(AlteredUserName, _resultUpdate.Name);
      Assert.Equal(AlteredUserEmail, _resultUpdate.Email);
    }
  }
}
