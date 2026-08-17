using EM.Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Infraestructure.Configuration.Persistance.Entities
{
    public class PatientConfiguration : IEntityTypeConfiguration<EM.Hospital.Domain.Entities.Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patient");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Phone)
                .IsRequired()
                .HasMaxLength(50);

            builder.OwnsOne(p => p.Document, d =>
            {
                d.Property(p => p.Type)
                    .HasColumnName("DocumentType")
                    .HasMaxLength(3);

                d.Property(p => p.Document)
                    .HasColumnName("DocumentNumber")
                    .HasMaxLength(15);

                d.HasIndex(doc => new { doc.Type, doc.Document }).IsUnique();
            });

            builder.Property(a => a.UpdatedAt).IsRequired(false);
            builder.Property(a => a.UpdatedBy).IsRequired(false);

            builder.HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
