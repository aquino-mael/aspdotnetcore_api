using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.ZipCode;
using Moq;
using Xunit;

namespace Api.Service.Test.ZipCode
{
    public class WhenExecuteDelete : ZipCodeTest
    {
        public IZipCodeService _service;
        public Mock<IZipCodeService> _serviceMock;

        [Fact(DisplayName = "Can execute delete on a zip code service.")]
        public async Task CanExecuteDeleteOnZipCodeService()
        {
            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(zipCodeService => zipCodeService.Delete(Id)).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var _deletedResult = await _service.Delete(Id);
            Assert.NotNull(_deletedResult);
            Assert.True(_deletedResult);
        }
    }
}
