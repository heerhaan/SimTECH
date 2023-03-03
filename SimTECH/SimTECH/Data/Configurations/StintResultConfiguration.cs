﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class StintResultConfiguration : IEntityTypeConfiguration<StintResult>
    {
        public void Configure(EntityTypeBuilder<StintResult> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Stint)
                .WithMany(e => e.StintResults)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
