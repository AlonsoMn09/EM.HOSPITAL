using EM.Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EM.Hospital.Infraestructure.Configuration.Persistance.Entities
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("appointments");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.PatientId).HasColumnName("patient_id");
            builder.Property(a => a.DoctorId).HasColumnName("doctor_id");
            builder.Property(a => a.ScheduledAt).HasColumnName("scheduled_at");
            builder.Property(a => a.Status).HasColumnName("status");

            builder.HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-one Payment
            builder.HasOne(a => a.Payment)
                .WithOne()
                .HasForeignKey<Payment>(p => p.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-one Prescription
            builder.HasOne(a => a.Prescription)
                .WithOne()
                .HasForeignKey<Prescription>(p => p.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
