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
	public class StatisticsRepository : GenericRepository<Statistics>, IStatisticsRepository
	{
		private readonly FarmContext _context;

		public StatisticsRepository(FarmContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Statistics>> GetStatisticsAsync(string period)
		{
			var chickens = await _context.Chicken.ToListAsync();
			var Weights = await _context.Weight.ToListAsync();
			var totalChickens = chickens.Count;
			var deadChickens = chickens.Count(c => c.ActivityStatus == ActivityStatus.Dead.ToString());
			var aliveChickens = totalChickens - deadChickens;

			DateTime startDate = period switch
			{
				"daily" => DateTime.Today,
				"weekly" => DateTime.Today.AddDays(-7),
				"month" => DateTime.Today.AddMonths(-1),
				"yearly" => DateTime.Today.AddYears(-1),
				_ => DateTime.Today
			};

			var filteredWeights = Weights.Where(w => w.EntryTime >= startDate).Select(w => w.ExitWeight);
			return new List<Statistics>
			{
				new Statistics
				{
					Date = DateTime.Today,
					AverageWeight = filteredWeights.Any() ? filteredWeights.Average() : 0,
					MortalityRate = totalChickens > 0 ? (double)deadChickens / totalChickens * 100 : 0,
					SurvivalRate = totalChickens > 0 ? (double)aliveChickens / totalChickens * 100 : 0
				}
			};
		}
	}
}
