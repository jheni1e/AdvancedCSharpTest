using System.Security.Claims;
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
            HttpContext http,
            [FromBody] CreateRidePayload payload,
            [FromServices] CreateRideUseCase useCase) =>
        {
            var claim = http.User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim is null)
                return Results.Unauthorized();
                
            var result = await useCase.Do(payload);
            if (result.IsSuccess)
                return Results.Ok();
            return Results.BadRequest(result.Reason);
        }).RequireAuthorization();

        app.MapPut("edit/ride", async (
            HttpContext http,
            [FromBody] EditRidePayload payload,
            [FromServices] EditRideUseCase useCase) =>
        {
            var claim = http.User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim is null)
                return Results.Unauthorized();

            var result = await useCase.Do(payload);
            if (result.IsSuccess)
                return Results.Ok();
            return Results.BadRequest(result.Reason);
        }).RequireAuthorization();

        app.MapGet("ride/{rideID}", async (
            int rideID,
            [FromServices] GetRideUseCase useCase) =>
        {
            var result = await useCase.Do(new GetRidePayload(rideID));
            if (result.IsSuccess)
                return Results.Ok();
            return Results.BadRequest(result.Reason);
        });
    }
}