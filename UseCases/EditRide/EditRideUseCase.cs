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
            
        // Editar um passeio adicionando um novo ponto turístico ao passeio,
        // enviando o ID do ponto turístico e o ID do passeio.
        // Isso só é possível para o usuário que criou o passeio.

        // user.Bio = payload.Bio ?? user.Bio;
        // user.Email = payload.Email ?? user.Email;
        // user.ImageURL = payload.ImageURL ?? user.ImageURL;
        // user.Username = payload.Username ?? user.Username;

        await ctx.SaveChangesAsync();
        
        return Result<EditRideResponse>.Success(null);
    }
}