using Microsoft.EntityFrameworkCore;
using RideClub.Models;
using RideClub.Services.Rides;

namespace RideClub.UseCases.GetRide;

public class GetRideUseCase
(
    RideClubDbContext ctx
)
{
    public async Task<Result<GetRideResponse>> Do(GetRidePayload payload)
    {
        var rides = await ctx.Rides
            .Include(r => r.Title)
            .Include(r => r.Description)
            .Include(r => r.Points)
                .ThenInclude(p => p.Point.Name)
            .Include(r => r.Creator)
                .ThenInclude(c => c.Name)
            .Where(r => r.ID == payload.RideID)
            .ToListAsync();

        if (!rides.Any())
            return Result<GetRideResponse>.Fail("There are no rooms.");

        var ridepoint = await ctx.RidePoints.Where(r => r.RideID == payload.RideID).ToListAsync();
        if (ridepoint is null)
            return Result<GetRideResponse>.Fail("There are no ride/points relation.");

        var points = new List<string>();
        foreach (var p in ridepoint)
            points.Add(p.Point.Name);

        var response = rides.Select(r => new GetRideResponse(
            r.Title,
            r.Description,
            points,
            r.Creator.Name
        ));

        return Result<GetRideResponse>.Success((GetRideResponse)response);
    }
}