using FarmPulse.Core.Interfaces;
using FarmPulse.Core.Models;
using FarmPulse.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmPulse.Repository.Implementation
{
	public class WeightRepository : GenericRepository<Weight>, IWeightRepository
	{
		private readonly FarmContext _context;

		public WeightRepository(FarmContext context) : base(context)
		{
			_context = context;
		}

		public async Task<Weight> GetExitWeightByChickenIdAsync(int id)
			=> await _context.Weight.Where(w => w.ChickenId == id).OrderByDescending(w => w.EntryTime) .FirstOrDefaultAsync();

		public async Task<IEnumerable<Weight>> GetWeightHistoryByRFIDAsync(string RFID)
			=> await _context.Weight.Where(w => w.Chicken.RFID == RFID).OrderByDescending(w => w.ExitWeight).ToListAsync();
		

	}
}
