using RideClub.Models;
using RideClub.Services.Profile;
using RideClub.Services.Rides;

namespace RideClub.UseCases.CreateRide;

public class CreateRideUseCase
(
    IRideService rideService,
    IProfileService profileService
)
{
    public async Task<Result<CreateRideResponse>> Do(CreateRidePayload payload)
    {
        var creator = await profileService.GetProfileByID(payload.CreatorID);
        if (creator is null)
            return Result<CreateRideResponse>.Fail("Creator doesn't exist.");

        var ride = new Ride
        {
            Title = payload.Title,
            Description = payload.Description,
            CreatorID = payload.CreatorID,
            Creator = creator
        };

        await rideService.CreateRide(ride);
        return Result<CreateRideResponse>.Success(new());
    }
}