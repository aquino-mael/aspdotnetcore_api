using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.County;
using Api.Domain.Interfaces.Services.County;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.County.WhenRequestGet
{
    public class Return_NotFound
    {
        private CountiesController _controller;

        [Fact]
        public async Task WhenRequestGetOnCountiesControllerAndReturnNotFound()
        {
            var _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(c => c.Get(It.IsAny<Guid>())).ReturnsAsync((CountyDto)null);

            _controller = new CountiesController(_serviceMock.Object);

            var _getResult = await _controller.Get(Guid.NewGuid());
            Assert.True(_getResult is NotFoundResult);
        }
    }
}
