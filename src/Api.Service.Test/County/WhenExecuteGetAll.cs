using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.County;
using Moq;
using Xunit;

namespace Api.Service.Test.County
{
    public class WhenExecuteGetAll : CountyTest
    {
        public ICountyService _service;
        public Mock<ICountyService> _serviceMock;

        [Fact(DisplayName = "Can execute get all on county service.")]
        public async Task WhenExecuteGetAllOnCountyService()
        {
            _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(countyService => countyService.GetAll()).ReturnsAsync(_countyDtoCompleteList);
            _service = _serviceMock.Object;

            var _getAllResult = await _service.GetAll();
            Assert.NotNull(_getAllResult);
            Assert.NotEmpty(_getAllResult);

            _countyDtoCompleteList.Clear();

            _getAllResult = await _service.GetAll();
            Assert.NotNull(_getAllResult);
            Assert.Empty(_getAllResult);
        }
    }
}
