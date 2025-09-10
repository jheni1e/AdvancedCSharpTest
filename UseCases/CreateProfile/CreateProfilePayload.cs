namespace RideClub.UseCases.CreateProfile;

public record CreateProfilePayload (
    string Name,
    string Username,
    string Password
);