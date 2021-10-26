using System;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class ZipCodeCompleteCrud : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _service;

        public ZipCodeCompleteCrud(DbTest dbTest)
        {
            _service = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "Can do a complete ZIPCODE CRUD.")]
        public async Task CanDoCompleteZipCodeCrud()
        {
            using (var context = _service.GetService<MyContext>())
            {
                CountyImplementation _countyRepository = new CountyImplementation(context);
                CountyEntity _countyEntity = new CountyEntity
                {
                    Name = Faker.Address.City(),
                    IBGECode = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("57a9e9f7-9aea-40fe-a783-65d4feb59fa8"),
                };
                await _countyRepository.InsertAsync(_countyEntity);


                ZipCodeImplementation _repository = new ZipCodeImplementation(context);
                ZipCodeEntity _entity = new ZipCodeEntity
                {
                    Street = Faker.Address.StreetAddress(),
                    Number = Faker.RandomNumber.Next(0, 1000).ToString(),
                    ZipCode = Faker.Address.ZipCode(),
                    CountyId = _countyEntity.Id,
                };

                // CREATE

                var _createResult = await _repository.InsertAsync(_entity);
                Assert.NotNull(_createResult);
                Assert.NotNull(_createResult.Id);
                Assert.NotNull(_createResult.CreatedAt);
                Assert.Equal(_entity.Street, _createResult.Street);
                Assert.Equal(_entity.ZipCode, _createResult.ZipCode);
                Assert.Equal(_entity.CountyId, _createResult.CountyId);

                // UPDATE

                _createResult.Street = Faker.Address.StreetAddress();
                _createResult.ZipCode = Faker.Address.ZipCode();

                var _updateResult = await _repository.UpdateAsync(_createResult);
                Assert.NotNull(_updateResult);
                Assert.NotNull(_updateResult.UpdatedAt);
                Assert.Equal(_createResult.Street, _updateResult.Street);
                Assert.Equal(_createResult.ZipCode, _updateResult.ZipCode);
                Assert.Equal(_createResult.Id, _updateResult.Id);
                Assert.Equal(_createResult.Number, _updateResult.Number);
                Assert.Equal(_createResult.CountyId, _updateResult.CountyId);

                // READ BY ID

                var _getByIdResult = await _repository.SelectAsync(_updateResult.Id);
                Assert.NotNull(_getByIdResult);
                Assert.Null(_getByIdResult.County.Uf);
                Assert.Equal(_updateResult.Id, _getByIdResult.Id);
                Assert.Equal(_updateResult.Number, _getByIdResult.Number);
                Assert.Equal(_updateResult.Street, _getByIdResult.Street);
                Assert.Equal(_updateResult.ZipCode, _getByIdResult.ZipCode);
                Assert.Equal(_updateResult.CountyId, _getByIdResult.CountyId);
                Assert.Equal(_updateResult.CreatedAt, _getByIdResult.CreatedAt);
                Assert.Equal(_updateResult.UpdatedAt, _getByIdResult.UpdatedAt);

                // READ BY ZIPCODE

                var _getByZipCodeResult = await _repository.SelectAsync(_updateResult.ZipCode);
                Assert.NotNull(_getByZipCodeResult);
                Assert.NotNull(_getByZipCodeResult.County);
                Assert.NotNull(_getByZipCodeResult.County.Uf);
                Assert.Equal(_updateResult.Id, _getByZipCodeResult.Id);
                Assert.Equal("MA", _getByZipCodeResult.County.Uf.Initials);
                Assert.Equal(_countyEntity.UfId, _getByZipCodeResult.County.UfId);
                Assert.Equal(_countyEntity.Name, _getByZipCodeResult.County.Name);
                Assert.Equal(_updateResult.Number, _getByZipCodeResult.Number);
                Assert.Equal(_updateResult.Street, _getByZipCodeResult.Street);
                Assert.Equal(_updateResult.ZipCode, _getByZipCodeResult.ZipCode);
                Assert.Equal(_updateResult.CountyId, _getByZipCodeResult.CountyId);
                Assert.Equal(_updateResult.CreatedAt, _getByZipCodeResult.CreatedAt);
                Assert.Equal(_updateResult.UpdatedAt, _getByZipCodeResult.UpdatedAt);
                Assert.Equal(_countyEntity.IBGECode, _getByZipCodeResult.County.IBGECode);

                // READ ALL

                var _getAllResult = await _repository.SelectAsync();
                Assert.NotNull(_getAllResult);
                Assert.NotEmpty(_getAllResult);

                // DELETE

                var _deletedResult = await _repository.DeleteAsync(_getByZipCodeResult.Id);
                Assert.NotNull(_deletedResult);
                Assert.True(_deletedResult);

                // VALIDATION GETALL WITHOUT REGISTERS

                _getAllResult = await _repository.SelectAsync();
                Assert.NotNull(_getAllResult);
                Assert.Empty(_getAllResult);
            }
        }
    }
}
