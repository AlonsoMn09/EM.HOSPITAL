using EM.Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Infraestructure.Configuration.Persistance.Entities
{
    public class SpecialityConfiguration : IEntityTypeConfiguration<Specialty>
    {        
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.ToTable("Speciality");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.UpdatedAt).IsRequired(false);
            builder.Property(a => a.UpdatedBy).IsRequired(false);

            builder.Property(a => a.Description)
                .HasMaxLength(200);

        }
    }
}
