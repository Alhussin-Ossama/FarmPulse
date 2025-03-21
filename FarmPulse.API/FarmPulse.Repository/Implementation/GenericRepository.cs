using FarmPulse.Core.Interfaces;
using FarmPulse.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FarmPulse.Repository.Implementation
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly FarmContext _context;

		public GenericRepository(FarmContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
			=> await _context.Set<T>().ToListAsync();

		public async Task<T> GetByIdAsync(int id)
			=> await _context.Set<T>().FindAsync(id);

		public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
			=> await _context.Set<T>().FirstOrDefaultAsync(predicate);
		

		public async Task AddAsync(T Item)
		=> await _context.Set<T>().AddAsync(Item);


		public void Update(T Item)
		=> _context.Set<T>().Update(Item);


		public void Delete(T Item)
		=> _context.Remove(Item);

	}
}
