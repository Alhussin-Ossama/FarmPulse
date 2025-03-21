using FarmPulse.Core;
using FarmPulse.Core.Interfaces;
using FarmPulse.Repository.Data;
using FarmPulse.Repository.Implementation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmPulse.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly FarmContext _context;
		private Hashtable _repositories;

		public UnitOfWork(FarmContext context)
		{
			_context = context;
			_repositories = new Hashtable();
		}

		public async ValueTask DisposeAsync()
			=> await _context.DisposeAsync();

		public async Task<int> SaveAsync()
			=> await _context.SaveChangesAsync();

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
		{


			var type = typeof(TEntity).Name;
			if (!_repositories.ContainsKey(type))
			{
				var Repository = new GenericRepository<TEntity>(_context); 
				_repositories.Add(type, Repository);
			}
			return _repositories[type] as IGenericRepository<TEntity>;
		}
	}
}
