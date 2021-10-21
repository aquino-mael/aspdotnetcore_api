using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
  public class WhenExecuteGetAll : UserTest
  {
    private IUserService _service;

    private Mock<IUserService> _serviceMock;

    [Fact(DisplayName = "When execute get all method.")]
    public async Task CanExecuteGetAll()
    {
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(userDtoList);
      _service = _serviceMock.Object;

      var _result = await _service.GetAll();
      Assert.NotNull(_result);
      Assert.True(_result.Count() == 10);

      var _listResult = new List<UserDto>();
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(_listResult.AsEnumerable);
      _service = _serviceMock.Object;

      var _resultEmpty = await _service.GetAll();
      Assert.Empty(_resultEmpty);
      Assert.True(_resultEmpty.Count() == 0);
    }
  }
}
