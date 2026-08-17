using EM.Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EM.Hospital.Infraestructure.Configuration.Persistance.Entities
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("doctors");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(d => d.LastName).HasMaxLength(100).IsRequired();
            builder.Property(d => d.Email).HasMaxLength(200).IsRequired(false);
            builder.Property(d => d.Phone).HasMaxLength(50).IsRequired(false);
            builder.Property(d => d.SpecialityId).HasColumnName("speciality_id");

            // Relationship to Specialty (no navigation on Specialty side)
            builder.HasOne<Specialty>().WithMany().HasForeignKey(d => d.SpecialityId).OnDelete(DeleteBehavior.Restrict);

            // Map schedules as a regular one-to-many using a shadow FK on DoctorSchedule
            builder.HasMany(d => d.DoctorSchedules)
                   .WithOne()
                   .HasForeignKey("DoctorId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
