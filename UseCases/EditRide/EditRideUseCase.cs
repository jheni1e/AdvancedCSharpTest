using Microsoft.EntityFrameworkCore;
using RideClub.Models;
using RideClub.Services.Profile;
using RideClub.Services.Rides;

namespace RideClub.UseCases.EditRide;

public class EditRideUseCase
(
    IRideService rideService,
    IProfileService profileService,
    RideClubDbContext ctx
)
{
    public async Task<Result<EditRideResponse>> Do(EditRidePayload payload)
    {
        var ride = await rideService.GetRideByID(payload.RideID);
        if (ride is null)
            return Result<EditRideResponse>.Fail("Ride not found.");
        
        var creator = await profileService.GetProfileByID(payload.CreatorID);
        if (creator is null)
            return Result<EditRideResponse>.Fail("Creator not found.");

        if (creator != ride.Creator)
            return Result<EditRideResponse>.Fail("User is not the ride creator.");
        
        var point = await ctx.Points.FirstOrDefaultAsync(p => p.ID == payload.PointID);
        if (point is null)
            return Result<EditRideResponse>.Fail("Point not found.");

        ride.Points.Add(new RidePoint
        {
            RideID = ride.ID,
            PointID = point.ID,
            Ride = ride,
            Point = point
        });

        await ctx.SaveChangesAsync();
        
        return Result<EditRideResponse>.Success(null);
    }
}