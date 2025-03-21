using FarmPulse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmPulse.Core.Interfaces
{
	public interface IStatisticsRepository
	{
		Task<IEnumerable<Statistics>> GetStatisticsAsync(string period);
	}
}
