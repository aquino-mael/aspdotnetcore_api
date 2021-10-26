using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.ZipCode;
using Api.Domain.Entities;
using Api.Domain.models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class ZipCodeMapper : BaseTestService
    {
        [Fact(DisplayName = "Can execute auto mapper on zip code models.")]
        public void CanExecuteAutoMapperOnZipCodeModels()
        {
            var _model = new ZipCodeModel
            {
                Id = Guid.NewGuid(),
                ZipCode = Faker.Address.ZipCode(),
                Street = Faker.Address.StreetAddress(),
                Number = Faker.RandomNumber.Next(1, 100).ToString(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var _entities = new List<ZipCodeEntity>();
            for (int i = 0; i < 5; i++)
            {
                var _entity = new ZipCodeEntity
                {
                    Id = Guid.NewGuid(),
                    ZipCode = Faker.Address.ZipCode(),
                    CountyId = Guid.NewGuid(),
                    Number = Faker.RandomNumber.Next(1, 100).ToString(),
                    Street = Faker.Address.StreetAddress(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };
            }

            // MODEL => ENTITY
            var _toEntity = Mapper.Map<ZipCodeEntity>(_model);
            Assert.NotNull(_toEntity);
            Assert.Equal(_model.Id, _toEntity.Id);
            Assert.Equal(_model.ZipCode, _toEntity.ZipCode);
            Assert.Equal(_model.Street, _toEntity.Street);
            Assert.Equal(_model.Number, _toEntity.Number);
            Assert.Equal(_model.CreatedAt, _toEntity.CreatedAt);
            Assert.Equal(_model.UpdatedAt, _toEntity.UpdatedAt);

            // ENTITY => DTO
            var _toDto = Mapper.Map<ZipCodeDto>(_toEntity);
            Assert.NotNull(_toDto);
            Assert.Equal(_toEntity.Id, _toDto.Id);
            Assert.Equal(_toEntity.ZipCode, _toDto.ZipCode);
            Assert.Equal(_toEntity.Street, _toDto.Street);
            Assert.Equal(_toEntity.Number, _toDto.Number);

            // DTO => MODEL
            var _toModel = Mapper.Map<ZipCodeModel>(_toDto);
            Assert.NotNull(_toModel);
            Assert.Equal(_toDto.Id, _toModel.Id);
            Assert.Equal(_toDto.ZipCode, _toModel.ZipCode);
            Assert.Equal(_toDto.Street, _toModel.Street);
            Assert.Equal(_toDto.Number, _toModel.Number);

            // ENTITIES => DTOs
            var _dtoList = Mapper.Map<List<ZipCodeDto>>(_entities);
            Assert.NotNull(_dtoList);
            Assert.True(_dtoList.Count() == _entities.Count());

            for (int i = 0; i < _dtoList.Count(); i++)
            {
                Assert.Equal(_dtoList[i].Id, _entities[i].Id);
                Assert.Equal(_dtoList[i].ZipCode, _entities[i].ZipCode);
                Assert.Equal(_dtoList[i].Number, _entities[i].Number);
                Assert.Equal(_dtoList[i].Street, _entities[i].Street);
                Assert.Equal(_dtoList[i].CountyId, _entities[i].CountyId);
            }
        }
    }
}
