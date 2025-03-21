using FarmPulse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FarmPulse.Core.Interfaces
{
	public interface IChickenRepository : IGenericRepository<Chicken>
	{
		Task<Chicken> GetByRFIDAsync(string RFID);
		Task<IEnumerable<Chicken>> GetFilteredChickens(Expression<Func<Chicken, bool>> filter);
		Task<int> GetAliveChickenCountAsync();
		Task<int> GetDeadChickenCountAsync();
	}
}
