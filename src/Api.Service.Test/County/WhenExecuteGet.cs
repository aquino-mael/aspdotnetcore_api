using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.County;
using Api.Domain.Interfaces.Services.County;
using Moq;
using Xunit;

namespace Api.Service.Test.County
{
    public class WhenExecuteGet : CountyTest
    {
        public ICountyService _service;
        public Mock<ICountyService> _serviceMock;

        [Fact(DisplayName = "Can execute GET on County service.")]
        public async Task WhenExecuteGetOnCountyService()
        {
            _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(countyService => countyService.Get(Id)).ReturnsAsync(countyDto);
            _service = _serviceMock.Object;

            var _getResult = await _service.Get(Id);
            Assert.NotNull(_getResult);
            Assert.Equal(Id, _getResult.Id);
            Assert.Equal(Name, _getResult.Name);
            Assert.Equal(UfId, _getResult.UfId);
            Assert.Equal(IBGECode, _getResult.IBGECode);

            _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(countyService => countyService.Get(It.IsAny<Guid>())).ReturnsAsync((CountyDto)null);
            _service = _serviceMock.Object;

            _getResult = await _service.Get(Guid.NewGuid());
            Assert.Null(_getResult);

            // GET COMPLETE BY ID

            _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(countyService => countyService.GetCompleteById(Id)).ReturnsAsync(countyDtoComplete);
            _service = _serviceMock.Object;

            var _getCompleteResult = await _service.GetCompleteById(Id);
            Assert.NotNull(_getCompleteResult);
            Assert.Equal(Id, _getCompleteResult.Id);
            Assert.NotNull(_getCompleteResult.Uf);

            _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(countyService => countyService.GetCompleteById(It.IsAny<Guid>())).ReturnsAsync((CountyDtoComplete)null);
            _service = _serviceMock.Object;

            _getCompleteResult = await _service.GetCompleteById(Guid.NewGuid());
            Assert.Null(_getCompleteResult);

            _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(countyService => countyService.GetCompleteById(Id)).ReturnsAsync(countyDtoComplete);
            _service = _serviceMock.Object;

            // GET COMPLETE BY IBGE

            _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(countyService => countyService.GetCompleteByIBGE(IBGECode)).ReturnsAsync(countyDtoComplete);
            _service = _serviceMock.Object;

            var _getCompleteIBGEResult = await _service.GetCompleteByIBGE(IBGECode);
            Assert.NotNull(_getCompleteIBGEResult);
            Assert.Equal(Id, _getCompleteIBGEResult.Id);
            Assert.NotNull(_getCompleteIBGEResult.Uf);

            _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(countyService => countyService.GetCompleteByIBGE(It.IsAny<int>())).ReturnsAsync((CountyDtoComplete)null);
            _service = _serviceMock.Object;

            _getCompleteIBGEResult = await _service.GetCompleteByIBGE(1);
            Assert.Null(_getCompleteIBGEResult);
        }
    }
}
