using FarmPulse.Core.Interfaces;
using FarmPulse.Core.Models;
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
	public class ChickenRepository : GenericRepository<Chicken>, IChickenRepository
	{
		private readonly FarmContext _context;

		public ChickenRepository(FarmContext context) : base(context)
		{
			_context = context;
		}

		public async Task<Chicken> GetByRFIDAsync(string RFID)
			=> await _context.Chicken.FirstOrDefaultAsync(C => C.RFID == RFID);

		public async Task<int> GetAliveChickenCountAsync()
			=> await _context.Chicken.CountAsync(c => c.ActivityStatus == ActivityStatus.Alive.ToString());
		
		public async Task<int> GetDeadChickenCountAsync()
			=> await _context.Chicken.CountAsync(c => c.ActivityStatus == ActivityStatus.Dead.ToString());

		public async Task<IEnumerable<Chicken>> GetFilteredChickens(Expression<Func<Chicken, bool>> filter)
		{
			return await _context.Chicken.Where(filter).ToListAsync();
		}
	}
}
