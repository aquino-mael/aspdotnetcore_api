using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Uf.WhenRequestGetAll
{
    public class Return_GetAll
    {
        private UfsController _controller;

        [Fact(DisplayName = "Can do a get request in Uf endpoint.")]
        public async Task WhenRequestGet()
        {
            var _serviceMock = new Mock<IUfService>();

            var _ufDtos = new List<UfDto>();
            for (int i = 0; i < 5; i++)
            {
                var _ufDto = new UfDto
                {
                    Id = Guid.NewGuid(),
                    Initials = Faker.Address.UsStateAbbr(),
                    Name = Faker.Address.UsState(),
                };

                _ufDtos.Add(_ufDto);
            }

            _serviceMock.Setup(ufService => ufService.GetAll()).ReturnsAsync(_ufDtos);

            _controller = new UfsController(_serviceMock.Object);

            var _result = await _controller.GetAll();
            Assert.True(_result is OkObjectResult);

            var _getResult = ((OkObjectResult)_result).Value as List<UfDto>;
            Assert.NotNull(_getResult);
            Assert.NotEmpty(_getResult);
        }
    }
}
