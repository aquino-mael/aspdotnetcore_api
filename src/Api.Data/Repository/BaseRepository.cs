using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
  public class BaseRepository<T> : IRepository<T> where T : BaseEntity
  {
    protected readonly MyContext _context;

    private DbSet<T> _dataset;

    public BaseRepository(MyContext context)
    {
      _context = context;
      _dataset = _context.Set<T>();
    }

    public Task<T> DeleteAsync(Guid id)
    {
      throw new NotImplementedException();
    }

    public async Task<T> InsertAsync(T entity)
    {
      try
      {
        if (entity.Id == Guid.Empty)
        {
          entity.Id = Guid.NewGuid();
        }

        entity.CreatedAt = DateTime.UtcNow;
        _dataset.Add(entity);

        await _context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        throw ex;
      }

      return entity;
    }

    public Task<T> SelectAsync(Guid id)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> SelectAsync()
    {
      throw new NotImplementedException();
    }

    public async Task<T> UpdateAsync(T entity)
    {
      try
      {
        var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(entity.Id));
        if (result == null) return null;

        entity.UpdatedAt = DateTime.UtcNow;
        entity.CreatedAt = result.CreatedAt;

        _context.Entry(result).CurrentValues.SetValues(entity);

        await _context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        throw ex;
      }

      return entity;
    }
  }
}
