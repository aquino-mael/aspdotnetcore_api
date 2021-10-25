using System;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Dtos.County;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class CountyCompleteCrud : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public CountyCompleteCrud(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "Can do a county CRUD.")]
        [Trait("CRUD", "CountyEntity")]
        public async Task CanDoCountyCrud()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                CountyImplementation _repository = new CountyImplementation(context);
                CountyEntity _entity = new CountyEntity
                {
                    Name = Faker.Address.City(),
                    IBGECode = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("57a9e9f7-9aea-40fe-a783-65d4feb59fa8"),
                };

                // CREATE

                var _createResult = await _repository.InsertAsync(_entity);
                Assert.NotNull(_createResult);
                Assert.True(_createResult.Id != Guid.Empty);
                Assert.Equal(_entity.Name, _createResult.Name);
                Assert.Equal(_entity.UfId, _createResult.UfId);
                Assert.Equal(_entity.IBGECode, _createResult.IBGECode);
                Assert.Null(_createResult.Uf);

                // READ BY ID

                var _readByIdResult = await _repository.GetCompleteById(_createResult.Id);
                Assert.NotNull(_readByIdResult);
                Assert.Equal(_createResult.Id, _readByIdResult.Id);
                Assert.Equal(_createResult.Name, _readByIdResult.Name);
                Assert.Equal(_createResult.UfId, _readByIdResult.UfId);
                Assert.Equal(_createResult.IBGECode, _readByIdResult.IBGECode);
                Assert.Equal(_createResult.CreatedAt, _readByIdResult.CreatedAt);
                Assert.NotNull(_readByIdResult.Uf);

                // READ BY IBGE

                var _readByIBGEResult = await _repository.GetCompleteByIBGE(_createResult.IBGECode);
                Assert.NotNull(_readByIBGEResult);
                Assert.Equal(_createResult.Id, _readByIBGEResult.Id);
                Assert.Equal(_createResult.Name, _readByIBGEResult.Name);
                Assert.Equal(_createResult.UfId, _readByIBGEResult.UfId);
                Assert.Equal(_createResult.IBGECode, _readByIBGEResult.IBGECode);
                Assert.Equal(_createResult.CreatedAt, _readByIBGEResult.CreatedAt);
                Assert.NotNull(_readByIBGEResult.Uf);

                // READ ALL

                var _allRegisters = await _repository.SelectAsync();
                Assert.NotNull(_allRegisters);
                Assert.NotEmpty(_allRegisters);

                // UPDATE

                _entity.Name = Faker.Address.City();
                _entity.UfId = new Guid("27f7a92b-1979-4e1c-be9d-cd3bb73552a8");

                var _updateResult = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_updateResult);
                Assert.NotNull(_updateResult.UpdatedAt);
                Assert.Equal(_entity.IBGECode, _updateResult.IBGECode);
                Assert.Equal(_entity.CreatedAt, _updateResult.CreatedAt);
                Assert.Equal(_entity.UfId, _updateResult.UfId);
                Assert.Equal(_entity.Name, _updateResult.Name);
                Assert.True(_entity.Id == _updateResult.Id);

                // DELETE

                var _deletedResult = await _repository.DeleteAsync(_updateResult.Id);
                Assert.NotNull(_deletedResult);
                Assert.True(_deletedResult);

                _allRegisters = await _repository.SelectAsync();
                Assert.NotNull(_allRegisters);
                Assert.Empty(_allRegisters);
            }
        }
    }
}
