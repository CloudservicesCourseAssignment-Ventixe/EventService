using Data.Entities;
using Data.Models;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IEventRepository : IBaseRepository<EventEntity>
{
}

