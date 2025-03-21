using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmPulse.Core.Models
{
	public class Notification
	{
		public int Id { get; set; }
		public string? Message { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public bool IsRead { get; set; } = false;
		public int ChickenId { get; set; }
		public Chicken Chicken { get; set; }
	}
}
