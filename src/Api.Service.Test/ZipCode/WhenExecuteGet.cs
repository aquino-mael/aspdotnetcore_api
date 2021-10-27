using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.ZipCode;
using Moq;
using Xunit;

namespace Api.Service.Test.ZipCode
{
    public class WhenExecuteGet : ZipCodeTest
    {
        public IZipCodeService _service;
        public Mock<IZipCodeService> _serviceMock;

        [Fact(DisplayName = "Can execute Get on zip code service.")]
        public async Task WhenExecuteGetOnCountyService()
        {
            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(zipCodeService => zipCodeService.Get(Id)).ReturnsAsync(zipCodeDto);
            _service = _serviceMock.Object;

            var _getByIdResult = await _service.Get(Id);
            Assert.NotNull(_getByIdResult);
            Assert.Equal(Id, zipCodeDto.Id);
            Assert.Equal(Number, zipCodeDto.Number);
            Assert.Equal(ZipCode, zipCodeDto.ZipCode);
            Assert.Equal(CountyId, zipCodeDto.CountyId);
            Assert.NotNull(zipCodeDto.County);
            Assert.NotNull(zipCodeDto.County.Uf);

            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(zipCodeService => zipCodeService.Get(ZipCode)).ReturnsAsync(zipCodeDto);
            _service = _serviceMock.Object;

            var _getByZipCodeResult = await _service.Get(ZipCode);
            Assert.NotNull(_getByIdResult);
            Assert.Equal(Id, zipCodeDto.Id);
            Assert.Equal(Number, zipCodeDto.Number);
            Assert.Equal(ZipCode, zipCodeDto.ZipCode);
            Assert.Equal(CountyId, zipCodeDto.CountyId);
            Assert.NotNull(zipCodeDto.County);
            Assert.NotNull(zipCodeDto.County.Uf);
        }
    }
}
