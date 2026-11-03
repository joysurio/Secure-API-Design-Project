namespace LFS.SecureAPI.Services;

/// <summary>
/// Minimal columns for issuing or refreshing SubCliente tokens. Avoids materializing the full
/// <c>MasterSubClientes</c> row (e.g. <c>image</c>/<c>rowversion</c> columns, fixed-length text)
/// which can throw for some legacy rows while others succeed.
/// </summary>
internal sealed record SubClienteAuthSlice(
    int Id,
    string? Nombre,
    string? Apellido1,
    string? Dni,
    string? IdEmpleado);
