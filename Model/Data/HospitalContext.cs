using Microsoft.EntityFrameworkCore;
using Model.EFModel;

namespace Model.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<AppointmentTime> AppointmentTimes { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=HospitalMVVM;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                        .Property(p => p.Id)
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<Appointment>()
                        .Property(p => p.Id)
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<AppointmentTime>()
                        .Property(p => p.Id)
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<Doctor>()
                        .Property(p => p.Id)
                        .ValueGeneratedOnAdd();
        }
    }
}
