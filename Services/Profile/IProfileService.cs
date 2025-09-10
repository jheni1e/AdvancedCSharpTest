using RideClub.Models;

namespace RideClub.Services.Profile;

public interface IProfileService
{
    Task<int> CreateProfile(User profile);
    Task<User?> GetProfile(string username);
}