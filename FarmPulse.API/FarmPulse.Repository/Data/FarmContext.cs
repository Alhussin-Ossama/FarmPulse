using FarmPulse.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FarmPulse.Repository.Data
{
	public class FarmContext : DbContext
	{
		public FarmContext(DbContextOptions<FarmContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Chicken> Chicken { get; set; }
		public DbSet<Weight> Weight { get; set; }
        public DbSet<Statistics> Statistic { get; set; }
        public DbSet<Notification> Notification { get; set; }
	}
}
