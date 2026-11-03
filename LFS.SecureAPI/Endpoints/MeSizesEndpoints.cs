using LFS.SecureAPI.Models;
using LFS.SecureAPI.Serialization;
using LFS.SecureAPI.Services;
using System.Security.Claims;
using System.Text.Json;

namespace LFS.SecureAPI.Endpoints;

/// <summary>
/// Size routes registered as minimal APIs (same mechanism as /api/health) so IIS/proxy setups
/// that fail to match controller attribute routes still resolve these paths.
/// </summary>
public static class MeSizesEndpoints
{
    public static WebApplication MapMeSizesEndpoints(this WebApplication app)
    {
        // No RequireAuthorization here: validate inside handlers so routing always matches (avoids host-specific 404s).
        app.MapGet("/api/me/sizes", GetMeSizes).AllowAnonymous();
        app.MapGet("/api/subcliente/me-sizes", GetMeSizes).AllowAnonymous();

        app.MapPut("/api/me/sizes", PutMeSizes).AllowAnonymous();
        app.MapPut("/api/subcliente/me-sizes", PutMeSizes).AllowAnonymous();

        app.MapGet("/api/auth/subcliente/sizes", GetSubClienteOnlySizes).AllowAnonymous();
        app.MapGet("/api/subcliente/garment-sizes", GetSubClienteOnlySizes).AllowAnonymous();

        app.MapPut("/api/auth/subcliente/sizes", PutSubClienteOnlySizes).AllowAnonymous();
        app.MapPut("/api/subcliente/garment-sizes", PutSubClienteOnlySizes).AllowAnonymous();

        // Same first path segment as /api/health — use if longer paths return 404 behind your proxy/IIS.
        app.MapGet("/api/health/subcliente-sizes", GetSubClienteOnlySizes).AllowAnonymous();
        app.MapPut("/api/health/subcliente-sizes", PutSubClienteOnlySizes).AllowAnonymous();
        app.MapGet("/api/health/me-sizes", GetMeSizes).AllowAnonymous();
        app.MapPut("/api/health/me-sizes", PutMeSizes).AllowAnonymous();

        return app;
    }

    private static string? SubClaim(ClaimsPrincipal user) =>
        user.FindFirst("sub")?.Value
        ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    private static IResult? RequireUserJwt(HttpContext ctx)
    {
        if (ctx.User?.Identity?.IsAuthenticated != true)
            return Results.Unauthorized();
        if (!ctx.User.HasClaim("auth_type", "user"))
            return Results.Json(new { error = "User JWT required (auth_type=user)." }, statusCode: StatusCodes.Status403Forbidden);
        return null;
    }

    public static async Task<IResult> GetMeSizes(HttpContext ctx, IMeService meService)
    {
        var deny = RequireUserJwt(ctx);
        if (deny != null)
            return deny;

        var userId = SubClaim(ctx.User);
        if (string.IsNullOrEmpty(userId))
            return Results.Unauthorized();
        if (userId.StartsWith("sc:", StringComparison.Ordinal))
        {
            var garments = await meService.GetSubClienteGarmentSizesAsync(userId);
            return Results.Ok(garments);
        }

        var sizes = await meService.GetSizesAsync(userId);
        return Results.Ok(sizes);
    }

    public static async Task<IResult> PutMeSizes(HttpContext ctx, IMeService me, JsonElement body)
    {
        var deny = RequireUserJwt(ctx);
        if (deny != null)
            return deny;

        var userId = SubClaim(ctx.User);
        if (string.IsNullOrEmpty(userId))
            return Results.Unauthorized();

        if (userId.StartsWith("sc:", StringComparison.Ordinal))
        {
            var garments = MeSizesJson.ParseSubClienteGarmentUpdates(body);
            if (garments == null || garments.Count == 0)
                return Results.BadRequest(new { error = "Expected a non-empty garments array or { \"garments\": [...] }." });
            try
            {
                await me.UpdateSubClienteGarmentSizesAsync(userId, garments);
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
            return Results.Ok();
        }

        List<SizePreferenceDto> prefs;
        if (body.ValueKind == JsonValueKind.Array)
        {
            prefs = JsonSerializer.Deserialize<List<SizePreferenceDto>>(body.GetRawText(), MeSizesJson.Options)
                ?? new List<SizePreferenceDto>();
        }
        else if (body.ValueKind == JsonValueKind.Object)
        {
            var wrap = JsonSerializer.Deserialize<MeSizesPutRequest>(body.GetRawText(), MeSizesJson.Options);
            prefs = wrap?.Preferences?.Select(p => new SizePreferenceDto
            {
                ModelId = p.ModelId,
                Size = p.Size
            }).ToList() ?? new List<SizePreferenceDto>();
        }
        else
            prefs = new List<SizePreferenceDto>();

        await me.UpdateSizesAsync(userId, prefs);
        return Results.Ok();
    }

    public static async Task<IResult> GetSubClienteOnlySizes(HttpContext ctx, IMeService meService)
    {
        var deny = RequireUserJwt(ctx);
        if (deny != null)
            return deny;

        var userId = SubClaim(ctx.User);
        if (string.IsNullOrEmpty(userId))
            return Results.Unauthorized();
        if (!userId.StartsWith("sc:", StringComparison.Ordinal))
            return Results.Json(new { error = "SubCliente token required" }, statusCode: StatusCodes.Status403Forbidden);

        var garments = await meService.GetSubClienteGarmentSizesAsync(userId);
        return Results.Ok(garments);
    }

    public static async Task<IResult> PutSubClienteOnlySizes(HttpContext ctx, IMeService me, JsonElement body)
    {
        var deny = RequireUserJwt(ctx);
        if (deny != null)
            return deny;

        var userId = SubClaim(ctx.User);
        if (string.IsNullOrEmpty(userId))
            return Results.Unauthorized();
        if (!userId.StartsWith("sc:", StringComparison.Ordinal))
            return Results.Json(new { error = "SubCliente token required" }, statusCode: StatusCodes.Status403Forbidden);

        var garments = MeSizesJson.ParseSubClienteGarmentUpdates(body);
        if (garments == null || garments.Count == 0)
            return Results.BadRequest(new { error = "Expected a non-empty garments array or { \"garments\": [...] }." });
        try
        {
            await me.UpdateSubClienteGarmentSizesAsync(userId, garments);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
        return Results.Ok();
    }
}
