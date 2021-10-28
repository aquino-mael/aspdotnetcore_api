using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.ZipCode;
using Api.Domain.Interfaces.Services.ZipCode;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.ZipCode
{
    public class WhenRequestUpdate
    {
        private ZipCodesController _controller;
        private Mock<IZipCodeService> _serviceMock;

        private static ZipCodeDtoUpdate _zipCodeDtoUpdate = new ZipCodeDtoUpdate
        {
            Id = Guid.NewGuid(),
            Number = Faker.RandomNumber.Next(1, 10).ToString(),
            Street = Faker.Address.StreetAddress(),
            CountyId = Guid.NewGuid(),
            ZipCode = Faker.Address.ZipCode(),
        };

        public void InitializeController()
        {
            ZipCodeDtoUpdateResult result = new ZipCodeDtoUpdateResult
            {
                Id = Guid.NewGuid(),
                CountyId = Guid.NewGuid(),
                ZipCode = Faker.Address.ZipCode(),
                Number = Faker.RandomNumber.Next(1, 10).ToString(),
                Street = Faker.Address.StreetAddress(),
                UpdatedAt = DateTime.UtcNow,
            };

            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(p => p.Put(It.IsAny<ZipCodeDtoUpdate>()))
                        .ReturnsAsync(result);

            _controller = new ZipCodesController(_serviceMock.Object);
        }

        [Fact]
        public async Task WhenRequestUpdateOnZipCodesController()
        {
            InitializeController();

            var _updateResult = await _controller.Put(_zipCodeDtoUpdate);
            Assert.True(_updateResult is OkObjectResult);
        }

        [Fact]
        public async Task WhenRequestUpdateOnZipCodesControllerAndReturnBadRequest()
        {
            InitializeController();
            _controller.ModelState.AddModelError("Id", "Id em formato inválido.");

            var _updateResult = await _controller.Put(_zipCodeDtoUpdate);
            Assert.True(_updateResult is BadRequestObjectResult);
        }
    }
}
