using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmPulse.Core.Models
{
	public class Statistics
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public double AverageWeight { get; set; }
		public double MortalityRate { get; set; }
		public double SurvivalRate { get; set; }
	}
}
