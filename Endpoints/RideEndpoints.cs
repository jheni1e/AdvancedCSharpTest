using Microsoft.AspNetCore.Mvc;
using RideClub.UseCases.CreateRide;
using RideClub.UseCases.EditRide;
using RideClub.UseCases.GetRide;

namespace RideClub.Endpoints;

public static class RideEndpoints
{
    public static void ConfigureRideEndpoints(this WebApplication app)
    {
        app.MapPost("create/ride", async (
            [FromBody] CreateRidePayload payload,
            [FromServices] CreateRideUseCase useCase) =>
        {
            var result = await useCase.Do(payload);
            if (result.IsSuccess)
                return Results.Ok();
            return Results.BadRequest(result.Reason);
        });

        app.MapPut("edit/ride", async (
            [FromBody] EditRidePayload payload,
            [FromServices] EditRideUseCase useCase) =>
        {
            var result = await useCase.Do(payload);
            if (result.IsSuccess)
                return Results.Ok();
            return Results.BadRequest(result.Reason);
        });

        app.MapPost("ride", async (
            [FromBody] GetRidePayload payload,
            [FromServices] GetRideUseCase useCase) =>
        {
            var result = await useCase.Do(payload);
            if (result.IsSuccess)
                return Results.Ok();
            return Results.BadRequest(result.Reason);
        });
    }
}