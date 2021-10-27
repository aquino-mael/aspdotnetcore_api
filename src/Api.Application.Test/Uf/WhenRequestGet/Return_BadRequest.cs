using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Uf.WhenRequestGet
{
    public class Return_BadRequest
    {
        private UfsController _controller;

        [Fact(DisplayName = "Can do a GET ALL request in Uf endpoint.")]
        public async Task WhenRequestGet()
        {
            var _serviceMock = new Mock<IUfService>();

            string initials = "MA";
            string name = "Maranhão";

            _serviceMock.Setup(ufService => ufService.Get(It.IsAny<Guid>())).ReturnsAsync(
                new UfDto
                {
                    Id = Guid.NewGuid(),
                    Initials = initials,
                    Name = name,
                }
            );

            _controller = new UfsController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Id no formato inválido.");

            var _result = await _controller.Get(Guid.NewGuid());
            Assert.True(_result is BadRequestObjectResult);
        }
    }
}
