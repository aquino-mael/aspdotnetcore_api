using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.County;
using Api.Domain.Dtos.Uf;
using Api.Domain.Dtos.ZipCode;
using Api.Domain.Interfaces.Services.ZipCode;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.ZipCode
{
    public class WhenRequestGet
    {
        private ZipCodesController _controller;
        private Mock<IZipCodeService> _serviceMock;

        private static ZipCodeDto _ZipCodeDto = new ZipCodeDto
        {
            Id = Guid.NewGuid(),
            Street = Faker.Address.StreetAddress(),
            Number = Faker.RandomNumber.Next(1, 10).ToString(),
            ZipCode = Faker.Address.ZipCode(),
            County = new CountyDtoComplete
            {
                Id = Guid.NewGuid(),
                UfId = Guid.NewGuid(),
                IBGECode = Faker.RandomNumber.Next(1000000, 9999999),
                Name = Faker.Address.UkCounty(),
                Uf = new UfDto
                {
                    Id = Guid.NewGuid(),
                    Initials = Faker.Address.UsStateAbbr(),
                    Name = Faker.Address.UsState(),
                }
            }
        };


        private void InitializeController(ZipCodeDto value)
        {
            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(p => p.Get(It.IsAny<Guid>())).ReturnsAsync(
                value
            );

            _serviceMock.Setup(p => p.Get(It.IsAny<string>())).ReturnsAsync(
                value
            );

            _controller = new ZipCodesController(_serviceMock.Object);
        }

        #region SUCCESS

        [Fact]
        public async Task WhenRequestGetWithIdOnZipCodesController()
        {
            InitializeController(_ZipCodeDto);
            var _getWithIdResult = await _controller.GetById(Guid.NewGuid());
            Assert.True(_getWithIdResult is OkObjectResult);
        }

        [Fact]
        public async Task WhenRequestGetWithZipCodeOnZipCodesController()
        {
            InitializeController(_ZipCodeDto);
            var _getWithIdResult = await _controller.GetByCep("65911-000");
            Assert.True(_getWithIdResult is OkObjectResult);
        }

        #endregion

        #region FAILURES

        [Fact]
        public async Task WhenRequestGetWithIdOnZipCodesControllerAndReturnBadRequest()
        {
            InitializeController(_ZipCodeDto);
            _controller.ModelState.AddModelError("Id", "Id no farmato inválido");

            var _getWithIdResult = await _controller.GetById(Guid.NewGuid());
            Assert.True(_getWithIdResult is BadRequestObjectResult);
        }

        [Fact]
        public async Task WhenRequestGetWithZipCodeOnZipCodesControllerAndReturnBadRequest()
        {
            InitializeController(_ZipCodeDto);
            _controller.ModelState.AddModelError("ZipCode", "CEP no farmato inválido");

            var _getWithIdResult = await _controller.GetByCep("659000");
            Assert.True(_getWithIdResult is BadRequestObjectResult);
        }

        [Fact]
        public async Task WhenRequestGetWithZipCodeOnZipCodesControllerAndReturnNotFound()
        {
            InitializeController((ZipCodeDto)null);

            var _getWithIdResult = await _controller.GetByCep("65911-324");
            Assert.True(_getWithIdResult is NotFoundResult);
        }

        [Fact]
        public async Task WhenRequestGetWithIdOnZipCodesControllerAndReturnNotFound()
        {
            InitializeController((ZipCodeDto)null);

            var _getWithIdResult = await _controller.GetById(Guid.NewGuid());
            Assert.True(_getWithIdResult is NotFoundResult);
        }

        #endregion
    }
}
