using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.County;
using Moq;
using Xunit;

namespace Api.Service.Test.County
{
    public class WhenExecuteDelete : CountyTest
    {
        public ICountyService _service;
        public Mock<ICountyService> _serviceMock;

        [Fact(DisplayName = "Can execute delete on county service.")]
        public async Task WhenExecuteDeleteOnCountyService()
        {
            _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(countyService => countyService.Delete(Id)).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var _deletedResult = await _service.Delete(Id);
            Assert.NotNull(_deletedResult);
            Assert.True(_deletedResult);
        }
    }
}
