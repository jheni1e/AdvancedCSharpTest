using Microsoft.EntityFrameworkCore;
using RideClub.Models;
using RideClub.Services.Rides;

namespace RideClub.UseCases.GetRide;

public class GetRideUseCase
(
    IRideService service,
    RideClubDbContext ctx
)
{
    public async Task<Result<GetRideResponse>> Do(GetRidePayload payload)
    {
        var ride = await service.GetRideByID(payload.RideID);

        if (ride is null)
            return Result<GetRideResponse>.Fail("There are no rooms.");

        var ridepoint = await ctx.RidePoints.Where(r => r.RideID == payload.RideID).ToListAsync();
        if (ridepoint is null)
            return Result<GetRideResponse>.Fail("There are no ride/points relation.");

        var points = new List<string>();
        foreach (var p in ridepoint)
            points.Add(p.Point.Name);

        var response = new GetRideResponse(
            ride.Title,
            ride.Description,
            points,
            ride.Creator.Name
        );

        return Result<GetRideResponse>.Success(response);
    }
}