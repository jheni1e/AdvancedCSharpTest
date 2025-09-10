using Microsoft.EntityFrameworkCore;

namespace RideClub.Models;

public class RideClubDbContext(DbContextOptions<RideClubDbContext> opts) : DbContext(opts)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Ride> Rides => Set<Ride>();
    public DbSet<Point> Points => Set<Point>();
    public DbSet<RidePoint> RidePoints => Set<RidePoint>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<User>()
            .HasMany(u => u.Rides)
            .WithOne(r => r.Creator)
            .HasForeignKey(r => r.CreatorID)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Ride>()
            .HasMany(r => r.Points)
            .WithOne(p => p.Ride)
            .HasForeignKey(r => r.PointID)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Point>()
            .HasMany(r => r.Rides)
            .WithOne(p => p.Point)
            .HasForeignKey(r => r.RideID)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<RidePoint>()
            .HasOne(r => r.Ride)
            .WithMany(p => p.Points)
            .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<RidePoint>()
            .HasOne(r => r.Point)
            .WithMany(p => p.Rides)
            .OnDelete(DeleteBehavior.NoAction);
    }
}