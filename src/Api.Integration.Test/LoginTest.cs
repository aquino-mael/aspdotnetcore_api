using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test
{
  public class LoginTest : BaseIntegration
  {
    [Fact(DisplayName = "Veriy if token is working.")]
    public async Task TokenTest()
    {
      await AddToken();
    }
  }
}
