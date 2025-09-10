using RideClub.Models;

namespace RideClub.UseCases.CreateRide;

public record CreateRidePayload (
    int CreatorID,
    string Title,
    string Description
);