using FarmPulse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FarmPulse.Core.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
		Task AddAsync(T Item);
		void Update(T Item);
		void Delete(T Item);
	}
}
