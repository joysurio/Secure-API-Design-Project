using LFS.InfraStructure;
using Microsoft.EntityFrameworkCore;

namespace LFS.SecureAPI.Services;

/// <summary>
/// CTO rule: users listed in the ApplicationUsers table (<see cref="LFS.Shared.Entities.User"/>) must be treated as administrators on mobile.
/// </summary>
public interface IApplicationUsersAdminService
{
    Task<bool> IsListedInApplicationUsersAsync(string? dni, int? idEmployee, CancellationToken ct = default);
}

public sealed class ApplicationUsersAdminService : IApplicationUsersAdminService
{
    private readonly DataContext _db;

    public ApplicationUsersAdminService(DataContext db) => _db = db;

    public static int? ParseEmployeeId(string? idEmpleado)
    {
        if (string.IsNullOrWhiteSpace(idEmpleado))
            return null;
        return int.TryParse(idEmpleado.Trim(), System.Globalization.NumberStyles.Integer,
            System.Globalization.CultureInfo.InvariantCulture, out var id) ? id : null;
    }

    public Task<bool> IsListedInApplicationUsersAsync(string? dni, int? idEmployee, CancellationToken ct = default)
    {
        var dniNorm = string.IsNullOrWhiteSpace(dni) ? null : dni.Trim();

        // StringComparison overloads cannot be translated by EF Core; use SQL-compatible comparison instead.
        // The DB collation handles case-insensitivity, so plain == is sufficient.
        return _db.ApplicationUsers.AsNoTracking().AnyAsync(u =>
            !u.Deleted &&
            (
                (dniNorm != null
                    && u.DNI != null
                    && u.DNI.Trim() == dniNorm)
                || (idEmployee.HasValue && u.IdEmployee == idEmployee)
            ), ct);
    }
}
