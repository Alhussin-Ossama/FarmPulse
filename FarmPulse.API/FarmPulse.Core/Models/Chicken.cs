using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmPulse.Core.Models
{
	public class Chicken
	{
		public int ChickenId { get; set; }
		public string RFID { get; set; }
		public double CurrentWeight { get; set; }
		public string ActivityStatus { get; set; }
		public DateTime DateOfBirth { get; set; } = DateTime.Now;


		public IEnumerable<Weight> Weights { get; set; }
		public IEnumerable<Notification> Notifications { get; set; }
	}
}
