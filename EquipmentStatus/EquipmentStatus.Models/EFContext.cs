using System.Data.Entity;
using EFmodel;

namespace EquipmentStatus.Models
{
    public partial class EFContext : DbContext
    {
        public EFContext()
            : base("name=EFDBConntext")
        {
        }

        public virtual DbSet<Cmaeras> Cmaeras { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<DVRInfoChecks> DVRInfoChecks { get; set; }
        public virtual DbSet<DVRs> DVRs { get; set; }
        public virtual DbSet<MonitorRooms> MonitorRooms { get; set; }
        public virtual DbSet<AlarmHost> AlarmHosts { get; set; }
        public virtual DbSet<AlarmManage> AlarmManages { get; set; }
        public virtual DbSet<Alarm> Alarms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departments>()
               .HasMany(e => e.Cmaeras)
               .WithRequired(e => e.Departments)
               .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<DVRs>()
                .HasMany(e => e.Cmaeras)
                .WithRequired(e => e.DVRs)
                .HasForeignKey(e => e.DVRId);

            modelBuilder.Entity<DVRs>()
                .HasMany(e => e.DVRInfoChecks)
                .WithRequired(e => e.DVRs)
                .HasForeignKey(e => e.DVRId);

            modelBuilder.Entity<MonitorRooms>()
                .HasMany(e => e.DVRs)
                .WithRequired(e => e.MonitorRooms)
                .HasForeignKey(e => e.monitorRoomId);
            modelBuilder.Entity<MonitorRooms>()
              .HasMany(e => e.AlarmHosts)
              .WithRequired(e => e.MonitorRooms)
              .HasForeignKey(e => e.monitorRoomId);
            modelBuilder.Entity<AlarmHost>()
                .HasMany(e => e.Alarms)
                .WithRequired(e => e.AlarmHost)
                .HasForeignKey(e => e.AlarmHostID);
          
        }
    }
}
