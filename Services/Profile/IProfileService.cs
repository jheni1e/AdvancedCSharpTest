using RideClub.Models;

namespace RideClub.Services.Profile;

public interface IProfileService
{
    Task<int> CreateProfile(User Profile);
    Task<User?> GetProfile(string Username);
    Task<User?> GetProfileByID(int ID);
}