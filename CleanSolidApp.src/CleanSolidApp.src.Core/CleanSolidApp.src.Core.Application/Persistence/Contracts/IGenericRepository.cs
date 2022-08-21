using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanSolidApp.src.Core.Application.Persistence.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();    
    Task<bool> Exists(int id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}
