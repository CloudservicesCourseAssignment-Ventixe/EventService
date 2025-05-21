using Data.Contexts;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<RepositoryResponse> AddAsync(TEntity entity)
    {
        if (entity == null)
        {
            return new RepositoryResponse { Succeeded = false, Error = "Entity cannot be null" };
        }


        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResponse { Succeeded = true, Message = "Entity created successfully" };
        }
        catch (Exception ex)
        {
            return new RepositoryResponse { Succeeded = false, Error = $"Problem creating {nameof(TEntity)} : {ex.Message}" };
        }
    }

    public virtual async Task<RepositoryResponse<TEntity>> GetOneAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return new RepositoryResponse<TEntity> { Succeeded = false, Error = "Expression cannot be null", Result = null };

        var entity = await _dbSet.FirstOrDefaultAsync(expression);

        return entity != null
            ? new RepositoryResponse<TEntity> { Succeeded = true, Result = entity }
            : new RepositoryResponse<TEntity> { Succeeded = false, Result = null, Error = "Entity not found" };
    }

    public virtual async Task<RepositoryResponse<IEnumerable<TEntity>>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();
        return new RepositoryResponse<IEnumerable<TEntity>> { Succeeded = true, Result = entities };
    }

    public virtual async Task<RepositoryResponse> UpdateAsync(TEntity updatedEntity)
    {
        if (updatedEntity == null)
        {
            return new RepositoryResponse { Succeeded = false, Error = "Entity cannot be null" };
        }


        try
        {
            _dbSet.Update(updatedEntity);
            await _context.SaveChangesAsync();
            return new RepositoryResponse { Succeeded = true, Message = "Entity updated successfully" };
        }
        catch (Exception ex)
        {
            return new RepositoryResponse { Succeeded = false, Error = $"Problem updating {nameof(TEntity)} : {ex.Message}" };
        }
    }

    public virtual async Task<RepositoryResponse> DeleteAsync(TEntity entity)
    {
        if (entity == null)
        {
            return new RepositoryResponse { Succeeded = false, Error = "Entity cannot be null" };
        }


        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResponse { Succeeded = true, Message = "Entity deleted successfully" };
        }
        catch (Exception ex)
        {
            return new RepositoryResponse { Succeeded = false, Error = $"Problem deleting {nameof(TEntity)} : {ex.Message}" };
        }
    }

    public virtual async Task<RepositoryResponse<TEntity>> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return new RepositoryResponse<TEntity> { Succeeded = false, Error = "Expression cannot be null" };
        var exists = await _dbSet.AnyAsync(expression);
         return exists
            ? new RepositoryResponse<TEntity> { Succeeded = true }
            : new RepositoryResponse<TEntity> { Succeeded = false, Error = "Entity not found" };
    }
}
