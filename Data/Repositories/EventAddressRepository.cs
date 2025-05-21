using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class EventAddressRepository(DataContext context) : BaseRepository<EventAddressEntity>(context), IEventAddressRepository
{
    public override async Task<RepositoryResponse<EventAddressEntity>> GetOneAsync(Expression<Func<EventAddressEntity, bool>> expression)
    {
        if (expression == null)
            return new RepositoryResponse<EventAddressEntity> { Succeeded = false, Error = "Expression cannot be null", Result = null };

        var entity = await _dbSet.Include(ea => ea.Events).FirstOrDefaultAsync(expression);

        return entity != null
            ? new RepositoryResponse<EventAddressEntity> { Succeeded = true, Result = entity }
            : new RepositoryResponse<EventAddressEntity> { Succeeded = false, Result = null, Error = "Entity not found" };
    }

    public override async Task<RepositoryResponse<IEnumerable<EventAddressEntity>>> GetAllAsync()
    {
        var entities = await _dbSet.Include(ea => ea.Events).ToListAsync();
        return new RepositoryResponse<IEnumerable<EventAddressEntity>> { Succeeded = true, Result = entities };
    }
}

