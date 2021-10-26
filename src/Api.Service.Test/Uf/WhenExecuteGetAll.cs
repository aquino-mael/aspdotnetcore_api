using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Uf;
using Moq;
using Xunit;

namespace Api.Service.Test.Uf
{
    public class WhenExecuteGetAll : UfTest
    {
        public IUfService _service;
        public Mock<IUfService> _serviceMock;

        [Fact(DisplayName = "When request get all on UF service")]
        public async Task WhenRequestGetAllOnUfService()
        {
            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(ufService => ufService.GetAll()).ReturnsAsync(dtoList);
            _service = _serviceMock.Object;

            var _getAllResult = await _service.GetAll();
            Assert.NotNull(_getAllResult);
            Assert.NotEmpty(_getAllResult);

            dtoList.Clear();

            var _getAllEmptyResult = await _service.GetAll();
            Assert.NotNull(_getAllEmptyResult);
            Assert.Empty(_getAllEmptyResult);
        }
    }
}
