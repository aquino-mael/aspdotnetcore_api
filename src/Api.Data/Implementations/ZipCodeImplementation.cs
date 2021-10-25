using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class ZipCodeImplementation : BaseRepository<ZipCodeEntity>, IZipCodeRepository
    {
        private DbSet<ZipCodeEntity> _dataset;
        public ZipCodeImplementation(MyContext context) : base(context)
        {
            _dataset = context.Set<ZipCodeEntity>();
        }

        public Task<ZipCodeEntity> SelectAsync(string zipCode)
        {
            return _dataset.Include(c => c.County)
                           .ThenInclude(m => m.Uf)
                           .FirstOrDefaultAsync(u => u.ZipCode.Equals(zipCode));
        }
    }
}
