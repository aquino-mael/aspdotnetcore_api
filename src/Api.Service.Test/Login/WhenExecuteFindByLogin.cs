using System;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Login
{
  public class WhenExecuteFindByLogin
  {
    private ILoginService _service;
    private Mock<ILoginService> _serviceMock;

    [Fact(DisplayName = "When execute find by login method.")]
    public async Task CanExecuteFindByLoginAsync()
    {
      var email = Faker.Internet.Email();
      var objetoRetorno = new
      {
        authenticated = true,
        created = DateTime.UtcNow,
        expiration = DateTime.UtcNow,
        accessToken = Guid.NewGuid(),
        username = email,
        message = "Usuário logado com sucesso.",
      };

      LoginDto loginDto = new LoginDto { Email = email };

      _serviceMock = new Mock<ILoginService>();
      _serviceMock.Setup(m => m.FindByLogin(loginDto)).ReturnsAsync(objetoRetorno);
      _service = _serviceMock.Object;

      var result = await _service.FindByLogin(loginDto);
      Assert.NotNull(result);
      Assert.Equal(result, objetoRetorno);
    }
  }
}
