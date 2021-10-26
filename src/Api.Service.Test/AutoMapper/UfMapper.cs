using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.Uf;
using Api.Domain.Entities;
using Api.Domain.models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UfMapper : BaseTestService
    {
        [Fact(DisplayName = "Transfer data model to an entity.")]
        public void CanExecuteAutoMapperOnUf()
        {
            var _model = new UfModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Initials = Faker.Address.UsStateAbbr(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var _entities = new List<UfEntity>();

            for (int i = 0; i < 5; i++)
            {
                UfEntity item = new UfEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Initials = Faker.Address.UsStateAbbr(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                _entities.Add(item);
            }

            // MODEL => ENTITY
            var _toEntity = Mapper.Map<UfEntity>(_model);
            Assert.NotNull(_toEntity);
            Assert.Equal(_model.Id, _toEntity.Id);
            Assert.Equal(_model.Name, _toEntity.Name);
            Assert.Equal(_model.Initials, _toEntity.Initials);
            Assert.Equal(_model.CreatedAt, _toEntity.CreatedAt);
            Assert.Equal(_model.UpdatedAt, _toEntity.UpdatedAt);

            // ENTITY => DTO
            var _toDto = Mapper.Map<UfDto>(_toEntity);
            Assert.NotNull(_toDto);
            Assert.Equal(_model.Id, _toDto.Id);
            Assert.Equal(_model.Name, _toDto.Name);
            Assert.Equal(_model.Initials, _toDto.Initials);

            // DTO => MODEL
            var _toModel = Mapper.Map<UfModel>(_toDto);
            Assert.NotNull(_toModel);
            Assert.Equal(_toDto.Id, _toModel.Id);
            Assert.Equal(_toDto.Name, _toModel.Name);
            Assert.Equal(_toDto.Initials, _toModel.Initials);

            // ENTITIES => DTOs
            var _dtoList = Mapper.Map<List<UfDto>>(_entities);
            Assert.True(_entities.Count() == _dtoList.Count());

            for (int i = 0; i < _dtoList.Count(); i++)
            {
                Assert.Equal(_dtoList[i].Id, _entities[i].Id);
                Assert.Equal(_dtoList[i].Name, _entities[i].Name);
                Assert.Equal(_dtoList[i].Initials, _entities[i].Initials);
            }
        }
    }
}
