using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Moq;
using Xunit;

namespace Api.Service.Test.Uf
{
    public class WhenExecuteGet : UfTest
    {
        public IUfService _service;
        public Mock<IUfService> _serviceMock;

        [Fact(DisplayName = "When request get for an UF.")]
        public async Task WhenRequestGet()
        {
            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(uf => uf.Get(IdUf)).ReturnsAsync(ufDto);
            _service = _serviceMock.Object;

            var _getResult = await _service.Get(IdUf);
            Assert.NotNull(_getResult);
            Assert.Equal(IdUf, _getResult.Id);
            Assert.Equal(Name, _getResult.Name);
            Assert.Equal(Initials, _getResult.Initials);

            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(uf => uf.Get(It.IsAny<Guid>())).ReturnsAsync((UfDto)null);
            _service = _serviceMock.Object;

            var _getNullResult = await _service.Get(Guid.NewGuid());
            Assert.Null(_getNullResult);
        }
    }
}
