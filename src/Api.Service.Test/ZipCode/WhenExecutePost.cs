using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.ZipCode;
using Moq;
using Xunit;

namespace Api.Service.Test.ZipCode
{
    public class WhenExecutePost : ZipCodeTest
    {
        public IZipCodeService _service;
        public Mock<IZipCodeService> _serviceMock;

        [Fact(DisplayName = "")]
        public async Task WhenExecutePostOnZipCodeService()
        {
            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(zipCodeService => zipCodeService.Post(zipCodeDtoCreate)).ReturnsAsync(zipCodeDtoCreateResult);
            _service = _serviceMock.Object;

            var _postResult = await _service.Post(zipCodeDtoCreate);
            Assert.NotNull(_postResult);
            Assert.NotNull(_postResult.Id);
            Assert.NotNull(_postResult.CreatedAt);
            Assert.Equal(zipCodeDtoCreate.Number, _postResult.Number);
            Assert.Equal(zipCodeDtoCreate.CountyId, _postResult.CountyId);
            Assert.Equal(zipCodeDtoCreate.Street, _postResult.Street);
            Assert.Equal(zipCodeDtoCreate.ZipCode, _postResult.ZipCode);
        }
    }
}
