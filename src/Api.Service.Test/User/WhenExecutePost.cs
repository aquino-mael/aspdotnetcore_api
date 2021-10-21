using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
  public class WhenExecutePost : UserTest
  {
    private IUserService _service;

    private Mock<IUserService> _serviceMock;

    [Fact(DisplayName = "When execute post method.")]
    public async Task CanExecutePostMethod()
    {
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Post(userDtoCreate)).ReturnsAsync(userDtoCreateResult);
      _service = _serviceMock.Object;

      var _resultCreate = await _service.Post(userDtoCreate);
      Assert.NotNull(_resultCreate);
      Assert.Equal(UserName, _resultCreate.Name);
      Assert.Equal(UserEmail, _resultCreate.Email);
    }
  }
}
