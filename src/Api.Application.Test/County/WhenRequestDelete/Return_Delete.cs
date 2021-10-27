using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.County;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.County.WhenRequestDelete
{
    public class Return_Delete
    {
        private CountiesController _controller;

        [Fact(DisplayName = "Can do a delete.")]
        public async Task WhenRequestDelete()
        {
            var _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            _controller = new CountiesController(_serviceMock.Object);

            var _deletedResult = await _controller.Delete(Guid.NewGuid());
            Assert.True(_deletedResult is OkObjectResult);

            var _deletedValue = ((OkObjectResult)_deletedResult).Value;
            Assert.NotNull(_deletedValue);
            Assert.True((bool)_deletedValue);
        }
    }
}
