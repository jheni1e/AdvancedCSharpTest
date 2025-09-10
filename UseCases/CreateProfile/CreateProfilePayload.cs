using System.ComponentModel.DataAnnotations;

namespace RideClub.UseCases.CreateProfile;

public record CreateProfilePayload
{
    [Required]
    public string Name { get;  init; }
    
    [Required]
    public string Username { get;  init; }

    [Required]
    public string Password { get;  init; }
}