using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.County;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.County;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.County.WhenRequestGetCompleteByIBGECode
{
    public class Return_NotFound
    {
        private CountiesController _controller;

        [Fact]
        public async Task WhenNotFoundOnGetCompleteByIBGEInCountiesController()
        {
            var _ufId = Guid.NewGuid();

            var _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(c => c.GetCompleteByIBGE(It.IsAny<int>()))
                        .ReturnsAsync((CountyDtoComplete)null);

            _controller = new CountiesController(_serviceMock.Object);

            var _getCompleteByIBGEResult = await _controller.GetCompleteByIBGE(Faker.RandomNumber.Next(1000000, 9999999));
            Assert.True(_getCompleteByIBGEResult is NotFoundResult);
        }
    }
}
