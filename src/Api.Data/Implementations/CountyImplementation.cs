using System;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class CountyImplementation : BaseRepository<CountyEntity>, ICountyRepository
    {
        private DbSet<CountyEntity> _dataset;

        public CountyImplementation(MyContext context) : base(context)
        {
            _dataset = context.Set<CountyEntity>();
        }
        public async Task<CountyEntity> GetCompleteByIBGE(int IBGECode)
        {
            return await _dataset.Include(m => m.Uf)
                                 .FirstOrDefaultAsync(m => m.IBGECode.Equals(IBGECode));
        }

        public async Task<CountyEntity> GetCompleteById(Guid id)
        {
            return await _dataset.Include(m => m.Uf)
                                 .FirstOrDefaultAsync(m => m.Id.Equals(id));
        }
    }
}
