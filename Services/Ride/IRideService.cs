using RideClub.Models;

namespace RideClub.Services.Rides;

public interface IRideService
{
    Task<int> CreateRide(Ride Ride);
    Task<Ride?> GetRideByID(int ID);
}