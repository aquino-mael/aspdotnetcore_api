using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class UfGets : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public UfGets(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "Can do all UF gets.")]
        [Trait("GETs", "UfEntity")]
        public async Task CanDoAllUfGets()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UfImplementation _repository = new UfImplementation(context);

                UfEntity _entity = new UfEntity
                {
                    Id = new Guid("57a9e9f7-9aea-40fe-a783-65d4feb59fa8"),
                    Initials = "MA",
                    Name = "Maranhão",
                };

                var _validRegister = await _repository.ExistsAsync(_entity.Id);
                Assert.True(_validRegister);

                var _selectedRegister = await _repository.SelectAsync(_entity.Id);
                Assert.NotNull(_selectedRegister);
                Assert.Equal(_entity.Id, _selectedRegister.Id);
                Assert.Equal(_entity.Name, _selectedRegister.Name);
                Assert.Equal(_entity.Initials, _selectedRegister.Initials);

                var _allRegisters = await _repository.SelectAsync();
                Assert.NotNull(_allRegisters);
                Assert.True(_allRegisters.Count() == 27);
            }
        }
    }
}
