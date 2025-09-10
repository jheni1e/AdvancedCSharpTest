using Microsoft.EntityFrameworkCore;
using RideClub.Models;

namespace RideClub.Services.Profile;

public class EFProfileService(RideClubDbContext ctx) : IProfileService
{
    public async Task<int> CreateProfile(User Profile)
    {
        ctx.Users.Add(Profile);
        await ctx.SaveChangesAsync();
        return Profile.ID;
    }

    public async Task<User?> GetProfile(string Username)
    {
        return await ctx.Users.FirstOrDefaultAsync(
            p => p.UserName == Username
        );
    }

    public async Task<User?> GetProfileByID(int ID)
    {
        return await ctx.Users.FirstOrDefaultAsync(
            p => p.ID == ID
        );
    }
}