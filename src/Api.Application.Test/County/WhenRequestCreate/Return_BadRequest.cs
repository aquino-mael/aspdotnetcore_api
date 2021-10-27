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
    public class Return_BadRequest
    {
        private CountiesController _controller;

        [Fact(DisplayName = "Can return BadRequest on delete.")]
        public async Task WhenRequestDeleteReturnsBadRequest()
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
            _controller.ModelState.AddModelError("IBGECode", "Código de IBGE inválido.");

            var _countyDtoCreate = new CountyDtoCreate
            {
                IBGECode = Faker.RandomNumber.Next(1, 1000000),
                Name = Faker.Address.UkCounty(),
                UfId = Guid.NewGuid(),
            };

            var _creationResult = await _controller.Post(_countyDtoCreate);
            Assert.True(_creationResult is BadRequestObjectResult);
        }
    }
}
