using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.County;
using Moq;
using Xunit;

namespace Api.Service.Test.County
{
    public class WhenExecutePutUpdate : CountyTest
    {
        public ICountyService _service;
        public Mock<ICountyService> _serviceMock;

        [Fact(DisplayName = "Can execute PUT on County service to update a County.")]
        public async Task WhenExecutePutUpdationOnCountyService()
        {
            _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(countyService => countyService.Put(updateDtoCounty)).ReturnsAsync(updateDtoCountyResult);
            _service = _serviceMock.Object;

            var updateResult = await _service.Put(updateDtoCounty);
            Assert.NotNull(updateResult);
            Assert.Equal(updateDtoCounty.Id, updateResult.Id);
            Assert.Equal(updateDtoCounty.Name, updateResult.Name);
            Assert.Equal(updateDtoCounty.IBGECode, updateResult.IBGECode);
        }
    }
}
