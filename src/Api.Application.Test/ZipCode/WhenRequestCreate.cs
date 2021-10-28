using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.ZipCode;
using Api.Domain.Interfaces.Services.ZipCode;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.ZipCode
{
    public class WhenRequestCreate
    {
        private ZipCodesController _controller;
        private Mock<IZipCodeService> _serviceMock;

        private static ZipCodeDtoCreate _zipCodeDtoCreate = new ZipCodeDtoCreate
        {
            ZipCode = Faker.Address.ZipCode(),
            CountyId = Guid.NewGuid(),
            Number = Faker.RandomNumber.Next(1, 10).ToString(),
            Street = Faker.Address.StreetAddress(),
        };

        public void InitializeController()
        {
            var result = new ZipCodeDtoCreateResult
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                CountyId = Guid.NewGuid(),
                ZipCode = Faker.Address.ZipCode(),
                Number = Faker.RandomNumber.Next(1, 10).ToString(),
                Street = Faker.Address.StreetAddress(),
            };

            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(p => p.Post(It.IsAny<ZipCodeDtoCreate>()))
                        .ReturnsAsync(result);

            _controller = new ZipCodesController(_serviceMock.Object);

            var url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;
        }

        [Fact]
        public async Task WhenRequestCreateOnZipCodesController()
        {
            InitializeController();

            var _creationResult = await _controller.Post(_zipCodeDtoCreate);
            Assert.True(_creationResult is CreatedResult);
        }

        [Fact]
        public async Task WhenRequestCreateOnZipCodesControllerAndReturnBadRequest()
        {
            InitializeController();
            _controller.ModelState.AddModelError("Id", "Id em formato inválido.");

            var _creationResult = await _controller.Post(_zipCodeDtoCreate);
            Assert.True(_creationResult is BadRequestObjectResult);
        }
    }
}
