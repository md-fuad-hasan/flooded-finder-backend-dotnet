using flooded_finder_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace flooded_finder_backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Division> Divisions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Upazila> Upazilas { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<UserArea> UserAreas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Division>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name)
                      .IsRequired();
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name)
                      .IsRequired();

                entity.HasOne(o => o.Division)
                      .WithMany(m => m.Districts)
                      .HasForeignKey(o => o.DivisionId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Upazila>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name)
                      .IsRequired();

                entity.HasOne(o => o.Division)
                      .WithMany(m => m.Upazilas)
                      .HasForeignKey(o => o.DivisionId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.District)
                      .WithMany(m => m.Upazilas)
                      .HasForeignKey(o => o.DistrictId)
                      .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name)
                      .IsRequired();

                entity.HasOne(o => o.Upazila)
                      .WithMany(m => m.Areas)
                      .HasForeignKey(o => o.UpazilaId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.Division)
                      .WithMany(m => m.Areas)
                      .HasForeignKey(o => o.DivisionId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.District)
                      .WithMany(m => m.Areas)
                      .HasForeignKey(o => o.DistrictId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<UserArea>(entity =>
            {
                entity.HasKey(ua => new { ua.UserId, ua.AreaId });

                entity.HasOne(u => u.AppUser)
                      .WithMany(ua => ua.UserAreas)
                      .HasForeignKey(u => u.UserId);

                entity.HasOne(a => a.Area)
                      .WithMany(ua => ua.UserAreas)
                      .HasForeignKey(a => a.AreaId);
            });

        }
    }
}
