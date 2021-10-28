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
    public class Return_Get
    {
        private CountiesController _controller;

        [Fact]
        public async Task WhenRequestGetOnCountiesController()
        {
            var _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(c => c.Get(It.IsAny<Guid>())).ReturnsAsync(new CountyDto
            {
                Id = Guid.NewGuid(),
                Name = Faker.Address.UkCounty(),
                IBGECode = Faker.RandomNumber.Next(1000000, 9999999),
                UfId = Guid.NewGuid(),
            });

            _controller = new CountiesController(_serviceMock.Object);

            var _getResult = await _controller.Get(Guid.NewGuid());
            Assert.True(_getResult is OkObjectResult);
        }
    }
}
