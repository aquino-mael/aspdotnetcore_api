using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.County;
using Api.Domain.Entities;
using Api.Domain.models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class CountyMapper : BaseTestService
    {
        [Fact(DisplayName = "Can execute auto mapper on county models.")]
        public void CanExecuteMapperOnCountyModels()
        {
            var _model = new CountyModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Address.UkCounty(),
                IBGECode = Faker.RandomNumber.Next(1000000, 9999999),
                UfId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var _entities = new List<CountyEntity>();
            for (int i = 0; i < 5; i++)
            {
                var _entity = new CountyEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    IBGECode = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Uf = new UfEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Initials = Faker.Address.UsStateAbbr(),
                    }
                };

                _entities.Add(_entity);
            }

            // MODEL => ENTITY
            var _toEntity = Mapper.Map<CountyEntity>(_model);
            Assert.NotNull(_toEntity);
            Assert.Equal(_model.Id, _toEntity.Id);
            Assert.Equal(_model.Name, _toEntity.Name);
            Assert.Equal(_model.UfId, _toEntity.UfId);
            Assert.Equal(_model.IBGECode, _toEntity.IBGECode);
            Assert.Equal(_model.CreatedAt, _toEntity.CreatedAt);
            Assert.Equal(_model.UpdatedAt, _toEntity.UpdatedAt);

            // MODEL => CREATE
            var _countyDtoCreate = Mapper.Map<CountyDtoCreate>(_model);
            Assert.NotNull(_countyDtoCreate);
            Assert.Equal(_model.Name, _countyDtoCreate.Name);
            Assert.Equal(_model.UfId, _countyDtoCreate.UfId);
            Assert.Equal(_model.IBGECode, _countyDtoCreate.IBGECode);

            // MODEL => UPDATE
            var _countyDtoUpdate = Mapper.Map<CountyDtoUpdate>(_model);
            Assert.NotNull(_countyDtoUpdate);
            Assert.Equal(_model.Id, _countyDtoUpdate.Id);
            Assert.Equal(_model.Name, _countyDtoUpdate.Name);
            Assert.Equal(_model.UfId, _countyDtoUpdate.UfId);
            Assert.Equal(_model.IBGECode, _countyDtoUpdate.IBGECode);

            // ENTITY => DTO
            var _toDto = Mapper.Map<CountyDto>(_toEntity);
            Assert.NotNull(_toDto);
            Assert.Equal(_toEntity.Id, _toDto.Id);
            Assert.Equal(_toEntity.Name, _toDto.Name);
            Assert.Equal(_toEntity.UfId, _toDto.UfId);
            Assert.Equal(_toEntity.IBGECode, _toDto.IBGECode);

            var _toCompleteDto = Mapper.Map<CountyDtoComplete>(_entities.FirstOrDefault());
            Assert.NotNull(_toCompleteDto);
            Assert.NotNull(_toCompleteDto.Uf);
            Assert.Equal(_entities.FirstOrDefault().Id, _toCompleteDto.Id);
            Assert.Equal(_entities.FirstOrDefault().Name, _toCompleteDto.Name);
            Assert.Equal(_entities.FirstOrDefault().UfId, _toCompleteDto.UfId);
            Assert.Equal(_entities.FirstOrDefault().IBGECode, _toCompleteDto.IBGECode);

            // DTO => MODEL
            var _toModel = Mapper.Map<CountyModel>(_toDto);
            Assert.NotNull(_toModel);
            Assert.Equal(_toDto.Id, _toModel.Id);
            Assert.Equal(_toDto.Name, _toModel.Name);
            Assert.Equal(_toDto.UfId, _toModel.UfId);
            Assert.Equal(_toDto.IBGECode, _toModel.IBGECode);

            // ENTITIES => DTOs
            var _dtoList = Mapper.Map<List<CountyDto>>(_entities);
            Assert.True(_dtoList.Count() == _entities.Count());

            for (int i = 0; i < _dtoList.Count(); i++)
            {
                Assert.Equal(_dtoList[i].Id, _entities[i].Id);
                Assert.Equal(_dtoList[i].Name, _entities[i].Name);
                Assert.Equal(_dtoList[i].UfId, _entities[i].UfId);
                Assert.Equal(_dtoList[i].IBGECode, _entities[i].IBGECode);
            }

            // ENTITY => DTO CREATE RESULT
            var _countyDtoCreateResult = Mapper.Map<CountyDtoCreateResult>(_toEntity);
            Assert.NotNull(_countyDtoCreateResult);
            Assert.Equal(_toEntity.Id, _countyDtoCreateResult.Id);
            Assert.Equal(_toEntity.Name, _countyDtoCreateResult.Name);
            Assert.Equal(_toEntity.UfId, _countyDtoCreateResult.UfId);
            Assert.Equal(_toEntity.IBGECode, _countyDtoCreateResult.IBGECode);
            Assert.Equal(_toEntity.CreatedAt, _countyDtoCreateResult.CreatedAt);

            // ENTITY => DTO UPDATE RESULT
            var _countyDtoUpdateResult = Mapper.Map<CountyDtoUpdateResult>(_toEntity);
            Assert.NotNull(_countyDtoUpdateResult);
            Assert.Equal(_toEntity.Id, _countyDtoUpdateResult.Id);
            Assert.Equal(_toEntity.Name, _countyDtoUpdateResult.Name);
            Assert.Equal(_toEntity.UfId, _countyDtoUpdateResult.UfId);
            Assert.Equal(_toEntity.IBGECode, _countyDtoUpdateResult.IBGECode);
            Assert.Equal(_toEntity.UpdatedAt, _countyDtoUpdateResult.UpdatedAt);
        }
    }
}
