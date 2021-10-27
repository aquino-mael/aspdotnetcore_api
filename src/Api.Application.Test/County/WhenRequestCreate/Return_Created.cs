using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.County;
using Api.Domain.Interfaces.Services.County;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.County.WhenRequestCreate
{
    public class Return_Created
    {
        private CountiesController _controller;

        [Fact(DisplayName = "Can do a delete.")]
        public async Task WhenRequestDelete()
        {
            var _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(countyService => countyService.Post(It.IsAny<CountyDtoCreate>())).ReturnsAsync(
                new CountyDtoCreateResult
                {
                    Id = Guid.NewGuid(),
                    Name = "Maranhão",
                    CreatedAt = DateTime.UtcNow,
                }
            );

            _controller = new CountiesController(_serviceMock.Object);

            var url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var _countyDtoCreate = new CountyDtoCreate
            {
                IBGECode = Faker.RandomNumber.Next(1, 1000000),
                Name = Faker.Address.UkCounty(),
                UfId = Guid.NewGuid(),
            };

            var _creationResult = await _controller.Post(_countyDtoCreate);
            Assert.True(_creationResult is CreatedResult);

            var _creationValue = ((CreatedResult)_creationResult).Value as CountyDtoCreateResult;
            Assert.NotNull(_creationValue);
            Assert.NotNull(_creationValue.Id);
            Assert.NotNull(_creationValue.CreatedAt);
        }
    }
}
