using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.County;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.County;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.County.WhenRequestGetAll
{
    public class Return_BadRequest
    {
        private CountiesController _controller;

        [Fact(DisplayName = "Can do Get All on County controller and return BadRequest.")]
        public async Task WhenRequestGetAllOnCountyControllerAndReturnBadRequest()
        {
            var _countiesDtoComplete = new List<CountyDtoComplete>();

            for (int i = 0; i < 10; i++)
            {
                var _ufId = Guid.NewGuid();

                var _countyDtoComplete = new CountyDtoComplete
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Address.UkCounty(),
                    IBGECode = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = _ufId,
                    Uf = new UfDto
                    {
                        Id = _ufId,
                        Name = Faker.Address.UsState(),
                        Initials = Faker.Address.UsStateAbbr(),
                    },
                };

                _countiesDtoComplete.Add(_countyDtoComplete);
            }

            var _serviceMock = new Mock<ICountyService>();
            _serviceMock.Setup(c => c.GetAll()).ReturnsAsync(_countiesDtoComplete);

            _controller = new CountiesController(_serviceMock.Object);
            _controller.ModelState.AddModelError(
                "Authorization",
                "Você não possui autorização para acessar essa parte do sistema."
            );

            var _getAllResult = await _controller.GetAll();
            Assert.True(_getAllResult is BadRequestObjectResult);
        }
    }
}
