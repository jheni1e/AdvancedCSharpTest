namespace RideClub.UseCases.GetRide;

public record GetRideResponse (
    string Title,
    string Description,
    List<string> Points,
    string CreatorName
);