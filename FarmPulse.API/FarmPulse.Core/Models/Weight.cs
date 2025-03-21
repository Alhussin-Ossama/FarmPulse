using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmPulse.Core.Models
{
	public class Weight
	{
		public int WeightID { get; set; }
		public double EntryWeight { get; set; }
		public double ExitWeight { get; set; } = 0;
		public DateTime EntryTime { get; set; } = DateTime.Now;
		public DateTime ExitTime { get; set; } = DateTime.Now;
		public int ChickenId { get; set; }
		public Chicken Chicken { get; set; }
	}
}
