using EM.Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EM.Hospital.Infraestructure.Configuration.Persistance.Entities
{
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("prescriptions");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.AppointmentId).HasColumnName("appointment_id");
            builder.Property(p => p.Medications).HasColumnName("medications").HasMaxLength(2000);
            builder.Property(p => p.Instructions).HasColumnName("instructions").HasMaxLength(4000);
            builder.Property(p => p.IssuedAt).HasColumnName("issued_at");

            
            builder.HasOne(p => p.appointment)
                .WithOne(a => a.Prescription)
                .HasForeignKey<Prescription>(p => p.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
