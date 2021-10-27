using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.ZipCode;
using Moq;
using Xunit;

namespace Api.Service.Test.ZipCode
{
    public class WhenExecutePut : ZipCodeTest
    {
        public IZipCodeService _service;
        public Mock<IZipCodeService> _serviceMock;

        [Fact(DisplayName = "Can execute PUT on zip code service.")]
        public async Task WhenExecutePutOnZipCodeService()
        {
            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(zipCodeService => zipCodeService.Put(zipCodeDtoUpdate)).ReturnsAsync(zipCodeDtoUpdateResult);
            _service = _serviceMock.Object;

            var _updateResult = await _service.Put(zipCodeDtoUpdate);
            Assert.NotNull(_updateResult);
            Assert.NotNull(_updateResult.UpdatedAt);
            Assert.Equal(zipCodeDtoUpdate.Id, _updateResult.Id);
            Assert.Equal(zipCodeDtoUpdate.Number, _updateResult.Number);
            Assert.Equal(zipCodeDtoUpdate.Street, _updateResult.Street);
            Assert.Equal(zipCodeDtoUpdate.ZipCode, _updateResult.ZipCode);
            Assert.Equal(zipCodeDtoUpdate.CountyId, _updateResult.CountyId);
        }
    }
}
