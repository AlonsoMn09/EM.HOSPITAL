using EM.Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EM.Hospital.Infraestructure.Configuration.Persistance.Entities
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.AppointmentId).HasColumnName("appointment_id");

            builder.Property(p => p.Date).HasColumnName("date");

            builder.Property(p => p.Method).HasColumnName("method");

            builder.Property(p => p.Status).HasColumnName("status");

            builder.OwnsOne(p => p.Amount, a =>
            {
                a.Property(x => x.Amount).HasColumnName("amount");
                a.Property(x => x.Currency).HasColumnName("currency").HasMaxLength(10);
            });
        }
    }
}
