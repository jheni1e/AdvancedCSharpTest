using RideClub.Models;
using RideClub.Services.Profile;

namespace RideClub.UseCases.CreateProfile;

public class CreateProfileUseCase
(
    IProfileService service
)
{
    public async Task<Result<CreateProfileResponse>> Do(CreateProfilePayload payload)
    {
        var username = service.GetProfile(payload.Username);
        if (username is not null)
            return Result<CreateProfileResponse>.Fail("Username already in use.");

        var user = new User
        {
            Name = payload.Name,
            UserName = payload.Username,
            Password = payload.Password
        };

        await service.CreateProfile(user);
        return Result<CreateProfileResponse>.Success(new());
    }
}