using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.ZipCode;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.ZipCode
{
    public class WhenRequestDelete
    {
        private ZipCodesController _controller;
        private Mock<IZipCodeService> _serviceMock;

        private static Guid Id = Guid.NewGuid();

        public void InitializeController()
        {
            _serviceMock = new Mock<IZipCodeService>();
            _serviceMock.Setup(p => p.Delete(It.IsAny<Guid>()))
                        .ReturnsAsync(true);

            _controller = new ZipCodesController(_serviceMock.Object);
        }

        [Fact]
        public async Task WhenRequestDeleteOnZipCodesController()
        {
            InitializeController();

            var _creationResult = await _controller.Delete(Id);
            Assert.True(_creationResult is OkObjectResult);
        }

        [Fact]
        public async Task WhenRequestDeleteOnZipCodesControllerAndReturnBadRequest()
        {
            InitializeController();
            _controller.ModelState.AddModelError("Id", "Id em formato inválido.");

            var _creationResult = await _controller.Delete(Id);
            Assert.True(_creationResult is BadRequestObjectResult);
        }
    }
}
