namespace RideClub.UseCases.GetRide;

public record GetRideResponse (
    string Title,
    string Description,
    string CreatorName,
    List<string> Points
);