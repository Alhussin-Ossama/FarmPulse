using FarmPulse.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmPulse.Repository.Data.Configurations
{
	public class ChickenConfiguration : IEntityTypeConfiguration<Chicken>
	{
		public void Configure(EntityTypeBuilder<Chicken> builder)
		{
			builder.HasMany(C => C.Weights)
			   .WithOne(W => W.Chicken)
			   .HasForeignKey(W => W.ChickenId);

			builder.HasMany(C => C.Notifications)
				.WithOne(N => N.Chicken)
				.HasForeignKey(N => N.ChickenId);

			builder.Property(C => C.RFID).IsRequired();
			builder.Property(C => C.CurrentWeight).IsRequired();
			builder.Property(C => C.ActivityStatus).IsRequired();
			builder.Property(C => C.DateOfBirth).IsRequired();
		}
	}
}
