using RideClub.Services.JWT;
using RideClub.Services.Profile;

namespace RideClub.UseCases.Login;

public class LoginUseCase
(
    IJWTService jWTService,
    IProfileService profileService
)
{
    public async Task<Result<LoginResponse>> Do(LoginPayload payload)
    {
        var user = await profileService.GetProfile(payload.Login);
        if (user is null)
            return Result<LoginResponse>.Fail("User not found");

        if (payload.Password != user.Password)
            return Result<LoginResponse>.Fail("User not found");

        var jwt = jWTService.CreateToken(
            new(
            user.ID,
            user.UserName
        ));

        return Result<LoginResponse>.Success(new LoginResponse(jwt));
    }
}