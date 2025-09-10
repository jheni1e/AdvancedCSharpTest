using Microsoft.AspNetCore.Mvc;
using RideClub.UseCases.CreateProfile;

namespace RideClub.Endpoints;

public static class UserEndpoints
{
    public static void ConfigureUserEndpoints(this WebApplication app)
    {
        app.MapPost("create/profile", async (
            [FromBody] CreateProfilePayload payload,
            [FromServices] CreateProfileUseCase useCase) =>
        {
            var result = await useCase.Do(payload);
            if (result.IsSuccess)
                return Results.Ok();
            return Results.BadRequest(result.Reason);
        });
    }
}