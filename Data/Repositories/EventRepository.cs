using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class EventRepository(DataContext context) : BaseRepository<EventEntity>(context) , IEventRepository
{
    public override async Task<RepositoryResponse<EventEntity>> GetOneAsync(Expression<Func<EventEntity, bool>> expression)
    {
        if (expression == null)
            return new RepositoryResponse<EventEntity> { Succeeded = false, Error = "Expression cannot be null", Result = null };

        var entity = await _dbSet.Include(e => e.EventAddress).Include(e => e.Packages).FirstOrDefaultAsync(expression);

        return entity != null
            ? new RepositoryResponse<EventEntity> { Succeeded = true, Result = entity }
            : new RepositoryResponse<EventEntity> { Succeeded = false, Result = null, Error = "Entity not found" };
    }

    public override async Task<RepositoryResponse<IEnumerable<EventEntity>>> GetAllAsync()
    {
        var entities = await _dbSet.Include(e => e.EventAddress)
                                   .Include(e => e.Packages)
                                   .ToListAsync();
        return new RepositoryResponse<IEnumerable<EventEntity>> { Succeeded = true, Result = entities };
    }
}


