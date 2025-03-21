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
	public class WeightConfiguration : IEntityTypeConfiguration<Weight>
	{
		public void Configure(EntityTypeBuilder<Weight> builder)
		{
			builder.HasOne(W => W.Chicken)
			   .WithMany(C => C.Weights)
			   .HasForeignKey(W => W.ChickenId);

			builder.Property(W=> W.EntryWeight).IsRequired();
			builder.Property(W => W.ExitWeight).IsRequired();
			builder.Property(W => W.EntryTime).IsRequired();
			builder.Property(W => W.ExitTime).IsRequired();
		}
	}
}
