namespace RideClub.UseCases.GetRide;

public record GetRideResponse (
    string Title,
    string Description,
    IEnumerable<string> Points,
    string CreatorName
);