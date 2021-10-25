using System;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Domain.Repository
{
    public interface ICountyRepository : IRepository<CountyEntity>
    {
        Task<CountyEntity> GetCompleteById(Guid id);
        Task<CountyEntity> GetCompleteByIBGE(int IBGECode);
    }
}
