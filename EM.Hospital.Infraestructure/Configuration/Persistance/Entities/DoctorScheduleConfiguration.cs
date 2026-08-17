using EM.Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EM.Hospital.Infraestructure.Configuration.Persistance.Entities
{
    public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
    {
        public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
        {
            builder.ToTable("doctor_schedules");

            builder.HasKey(ds => ds.Id);

            builder.Property(ds => ds.DayOfWeek)
                .HasColumnName("day_of_week")
                .IsRequired();

            builder.OwnsOne(ds => ds.Schedule, s =>
            {
                s.Property(p => p.Start).HasColumnName("start");
                s.Property(p => p.End).HasColumnName("end");
            });
        }
    }
}
