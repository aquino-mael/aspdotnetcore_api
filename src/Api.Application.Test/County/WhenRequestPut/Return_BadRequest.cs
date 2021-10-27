using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.County;
using Api.Domain.Interfaces.Services.County;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.County.WhenRequestPut
{
    public class Return_BadRequest
    {
        private CountiesController _controller;

        [Fact(DisplayName = "Can do a put request to County controller and return BadRequest.")]
        public async Task WhenRequestPutOnCountyControllerAndReturnBadRequest()
        {
            var _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(c => c.Put(It.IsAny<CountyDtoUpdate>())).ReturnsAsync(
                new CountyDtoUpdateResult
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    IBGECode = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = Guid.NewGuid(),
                    UpdatedAt = DateTime.UtcNow,
                }
            );

            _controller = new CountiesController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "O ID informado é inválido.");

            var _countyUpdate = new CountyDtoUpdate
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                IBGECode = Faker.RandomNumber.Next(1000000, 9999999),
                UfId = Guid.NewGuid(),
            };

            var _updateResult = await _controller.Put(_countyUpdate);
            Assert.True(_updateResult is BadRequestObjectResult);
        }
    }
}
