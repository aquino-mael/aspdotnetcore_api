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
    public class Return_Get
    {
        private UfsController _controller;

        [Fact(DisplayName = "Can do a get request in Uf endpoint.")]
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

            var _result = await _controller.Get(Guid.NewGuid());
            Assert.True(_result is OkObjectResult);

            var _getResult = ((OkObjectResult)_result).Value as UfDto;
            Assert.NotNull(_getResult);
            Assert.Equal(name, _getResult.Name);
            Assert.Equal(initials, _getResult.Initials);
        }
    }
}
