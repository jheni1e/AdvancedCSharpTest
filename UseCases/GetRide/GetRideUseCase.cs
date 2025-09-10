using Microsoft.EntityFrameworkCore;
using RideClub.Models;
using RideClub.Services.Rides;

namespace RideClub.UseCases.GetRide;

public class GetRideUseCase
(
    IRideService rideService,
    RideClubDbContext ctx
)
{
    public async Task<Result<GetRideResponse>> Do(GetRidePayload payload)
    {
        var rides = await ctx.Rides
            .Include(r => r.Title)
            .Include(r => r.Description)
            .Include(r => r.Creator)
                .ThenInclude(c => c.Name)
            .Include(r => r.Points)
                .ThenInclude(p => p.Point.Name)
            .Where(r => r.ID == payload.RideID)
            .ToListAsync();

        if (!rides.Any())
            return Result<GetRideResponse>.Fail("There are no rooms.");

        var response = rides.Select(r => new GetRideResponse
        {
            Title = r.Title,
            Description = r.Description,
            Points = p.Name, //ienumerable de strings dos nomes do pontos
            CreatorName = r.Creator.Name
        });

        return Result<GetRideResponse>.Success(response);
    }
}