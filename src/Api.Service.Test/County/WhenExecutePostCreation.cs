using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.County;
using Moq;
using Xunit;

namespace Api.Service.Test.County
{
    public class WhenExecutePostCreation : CountyTest
    {
        public ICountyService _service;
        public Mock<ICountyService> _serviceMock;

        [Fact(DisplayName = "Can execute POST on County service to create a new County.")]
        public async Task WhenExecutePostCreationOnCountyService()
        {
            _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(countyService => countyService.Post(createDtoCounty)).ReturnsAsync(createDtoCountyResult);
            _service = _serviceMock.Object;

            var _postResult = await _service.Post(createDtoCounty);
            Assert.NotNull(_postResult);
            Assert.NotNull(_postResult.CreatedAt);
            Assert.NotNull(_postResult.Id);
            Assert.Equal(createDtoCounty.Name, _postResult.Name);
            Assert.Equal(createDtoCounty.UfId, _postResult.UfId);
            Assert.Equal(createDtoCounty.IBGECode, _postResult.IBGECode);
        }
    }
}
