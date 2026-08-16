using System;
using System.Collections.Generic;
using System.Text;
using EM.Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EM.Hospital.Infraestructure.Configuration.Persistance.Context
{
    //public class HospitalDbContext(DbContextOptions<HospitalDbContext> options) : DbContext(options)
    //{
    //    public DbSet<Specialty> Specialties => Set<Specialty>();
    //    public DbSet<Patient> Patients => Set<Patient>();
    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HospitalDbContext).Assembly);
    //        base.OnModelCreating(modelBuilder);
    //    }
    //}
    public class HospitalDbContext : DbContext//(DbContextOptions<PlanillaDbContext> options) : DbContext(options)
    {
        public HospitalDbContext()
        {
        }
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=1600;Database=dbhospital;Username=admin;Password=Password2026");
        }
        public DbSet<Specialty> specialties { get; set; }
        //public DbSet<Patient> patients { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("hospital");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HospitalDbContext).Assembly);
        }
    }
}
