using Microsoft.EntityFrameworkCore;
using RideClub.Models;

namespace RideClub.Services.Rides;

public class EFRideService(RideClubDbContext ctx) : IRideService
{
    public async Task<int> CreateRide(Ride Ride)
    {
        ctx.Rides.Add(Ride);
        await ctx.SaveChangesAsync();
        return Ride.ID;
    }

    public async Task<Ride?> GetRideByID(int ID)
    {
        return await ctx.Rides.FirstOrDefaultAsync(
            p => p.ID == ID
        );
    }
}