using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.County;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.County;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.County.WhenRequestGetCompleteById
{
    public class Return_GetCompleteById
    {
        private CountiesController _controller;

        [Fact]
        public async Task WhenGetCompleteByIdOnCountiesController()
        {
            var _ufId = Guid.NewGuid();

            var _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(c => c.GetCompleteById(It.IsAny<Guid>()))
                        .ReturnsAsync(new CountyDtoComplete
                        {
                            Id = Guid.NewGuid(),
                            Name = Faker.Address.UkCounty(),
                            IBGECode = Faker.RandomNumber.Next(1000000, 9999999),
                            UfId = _ufId,
                            Uf = new UfDto
                            {
                                Id = _ufId,
                                Initials = Faker.Address.UsStateAbbr(),
                                Name = Faker.Address.UsState(),
                            }
                        });

            _controller = new CountiesController(_serviceMock.Object);

            var _getCompleteByIdResult = await _controller.GetCompleteById(Guid.NewGuid());
            Assert.True(_getCompleteByIdResult is OkObjectResult);
        }
    }
}
