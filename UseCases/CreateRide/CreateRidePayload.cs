using System.ComponentModel.DataAnnotations;
using RideClub.Models;

namespace RideClub.UseCases.CreateRide;

public record CreateRidePayload
{
    [Required]
    public int CreatorID { get; init; }

    [Required]
    [MaxLength(20)]
    public string Title { get; init; }

    [Required]
    [MinLength(40)]
    [MaxLength(200)]
    public string Description { get; init; }
}