using FarmPulse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmPulse.Core.Interfaces
{
	public interface IWeightRepository:IGenericRepository<Weight>
	{
		Task<Weight> GetExitWeightByChickenIdAsync(int id);
		Task<IEnumerable<Weight>> GetWeightHistoryByRFIDAsync(string RFID);
	}
}
