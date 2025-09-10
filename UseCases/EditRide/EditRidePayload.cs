namespace RideClub.UseCases.EditRide;

public record EditRidePayload (
    int PointID,
    int RideID,
    int CreatorID
);