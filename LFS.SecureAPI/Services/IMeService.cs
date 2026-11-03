using LFS.SecureAPI.Models;

namespace LFS.SecureAPI.Services
{
    /// <summary>
    /// User profile and preferences service per CTO MVP spec.
    /// </summary>
    public interface IMeService
    {
        Task<MeResponse> GetProfileAsync(string userId);
        Task<MeCreditsResponse> GetCreditsAsync(string userId, string? languageCode = null);
        Task<IReadOnlyList<MachineStatusDto>> GetMachineStatusAsync(string userId, string? languageCode = null);
        Task<IReadOnlyList<MachineWarehouseStockDto>> GetMachineWarehouseStockAsync(string userId, int machineId, string? languageCode = null);
        Task<MachineConsumptionDto> GetMachineConsumptionAsync(string userId, string period, string? languageCode = null);
        Task<List<SizePreferenceDto>> GetSizesAsync(string userId);
        Task UpdateSizesAsync(string userId, List<SizePreferenceDto> sizes);
        Task<List<SubClienteGarmentSizeDto>> GetSubClienteGarmentSizesAsync(string userId, string? languageCode = null);
        Task UpdateSubClienteGarmentSizesAsync(string userId, IReadOnlyList<SubClienteGarmentSizeUpdateDto> updates);
        Task<MovementsResponse> GetMovementsAsync(string userId, DateTime? from, DateTime? to, string? type, int? modelId, int page, int size, string? languageCode = null);
        Task<MovementsResponse> GetAdminMovementsAsync(
            string adminUserId,
            DateTime? from,
            DateTime? to,
            int? machineId,
            int? modelId,
            string? talla,
            int? userId,
            string? type,
            int page,
            int size,
            string? languageCode = null);

        /// <summary>Admin: movement types from <c>MasterTipoAntena</c> for Consumption filters.</summary>
        Task<IReadOnlyList<AdminMovementTypeDto>> GetAdminMovementTypesAsync(string adminUserId, string? languageCode = null);

        /// <summary>Admin: search <c>MasterSubClientes</c> in the same client as the signed-in admin.</summary>
        Task<IReadOnlyList<AdminSubClienteSummaryDto>> SearchSubClientesForAdminAsync(string adminUserId, string? search, int take);

        /// <summary>Admin: credits for a sub-client with per-model limits from <c>MovCreditosModelosDepartamentos</c>.</summary>
        Task<AdminUserCreditsDetailDto?> GetSubClienteCreditsDetailForAdminAsync(string adminUserId, int subClienteId, string? languageCode = null);

        /// <summary>
        /// Admin: credit history for one sub-client from <c>MovStockPrendasUsu_Historico</c>
        /// (audit trail of <c>MovStockPrendasUsu</c>), filterable by date range and model.
        /// </summary>
        Task<AdminCreditHistoryResponse> GetSubClienteCreditHistoryForAdminAsync(
            string adminUserId,
            int subClienteId,
            DateTime? from,
            DateTime? to,
            int? modelId,
            int page,
            int size,
            string? languageCode = null);

        /// <summary>Admin: update <c>MovStockPrendasUsu</c> and append <c>MovStockPrendasUsu_Historico</c>.</summary>
        Task UpdateSubClienteCreditForAdminAsync(string adminUserId, int subClienteId, AdminCreditUpdateRequestDto body);
    }

    public sealed class AdminSubClienteSummaryDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string? RoleLabel { get; set; }
    }

    public sealed class AdminMovementTypeDto
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
    }

    public sealed class AdminUserCreditsDetailDto
    {
        public int SubClienteId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string? RoleLabel { get; set; }
        /// <summary>Sum of per-model <c>LimiteCantidadPrendasEnUso</c> for the user&apos;s department (max per model, then sum).</summary>
        public int? TotalConfiguredLimit { get; set; }
        public List<AdminUserCreditLineDto> Lines { get; set; } = new();
    }

    public sealed class AdminUserCreditLineDto
    {
        public int StockRowId { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public string Talla { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public int LimiteCantidadPrendasEnUso { get; set; }
    }

    public sealed class AdminCreditUpdateRequestDto
    {
        public int StockRowId { get; set; }
        public int Cantidad { get; set; }
    }

    public sealed class AdminCreditHistoryResponse
    {
        public int SubClienteId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public List<AdminCreditHistoryItemDto> Items { get; set; } = new();
        public int Total { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }

    public sealed class AdminCreditHistoryItemDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public int? MachineId { get; set; }
        public string? MachineName { get; set; }
        /// <summary><c>MovStockPrendasUsu_Historico.StockEnUsoActual</c> after the event.</summary>
        public int Quantity { get; set; }
    }

    public class MovementsResponse
    {
        public List<MovementItemDto> Items { get; set; } = new();
        public int Total { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }

    public class MovementItemDto
    {
        public int Id { get; set; }
        public int? ModelId { get; set; }
        public int? MachineId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        /// <summary><c>MovStockPrendasUsu_Historico.idLastMov</c>: 50 received, 51 dispensed, 55 dispensed lingerie, 1 web setting.</summary>
        public int? LastMovementCode { get; set; }

        public string? GarmentName { get; set; }
        public string? MachineName { get; set; }
        public string? Talla { get; set; }
        /// <summary>Display name of the sub-client (<c>MasterSubClientes</c>) for admin Consumption.</summary>
        public string? UserName { get; set; }
        /// <summary><c>MovPren.idSubCli</c> when available.</summary>
        public int? UserId { get; set; }
        /// <summary>Stock in use after this event (<c>StockEnUsoActual</c>) — unused for MovPren rows.</summary>
        public int Quantity { get; set; }
    }

    public class MeResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = "user";
        public string? DepartmentId { get; set; }
        /// <summary>Machine authentication code (<c>MasterSubClientes.Campo1</c>).</summary>
        public string? Campo1 { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        public List<CustomerSummaryDto> Customers { get; set; } = new();
    }

    public class CustomerSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class MeCreditsResponse
    {
        public List<CreditBalanceDto> Credits { get; set; } = new();
    }

    public class MachineStatusDto
    {
        public int MachineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = "dispenser";
        /// <summary>EF <c>MasterTipoMaquinaDispensadora.Descripcion</c> when available.</summary>
        public string? MachineTypeDescription { get; set; }
        public bool HasEmptyCompartment { get; set; }
        public bool HasFewGarments { get; set; }
        public bool HasLowStockPercent { get; set; }
        public int? EmptyCompartments { get; set; }
        public int? TotalCompartments { get; set; }
        public int? CurrentCollectorCount { get; set; }
        public int? CollectorLimit { get; set; }
        public double? CollectorFillPercent { get; set; }
        /// <summary>At or over configured maximum (100%+) — show FULL / red bar.</summary>
        public bool IsCollectorFull { get; set; }
        /// <summary>Above 70% of maximum but still under the limit — orange bar.</summary>
        public bool IsCollectorNearLimit { get; set; }
    }

    public class MachineWarehouseStockDto
    {
        public int IdAlmacen { get; set; }
        public int IdModelo { get; set; }
        /// <summary><c>MasterModeloPrenda.Descripcion</c> for display.</summary>
        public string? ModelName { get; set; }
        public string Talla { get; set; } = string.Empty;
        public int StockReal { get; set; }
        public int CantMax { get; set; }
    }

    public class MachineConsumptionDto
    {
        public string Period { get; set; } = "day";
        public int DispenserTotal { get; set; }
        public int CollectorTotal { get; set; }
        public List<ConsumptionModelBarDto> TopModels { get; set; } = new();
    }

    public class ConsumptionModelBarDto
    {
        public int? ModelId { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public int Qty { get; set; }
        public double Ratio { get; set; }
    }

    public class CreditBalanceDto
    {
        public string ModelId { get; set; } = string.Empty;
        public string ModelName { get; set; } = string.Empty;
        /// <summary>Current quantity in use for this model.</summary>
        public decimal Amount { get; set; }
        /// <summary>Maximum allowed garments for this model/department (null = unlimited).</summary>
        public int? MaxAmount { get; set; }
        /// <summary>Size breakdown for SubCliente garments.</summary>
        public string? Size { get; set; }
    }

    public class SizePreferenceDto
    {
        public string ModelId { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
    }
}
