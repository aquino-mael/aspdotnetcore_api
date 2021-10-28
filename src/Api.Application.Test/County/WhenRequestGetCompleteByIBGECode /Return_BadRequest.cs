using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.County;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.County;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.County.WhenRequestGetCompleteByIBGECode
{
    public class Return_BadRequest
    {
        private CountiesController _controller;

        [Fact]
        public async Task WhenBadRequestOnGetCompleteByIBGEInCountiesController()
        {
            var _ufId = Guid.NewGuid();

            var _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(c => c.GetCompleteByIBGE(It.IsAny<int>()))
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
            _controller.ModelState.AddModelError("Id", "Id no formato inválido.");

            var _getCompleteByIBGEResult = await _controller.GetCompleteByIBGE(Faker.RandomNumber.Next(1000000, 9999999));
            Assert.True(_getCompleteByIBGEResult is BadRequestObjectResult);
        }
    }
}
