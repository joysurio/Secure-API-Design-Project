using LFS.InfraStructure;
using LFS.SecureAPI.Models;
using LFS.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;

namespace LFS.SecureAPI.Services
{
    /// <summary>
    /// User profile, credits, and size preferences.
    /// Handles AppUser (Identity), SubCliente tokens (sub prefix "sc:"), and ApplicationUsers-only admins (sub prefix "au:").
    /// </summary>
    public class MeService : IMeService
    {
        private static readonly Dictionary<string, string> ExactTranslations = new(StringComparer.Ordinal)
        {
            ["חולצה ירוקה"] = "Green shirt",
            ["חולצה תכלת"] = "Light-blue shirt",
            ["חולצה כחול ניבי"] = "Navy-blue shirt",
            ["חולצה כחול נייבי"] = "Navy-blue shirt",
            ["חולצה נייבי"] = "Navy-blue shirt",
            ["חולצה שירות לאומי"] = "National service shirt",
            ["מכנסיים ירוקים"] = "Green pants",
            ["מכנסיים תכלת"] = "Light-blue pants",
            ["מכנסיים כחול ניבי"] = "Navy-blue pants",
            ["מכנסיים כחול נייבי"] = "Navy-blue pants",
            ["מכנסיים נייבי"] = "Navy-blue pants",
            ["שמלה ירוקה"] = "Green dress",
            ["שמלה תכלת"] = "Light-blue dress",
            ["שמלה כחול נייבי"] = "Navy-blue dress"
        };

        private static readonly Dictionary<string, string> TokenTranslations = new(StringComparer.Ordinal)
        {
            ["חולצה"] = "shirt",
            ["מכנסיים"] = "pants",
            ["ירוקה"] = "green",
            ["ירוקים"] = "green",
            ["תכלת"] = "light blue",
            ["כחול"] = "blue",
            ["ניבי"] = "navy",
            ["נייבי"] = "navy",
            ["כהה"] = "dark",
            ["בהיר"] = "light",
            ["נעל"] = "shoe",
            ["נעליים"] = "shoes",
            ["גרב"] = "sock",
            ["גרביים"] = "socks",
            ["שמלה"] = "dress",
            ["שירות"] = "service",
            ["לאומי"] = "national",
            ["חלוק"] = "gown",
            ["מעיל"] = "coat",
            ["סינר"] = "apron"
        };

        private static readonly TimeSpan PrimaryTenantCacheTtl = TimeSpan.FromMinutes(10);
        private static readonly TimeSpan DashboardCacheTtl = TimeSpan.FromMinutes(5);
        private static readonly TimeSpan ClientIdCacheTtl = TimeSpan.FromMinutes(10);
        private static readonly SemaphoreSlim PrimaryTenantGate = new(1, 1);
        private static int? _primaryTenantId;
        private static DateTime _primaryTenantCachedAtUtc;

        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IApplicationUsersAdminService _applicationUsersAdmin;
        private readonly IMemoryCache _cache;

        public MeService(
            DataContext context,
            UserManager<AppUser> userManager,
            IApplicationUsersAdminService applicationUsersAdmin,
            IMemoryCache cache)
        {
            _context = context;
            _userManager = userManager;
            _applicationUsersAdmin = applicationUsersAdmin;
            _cache = cache;
        }

        // ── Profile ────────────────────────────────────────────────────────────────

        public async Task<MeResponse> GetProfileAsync(string userId)
        {
            if (TryParseSubClienteId(userId, out var subClienteId))
                return await GetSubClienteProfileAsync(subClienteId);

            if (TryParseQrAdminUserId(userId, out var qrAdminUserId))
                return await GetAppUserProfileAsync(qrAdminUserId, forceAdmin: true).ConfigureAwait(false);

            if (TryParseApplicationUserId(userId, out var applicationUserId))
                return await GetApplicationUsersRowProfileAsync(applicationUserId).ConfigureAwait(false);

            return await GetAppUserProfileAsync(userId);
        }

        private async Task<MeResponse> GetSubClienteProfileAsync(int subClienteId)
        {
            var sc = await _context.MasterSubClientes
                .Include(s => s.IdClienteNavigation)
                .Include(s => s.IdDepartamentoClienteNavigation)
                .FirstOrDefaultAsync(s => s.Id == subClienteId);

            if (sc == null)
                throw new InvalidOperationException("SubCliente not found");

            var customers = new List<CustomerSummaryDto>();
            if (sc.IdClienteNavigation != null)
                customers.Add(new CustomerSummaryDto
                {
                    Id = sc.IdClienteNavigation.Id,
                    Name = sc.IdClienteNavigation.Nombre ?? ""
                });

            var idEmp = ApplicationUsersAdminService.ParseEmployeeId(sc.IdEmpleado);
            var isAdmin = await _applicationUsersAdmin
                .IsListedInApplicationUsersAsync(sc.Dni, idEmp)
                .ConfigureAwait(false);

            return new MeResponse
            {
                Id = $"sc:{sc.Id}",
                Email = "",
                Name = string.Join(" ", new[] { sc.Nombre, sc.Apellido1 }
                    .Where(s => !string.IsNullOrWhiteSpace(s))),
                Role = isAdmin ? "admin" : "user",
                DepartmentId = sc.IdDepartamentoCliente?.ToString(),
                Campo1 = string.IsNullOrWhiteSpace(sc.Campo1) ? null : sc.Campo1.Trim(),
                Nombre = sc.Nombre,
                Apellido1 = sc.Apellido1,
                Apellido2 = sc.Apellido2,
                Customers = customers
            };
        }

        /// <summary>Profile for rows in dbo.Users (QR admin without MasterSubClientes).</summary>
        private async Task<MeResponse> GetApplicationUsersRowProfileAsync(int applicationUserId)
        {
            var row = await _context.ApplicationUsers
                .AsNoTracking()
                .Where(u => u.Id == applicationUserId && !u.Deleted)
                .Select(u => new { u.Name, u.SurName1, u.SurName2 })
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (row == null)
                throw new InvalidOperationException("User not found");

            var name = string.Join(" ",
                new[] { row.Name, row.SurName1, row.SurName2 }.Where(s => !string.IsNullOrWhiteSpace(s)));

            return new MeResponse
            {
                Id = $"au:{applicationUserId}",
                Email = "",
                Name = name,
                Role = "admin",
                DepartmentId = null,
                Customers = new List<CustomerSummaryDto>()
            };
        }

        private async Task<MeResponse> GetAppUserProfileAsync(string userId, bool forceAdmin = false)
        {
            var user = await _userManager.Users
                .Include(u => u.UserCustomers!)
                .ThenInclude(uc => uc.MasterClientes)
                .FirstOrDefaultAsync(u => u.Id == userId)
                .ConfigureAwait(false);

            if (user == null)
            {
                var fallback = await FindUsersTableRowByIdAsync(userId).ConfigureAwait(false);
                if (fallback == null)
                    throw new InvalidOperationException("User not found");
                return new MeResponse
                {
                    Id = fallback.Value.Id,
                    Email = "",
                    Name = string.Join(" ", new[] { fallback.Value.Name, fallback.Value.SurName1, fallback.Value.SurName2 }
                        .Where(s => !string.IsNullOrWhiteSpace(s))),
                    Role = forceAdmin ? "admin" : "user",
                    DepartmentId = null,
                    Customers = new List<CustomerSummaryDto>()
                };
            }

            var customers = user.UserCustomers?
                .Select(uc => new CustomerSummaryDto
                {
                    Id = uc.MasterClientesId,
                    Name = uc.MasterClientes?.Nombre ?? ""
                })
                .ToList() ?? new List<CustomerSummaryDto>();

            var listed = await _applicationUsersAdmin
                .IsListedInApplicationUsersAsync(user.DNI, user.IdEmployee)
                .ConfigureAwait(false);
            var identityRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            var isAdmin = forceAdmin
                || listed
                || identityRoles.Any(r => string.Equals(r, "admin", StringComparison.OrdinalIgnoreCase));

            return new MeResponse
            {
                Id = user.Id,
                Email = user.Email ?? "",
                Name = user.Name ?? "",
                Role = isAdmin ? "admin" : "user",
                DepartmentId = null,
                Customers = customers
            };
        }

        // ── Credits ────────────────────────────────────────────────────────────────

        public async Task<MeCreditsResponse> GetCreditsAsync(string userId, string? languageCode = null)
        {
            if (TryParseSubClienteId(userId, out var subClienteId))
                return await GetSubClienteCreditsAsync(subClienteId, languageCode);

            if (TryParseQrAdminUserId(userId, out var qrAdminUserId))
                return await GetAppUserCreditsAsync(qrAdminUserId, languageCode);

            if (TryParseApplicationUserId(userId, out _))
                return new MeCreditsResponse { Credits = new List<CreditBalanceDto>() };

            return await GetAppUserCreditsAsync(userId, languageCode);
        }

        private async Task<MeCreditsResponse> GetSubClienteCreditsAsync(int subClienteId, string? languageCode = null)
        {
            var sc = await _context.MasterSubClientes
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == subClienteId)
                .ConfigureAwait(false);

            DepartmentCreditLimits? deptLimits = null;
            if (sc?.IdDepartamentoCliente is int deptId)
                deptLimits = await LoadDepartmentCreditLimitsAsync(deptId).ConfigureAwait(false);

            var stock = await _context.MovStockPrendasUsus
                .AsNoTracking()
                .Include(s => s.IdModeloNavigation)
                .Where(s => s.IdSubCliente == subClienteId)
                .ToListAsync()
                .ConfigureAwait(false);

            var credits = stock
                .GroupBy(s => s.IdModelo)
                .Select(g =>
                {
                    var modelNav = g.First().IdModeloNavigation;
                    return new CreditBalanceDto
                    {
                        ModelId = g.Key.ToString(),
                        ModelName = TranslateDisplayText(modelNav?.Descripcion, languageCode),
                        Amount = g.Sum(x => x.Cantidad),
                        MaxAmount = deptLimits?.ResolveForModel(g.Key)
                    };
                })
                .OrderBy(c => c.ModelName, StringComparer.OrdinalIgnoreCase)
                .ToList();

            return new MeCreditsResponse { Credits = credits };
        }

        /// <summary>
        /// Per-model limit from <c>MovCreditosModelosDepartamentos.LimiteCantidadPrendasEnUso</c>,
        /// falling back to <c>MovCreditosDepartamentos.CantMaxPrendasUso</c> when missing or zero.
        /// </summary>
        private async Task<DepartmentCreditLimits?> LoadDepartmentCreditLimitsAsync(int departmentId)
        {
            var modelLimitRows = await _context.MovCreditosModelosDepartamentos
                .AsNoTracking()
                .Where(m => m.IdDepartamento == departmentId)
                .GroupBy(m => m.IdModelo)
                .Select(g => new { ModelId = g.Key, Limite = g.Max(x => x.LimiteCantidadPrendasEnUso) })
                .ToListAsync()
                .ConfigureAwait(false);

            var deptRows = await _context.MovCreditosDepartamentos
                .AsNoTracking()
                .Where(d => d.IdDepartamento == departmentId
                            && d.CantMaxPrendasUso.HasValue
                            && d.CantMaxPrendasUso.Value > 0)
                .Select(d => d.CantMaxPrendasUso!.Value)
                .ToListAsync()
                .ConfigureAwait(false);

            return new DepartmentCreditLimits
            {
                ModelLimits = modelLimitRows.ToDictionary(x => x.ModelId, x => x.Limite),
                DepartmentFallbackMax = deptRows.Count > 0 ? deptRows.Max() : null
            };
        }

        private sealed class DepartmentCreditLimits
        {
            public Dictionary<int, int> ModelLimits { get; init; } = new();

            public int? DepartmentFallbackMax { get; init; }

            public int? ResolveForModel(int modelId)
            {
                if (ModelLimits.TryGetValue(modelId, out var modelLimit) && modelLimit > 0)
                    return modelLimit;
                return DepartmentFallbackMax;
            }
        }

        private async Task<MeCreditsResponse> GetAppUserCreditsAsync(string userId, string? languageCode = null)
        {
            var user = await _context.Users
                .Include(u => u.UserCustomers)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user?.UserCustomers == null || !user.UserCustomers.Any())
                return new MeCreditsResponse { Credits = new List<CreditBalanceDto>() };

            var clienteIds = user.UserCustomers.Select(uc => uc.MasterClientesId).ToList();
            var deptIds = await _context.MasterDepartamentosClientes
                .Where(d => d.IdCliente.HasValue && clienteIds.Contains(d.IdCliente.Value))
                .Select(d => d.Id)
                .ToListAsync();

            var deptCredits = await _context.MovCreditosModelosDepartamentos
                .Include(m => m.IdModeloNavigation)
                .Where(m => deptIds.Contains(m.IdDepartamento))
                .Take(50)
                .ToListAsync();

            var credits = deptCredits
                .Select(m => new CreditBalanceDto
                {
                    ModelId = m.IdModelo.ToString(),
                    ModelName = TranslateDisplayText(m.IdModeloNavigation?.Descripcion, languageCode),
                    Amount = m.LimiteCantidadPrendasEnUso
                })
                .ToList();

            return new MeCreditsResponse { Credits = credits };
        }

        // ── Machine status / consumption ───────────────────────────────────────────

        public async Task<IReadOnlyList<MachineStatusDto>> GetMachineStatusAsync(string userId, string? languageCode = null)
        {
            var cacheKey = $"machines:status:{userId}:{languageCode ?? "default"}";
            if (_cache.TryGetValue(cacheKey, out IReadOnlyList<MachineStatusDto>? cached) && cached != null)
                return cached;

            var clientId = await ResolveClientIdAsync(userId).ConfigureAwait(false);
            if (clientId == null)
                return Array.Empty<MachineStatusDto>();

            // Only dispensers (types 1, 5) and collectors (type 4) — MasterMaquinaDispensacion.IdTipoMaquina.
            var machineRows = await _context.MasterMaquinaDispensacions
                .AsNoTracking()
                .Where(m => m.IdCliente == clientId.Value
                            && m.IdTipoMaquina.HasValue
                            && (m.IdTipoMaquina.Value == 1 || m.IdTipoMaquina.Value == 4 || m.IdTipoMaquina.Value == 5))
                .Select(m => new
                {
                    m.Id,
                    Name = m.Descripcion ?? ("Machine " + m.Id),
                    m.IdTipoMaquina,
                    m.NumeroPrendasSucias,
                    TipoNombre = m.IdTipoMaquinaNavigation != null ? m.IdTipoMaquinaNavigation.Descripcion : null
                })
                .ToListAsync()
                .ConfigureAwait(false);
            if (machineRows.Count == 0)
                return Array.Empty<MachineStatusDto>();

            var machineIds = machineRows.Select(m => m.Id).ToArray();

            var (limitFew, limitLowPercent, generalCollectorLimitRaw) = await GetAvisoLimitsBundleAsync().ConfigureAwait(false);
            var generalCollectorLimit = generalCollectorLimitRaw is > 0 ? generalCollectorLimitRaw : null;

            // DbContext is not thread-safe — run queries sequentially (parallel caused HTTP 500).
            var stockRows = await _context.MovMaquinaDisAlmacenes
                .AsNoTracking()
                .Where(r => machineIds.Contains(r.IdMaquinaDispensacion))
                .Select(r => new DispenserStockRow
                {
                    MachineId = r.IdMaquinaDispensacion,
                    StockReal = r.StockReal,
                    MaxCapacity = r.CantMax
                })
                .ToListAsync()
                .ConfigureAwait(false);

            var countersByMachine = await LoadLatestCollectorCountersAsync(machineIds, clientId.Value, tenantScoped: true)
                .ConfigureAwait(false);

            // Safety fallback: if a machine has no same-tenant counter row (data inconsistency),
            // still show the latest known counter for that machine id.
            var missingCounterMachineIds = machineIds.Where(id => !countersByMachine.ContainsKey(id)).ToArray();
            if (missingCounterMachineIds.Length > 0)
            {
                var fallback = await LoadLatestCollectorCountersAsync(missingCounterMachineIds, clientId.Value, tenantScoped: false)
                    .ConfigureAwait(false);
                foreach (var (machineId, counter) in fallback)
                    countersByMachine[machineId] = counter;
            }

            var stockByMachine = stockRows.GroupBy(s => s.MachineId).ToDictionary(g => g.Key, g => g.ToList());

            var result = new List<MachineStatusDto>(machineRows.Count);
            foreach (var machine in machineRows)
            {
                var model = new MachineStatusDto
                {
                    MachineId = machine.Id,
                    Name = TranslateDisplayText(machine.Name, languageCode),
                    MachineTypeDescription = TranslateDisplayText(machine.TipoNombre, languageCode)
                };

                var isCollector = machine.IdTipoMaquina == 4;
                model.Type = isCollector ? "collector" : "dispenser";

                if (isCollector)
                {
                    // Current garments: latest MovMaquinaContadorPrendasSucias.Contador for this machine.
                    var current = countersByMachine.TryGetValue(machine.Id, out var c) ? c : 0;
                    model.CurrentCollectorCount = current;

                    // Max: machine-specific NumeroPrendasSucias first; else general Tipo=4 aviso limit.
                    var machineLimit = machine.NumeroPrendasSucias is > 0
                        ? machine.NumeroPrendasSucias
                        : null;
                    var lim = machineLimit ?? generalCollectorLimit;
                    model.CollectorLimit = lim;

                    if (lim is > 0)
                    {
                        var max = lim.Value;
                        var ratio = current / (double)max;
                        model.CollectorFillPercent = Math.Round(ratio * 100d, 1);
                        // FULL/red at or over 100%; orange when fill exceeds 70% but is still under the limit.
                        model.IsCollectorFull = current >= max;
                        model.IsCollectorNearLimit = !model.IsCollectorFull && ratio > 0.7d;
                    }
                }
                else
                {
                    if (!stockByMachine.TryGetValue(machine.Id, out var rows))
                        rows = new List<DispenserStockRow>();
                    var totalCompartments = rows.Count;
                    var emptyCompartments = rows.Count(r => r.StockReal <= 0);

                    model.TotalCompartments = totalCompartments;
                    model.EmptyCompartments = emptyCompartments;
                    model.HasEmptyCompartment = emptyCompartments > 0;
                    model.HasFewGarments = limitFew.HasValue && rows.Any(r => r.StockReal < limitFew.Value);
                    model.HasLowStockPercent = limitLowPercent.HasValue
                        && rows.Any(r => r.MaxCapacity > 0 && (r.StockReal * 100d / r.MaxCapacity) < limitLowPercent.Value);
                }

                result.Add(model);
            }

            _cache.Set(cacheKey, result, DashboardCacheTtl);
            return result;
        }

        public async Task<IReadOnlyList<MachineWarehouseStockDto>> GetMachineWarehouseStockAsync(string userId, int machineId, string? languageCode = null)
        {
            var clientId = await ResolveClientIdAsync(userId).ConfigureAwait(false);
            if (clientId == null)
                throw new UnauthorizedAccessException("User has no client access.");

            var machineInfo = await _context.MasterMaquinaDispensacions
                .AsNoTracking()
                .Where(m => m.Id == machineId && m.IdCliente == clientId.Value)
                .Select(m => new { m.Id, m.IdTipoMaquina })
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (machineInfo == null)
                throw new UnauthorizedAccessException("Machine is not available for this user.");

            // Collectors (type 4) do not use dispenser warehouse rows.
            if (machineInfo.IdTipoMaquina == 4)
                return Array.Empty<MachineWarehouseStockDto>();

            var rows = await (
                    from r in _context.MovMaquinaDisAlmacenes.AsNoTracking()
                    where r.IdMaquinaDispensacion == machineId
                    join mod in _context.MasterModeloPrenda.AsNoTracking() on r.IdModelo equals mod.Id into modG
                    from mod in modG.DefaultIfEmpty()
                    orderby r.IdAlmacen
                    select new MachineWarehouseStockDto
                    {
                        IdAlmacen = r.IdAlmacen,
                        IdModelo = r.IdModelo ?? 0,
                        ModelName = mod != null ? mod.Descripcion : null,
                        Talla = r.Talla ?? string.Empty,
                        StockReal = r.StockReal,
                        CantMax = r.CantMax
                    })
                .ToListAsync()
                .ConfigureAwait(false);

            foreach (var row in rows)
                row.ModelName = TranslateDisplayText(row.ModelName, languageCode);

            return rows;
        }

        public async Task<MachineConsumptionDto> GetMachineConsumptionAsync(string userId, string period, string? languageCode = null)
        {
            var normalizedPeriod = NormalizePeriod(period);
            var cacheKey = $"machines:consumption:{userId}:{normalizedPeriod}:{languageCode ?? "default"}";
            if (_cache.TryGetValue(cacheKey, out MachineConsumptionDto? cached) && cached != null)
                return cached;

            var (start, end) = GetPeriodBounds(normalizedPeriod, DateTime.Today);
            var clientId = await ResolveClientIdAsync(userId).ConfigureAwait(false);
            if (!clientId.HasValue)
            {
                return new MachineConsumptionDto
                {
                    Period = normalizedPeriod,
                    DispenserTotal = 0,
                    CollectorTotal = 0,
                    TopModels = new List<ConsumptionModelBarDto>()
                };
            }

            // One SQL round-trip: group by movement type + model (remote SQL Express is slow per query).
            var aggRows = await _context.Database
                .SqlQuery<ConsumptionAggRow>($@"
                    SELECT mp.id_TipoAntena AS IdTipoAntena, mp.idModeloPrenda AS IdModeloPrenda, COUNT(*) AS Qty
                    FROM MovPren mp WITH (NOLOCK)
                    WHERE mp.idCli = {clientId.Value}
                      AND mp.Fecha >= {start}
                      AND mp.Fecha < {end}
                      AND mp.id_TipoAntena IN (50, 51, 55)
                    GROUP BY mp.id_TipoAntena, mp.idModeloPrenda")
                .ToListAsync()
                .ConfigureAwait(false);

            var dispenserTotal = aggRows
                .Where(x => x.IdTipoAntena is 51 or 55)
                .Sum(x => x.Qty);
            var collectorTotal = aggRows
                .Where(x => x.IdTipoAntena == 50)
                .Sum(x => x.Qty);

            var topRaw = aggRows
                .Where(x => x.IdTipoAntena is 51 or 55 && x.IdModeloPrenda.HasValue)
                .GroupBy(x => x.IdModeloPrenda!.Value)
                .Select(g => new { ModelId = (int?)g.Key, Qty = g.Sum(x => x.Qty) })
                .OrderByDescending(x => x.Qty)
                .Take(5)
                .ToList();

            var modelIds = topRaw.Where(x => x.ModelId.HasValue).Select(x => x.ModelId!.Value).Distinct().ToList();
            var names = modelIds.Count == 0
                ? new Dictionary<int, string>()
                : await _context.MasterModeloPrenda
                    .AsNoTracking()
                    .Where(m => modelIds.Contains(m.Id))
                    .ToDictionaryAsync(m => m.Id, m => m.Descripcion ?? ("Model " + m.Id))
                    .ConfigureAwait(false);

            var maxQty = topRaw.Count == 0 ? 0 : topRaw.Max(x => x.Qty);
            var bars = topRaw.Select(x =>
            {
                var modelName = x.ModelId.HasValue && names.TryGetValue(x.ModelId.Value, out var n)
                    ? n
                    : (x.ModelId.HasValue ? ("Model " + x.ModelId.Value) : "Unknown");
                return new ConsumptionModelBarDto
                {
                    ModelId = x.ModelId,
                    ModelName = TranslateDisplayText(modelName, languageCode),
                    Qty = x.Qty,
                    Ratio = maxQty <= 0 ? 0 : Math.Round((double)x.Qty / maxQty, 3)
                };
            }).ToList();

            var dto = new MachineConsumptionDto
            {
                Period = normalizedPeriod,
                DispenserTotal = dispenserTotal,
                CollectorTotal = collectorTotal,
                TopModels = bars
            };
            _cache.Set(cacheKey, dto, DashboardCacheTtl);
            return dto;
        }

        private sealed class ConsumptionAggRow
        {
            public int? IdTipoAntena { get; set; }
            public int? IdModeloPrenda { get; set; }
            public int Qty { get; set; }
        }

        // ── Sizes ──────────────────────────────────────────────────────────────────

        public async Task<List<SizePreferenceDto>> GetSizesAsync(string userId)
        {
            if (TryParseSubClienteId(userId, out _) || TryParseApplicationUserId(userId, out _))
                return new List<SizePreferenceDto>();

            var user = await _context.Users.FindAsync(userId);
            if (user?.CustomerId == null)
                return new List<SizePreferenceDto>();

            return await _context.CustomerItemModels
                .Where(c => c.CustomerId == user.CustomerId)
                .Select(c => new SizePreferenceDto
                {
                    ModelId = c.FamilyItemModelId.ToString(),
                    Size = ""
                })
                .ToListAsync();
        }

        /// <summary>
        /// MovModeloTallaSub + MasterSubClientes + MasterModeloPrenda (SubCliente APK Size tab).
        /// </summary>
        public async Task<List<SubClienteGarmentSizeDto>> GetSubClienteGarmentSizesAsync(string userId, string? languageCode = null)
        {
            if (!TryParseSubClienteId(userId, out var subClienteId))
                return new List<SubClienteGarmentSizeDto>();

            var list = await _context.MovModeloTallaSubs
                .AsNoTracking()
                .Where(m => m.IdSubCli == subClienteId)
                .Select(m => new SubClienteGarmentSizeDto
                {
                    Id = m.Id,
                    FirstName = m.IdSubCliNavigation != null ? m.IdSubCliNavigation.Nombre ?? "" : "",
                    LastName1 = m.IdSubCliNavigation != null ? m.IdSubCliNavigation.Apellido1 ?? "" : "",
                    LastName2 = m.IdSubCliNavigation != null ? m.IdSubCliNavigation.Apellido2 ?? "" : "",
                    ModelId = m.Idmod ?? 0,
                    ModelDescription = m.IdmodNavigation != null ? m.IdmodNavigation.Descripcion ?? "" : "",
                    Size = m.Talla ?? ""
                })
                .OrderBy(d => d.ModelDescription)
                .ThenBy(d => d.Id)
                .ToListAsync()
                .ConfigureAwait(false);

            foreach (var item in list)
                item.ModelDescription = TranslateDisplayText(item.ModelDescription, languageCode);

            return list;
        }

        public async Task UpdateSubClienteGarmentSizesAsync(string userId, IReadOnlyList<SubClienteGarmentSizeUpdateDto> updates)
        {
            if (!TryParseSubClienteId(userId, out var subClienteId))
                throw new InvalidOperationException("SubCliente token required");

            if (updates == null || updates.Count == 0)
                return;

            var ids = updates.Where(u => u.Id > 0).Select(u => u.Id).Distinct().ToList();
            var rowsById = new Dictionary<int, MovModeloTallaSub>();
            if (ids.Count > 0)
            {
                var byId = await _context.MovModeloTallaSubs
                    .Where(r => ids.Contains(r.Id) && r.IdSubCli == subClienteId)
                    .ToListAsync();
                foreach (var r in byId)
                    rowsById[r.Id] = r;
            }

            var modelIds = updates
                .Where(u => u.Id <= 0 && u.ModelId.HasValue && u.ModelId.Value > 0)
                .Select(u => u.ModelId!.Value)
                .Distinct()
                .ToList();

            List<MovModeloTallaSub>? rowsByModel = null;
            if (modelIds.Count > 0)
            {
                rowsByModel = await _context.MovModeloTallaSubs
                    .Where(r => r.IdSubCli == subClienteId && r.Idmod.HasValue && modelIds.Contains(r.Idmod.Value))
                    .ToListAsync();
            }

            foreach (var u in updates)
            {
                var talla = NormalizeTalla(u.Size);

                if (u.Id > 0)
                {
                    if (rowsById.TryGetValue(u.Id, out var row))
                        row.Talla = talla;
                    continue;
                }

                if (rowsByModel != null && u.ModelId.HasValue && u.ModelId.Value > 0)
                {
                    var mid = u.ModelId.Value;
                    foreach (var row in rowsByModel.Where(r => r.Idmod == mid))
                        row.Talla = talla;
                }
            }

            await _context.SaveChangesAsync();
        }

        private static string NormalizeTalla(string? size)
        {
            var talla = (size ?? "").Trim();
            if (talla.Length > 50)
                talla = talla[..50];
            return talla;
        }

        public async Task UpdateSizesAsync(string userId, List<SizePreferenceDto> sizes)
        {
            await Task.CompletedTask;
        }

        // ── Movements ──────────────────────────────────────────────────────────────

        public async Task<MovementsResponse> GetMovementsAsync(
            string userId, DateTime? from, DateTime? to, string? type, int? modelId, int page, int size, string? languageCode = null)
        {
            if (TryParseSubClienteId(userId, out var subClienteId))
                return await GetSubClienteMovementsFromHistoricoAsync(subClienteId, from, to, type, modelId, page, size, languageCode)
                    .ConfigureAwait(false);

            IQueryable<LFS.Shared.Entities.Movement> query;
            var effectiveUserId = TryParseQrAdminUserId(userId, out var qaUserId) ? qaUserId : userId;

            query = _context.Movements
                .Where(m => !m.Deleted && m.Description.Contains("userId=" + effectiveUserId));

            if (from.HasValue)
                query = query.Where(m => m.CreationDate >= from.Value);
            if (to.HasValue)
                query = query.Where(m => m.CreationDate < to.Value);
            if (!string.IsNullOrEmpty(type))
                query = query.Where(m => m.Description.Contains(type));

            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(m => m.CreationDate)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(m => new MovementItemDto
                {
                    Id = m.Id,
                    ModelId = null,
                    MachineId = null,
                    Type = "",
                    Description = m.Description,
                    CreatedAt = m.CreationDate,
                    LastMovementCode = null,
                    GarmentName = null,
                    MachineName = null,
                    Talla = null,
                    Quantity = 0
                })
                .ToListAsync();

            return new MovementsResponse
            {
                Items = items,
                Total = total,
                Page = page,
                Size = size
            };
        }

        /// <summary>
        /// Garment movement history from <c>MovStockPrendasUsu_Historico</c> (idLastMov: 50 / 51 / 55 / 1).
        /// </summary>
        private async Task<MovementsResponse> GetSubClienteMovementsFromHistoricoAsync(
            int subClienteId,
            DateTime? from,
            DateTime? to,
            string? type,
            int? modelId,
            int page,
            int size,
            string? languageCode = null)
        {
            var query = _context.MovStockPrendasUsuHistoricos.AsNoTracking()
                .Where(h => h.IdSubCliente == subClienteId);

            // from = start of first selected calendar day; to = exclusive end (midnight after last selected day), same as Fecha >= @from AND Fecha < @toExclusive
            if (from.HasValue)
                query = query.Where(h => h.Fecha >= from.Value);
            if (to.HasValue)
                query = query.Where(h => h.Fecha < to.Value);
            if (!string.IsNullOrWhiteSpace(type) && int.TryParse(type.Trim(), out var movCode))
                query = query.Where(h => h.IdLastMov == movCode);
            if (modelId.HasValue && modelId.Value > 0)
                query = query.Where(h => h.IdModelo == modelId.Value);

            var total = await query.CountAsync().ConfigureAwait(false);

            var hist = _context.MovStockPrendasUsuHistoricos.AsNoTracking();
            var items = await (
                    from h in query
                    join mod in _context.MasterModeloPrenda.AsNoTracking() on h.IdModelo equals mod.Id into modG
                    from mod in modG.DefaultIfEmpty()
                    join maq in _context.MasterMaquinaDispensacions.AsNoTracking() on h.IdMaquinaMov equals maq.Id into maqG
                    from maq in maqG.DefaultIfEmpty()
                    let prevStock = hist
                        .Where(p => p.IdSubCliente == h.IdSubCliente && p.IdModelo == h.IdModelo
                            && (p.Fecha < h.Fecha || (p.Fecha == h.Fecha && p.Id < h.Id)))
                        .OrderByDescending(p => p.Fecha)
                        .ThenByDescending(p => p.Id)
                        .Select(p => (int?)p.StockEnUsoActual)
                        .FirstOrDefault()
                    let resolvedMov = h.IdLastMov == 50 || h.IdLastMov == 51 || h.IdLastMov == 55 || h.IdLastMov == 1
                        ? h.IdLastMov
                        : h.IdLastMov != null && h.IdLastMov != 0
                            ? h.IdLastMov
                        : prevStock == null
                            ? (h.StockEnUsoActual > 0 ? (int?)50 : (int?)51)
                        : h.StockEnUsoActual > prevStock
                            ? (int?)50
                        : h.StockEnUsoActual < prevStock
                            ? (int?)51
                        : prevStock == 0 && h.StockEnUsoActual == 0
                            ? (int?)51
                            : (int?)1
                    orderby h.Fecha descending
                    select new MovementItemDto
                    {
                        Id = h.Id,
                        ModelId = h.IdModelo,
                        MachineId = h.IdMaquinaMov,
                        LastMovementCode = resolvedMov,
                        Type = MapLastMovToTypeKey(resolvedMov),
                        GarmentName = mod != null ? mod.Descripcion : null,
                        MachineName = maq != null ? maq.Descripcion : null,
                        Talla = null,
                        Quantity = h.StockEnUsoActual,
                        Description = "",
                        CreatedAt = h.Fecha
                    })
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync()
                .ConfigureAwait(false);

            foreach (var item in items)
            {
                item.GarmentName = TranslateDisplayText(item.GarmentName, languageCode);
                item.MachineName = TranslateDisplayText(item.MachineName, languageCode);
            }

            return new MovementsResponse
            {
                Items = items,
                Total = total,
                Page = page,
                Size = size
            };
        }

        public async Task<MovementsResponse> GetAdminMovementsAsync(
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
            string? languageCode = null)
        {
            await RequireAdminAsync(adminUserId).ConfigureAwait(false);
            var clientId = await ResolveClientIdAsync(adminUserId).ConfigureAwait(false);
            if (!clientId.HasValue)
                return new MovementsResponse { Items = new(), Total = 0, Page = page, Size = size };

            page = Math.Max(1, page);
            size = Math.Clamp(size, 1, 100);
            var tallaNorm = string.IsNullOrWhiteSpace(talla) ? null : talla.Trim();
            var typeCodes = ResolveMovementTypeCodes(type);

            if (!from.HasValue && !to.HasValue)
            {
                var today = DateTime.Today;
                from = today.AddDays(1 - today.Day);
                to = today;
            }

            var cacheKey =
                $"admin:movements:{adminUserId}:{from:O}:{to:O}:{machineId}:{modelId}:{tallaNorm}:{userId}:{type ?? "all"}:{page}:{size}:{languageCode ?? "default"}";
            if (_cache.TryGetValue(cacheKey, out MovementsResponse? cachedMovements) && cachedMovements != null)
                return cachedMovements;

            // Admin Consumption reads garment events from MovPren (not stock historico).
            // Filter/sort/page on MovPren alone: joining the name tables before the sort makes SQL Server
            // sort the whole tenant history (200k+ rows) and the request times out. Names are resolved
            // afterwards, for the page rows only.
            var query = _context.MovPrens
                .AsNoTracking()
                .Where(mp => mp.IdCli == clientId.Value);

            var fromDate = NormalizeFilterFromDate(from);
            var toDate = NormalizeFilterToDateInclusive(to);
            if (fromDate.HasValue)
                query = query.Where(mp => mp.Fecha.HasValue && mp.Fecha.Value.Date >= fromDate.Value);
            if (toDate.HasValue)
                query = query.Where(mp => mp.Fecha.HasValue && mp.Fecha.Value.Date <= toDate.Value);
            if (modelId.HasValue && modelId.Value > 0)
                query = query.Where(mp => mp.IdModeloPrenda == modelId.Value);
            if (!string.IsNullOrWhiteSpace(tallaNorm))
                query = query.Where(mp => mp.Talla == tallaNorm);
            if (userId.HasValue && userId.Value > 0)
                query = query.Where(mp => mp.IdSubCli == userId.Value);
            if (typeCodes.Count == 1)
                query = query.Where(mp => mp.IdTipoAntena == typeCodes[0]);
            else if (typeCodes.Count > 1)
                query = query.Where(mp => mp.IdTipoAntena.HasValue && typeCodes.Contains(mp.IdTipoAntena.Value));

            // Machine filter via dispenser warehouse slot, else puesto on the machine master.
            if (machineId.HasValue && machineId.Value > 0)
            {
                var mid = machineId.Value;
                var warehouseAlmacenes = _context.MovMaquinaDisAlmacenes.AsNoTracking()
                    .Where(w => w.IdMaquinaDispensacion == mid)
                    .Select(w => w.IdAlmacen);
                var machinePuestos = _context.MasterMaquinaDispensacions.AsNoTracking()
                    .Where(m => m.Id == mid)
                    .Select(m => m.IdPuesto);

                query = query.Where(mp =>
                    (mp.AlmacenDispensadora.HasValue && warehouseAlmacenes.Contains(mp.AlmacenDispensadora.Value))
                    || (mp.IdPuesto.HasValue && machinePuestos.Contains(mp.IdPuesto)));
            }

            var pageRows = await query
                .OrderByDescending(mp => mp.Fecha)
                .ThenByDescending(mp => mp.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(mp => new
                {
                    mp.Id,
                    mp.IdModeloPrenda,
                    mp.IdTipoAntena,
                    mp.IdSubCli,
                    mp.Talla,
                    mp.Fecha,
                    mp.AlmacenDispensadora,
                    mp.IdPuesto
                })
                .ToListAsync()
                .ConfigureAwait(false);

            var total = await query.CountAsync().ConfigureAwait(false);

            var pageModelIds = pageRows
                .Where(r => r.IdModeloPrenda.HasValue)
                .Select(r => r.IdModeloPrenda!.Value)
                .Distinct()
                .ToArray();
            var garmentNames = pageModelIds.Length == 0
                ? new Dictionary<int, string?>()
                : (await _context.MasterModeloPrenda
                        .AsNoTracking()
                        .Where(m => pageModelIds.Contains(m.Id))
                        .Select(m => new { m.Id, m.Descripcion })
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .ToDictionary(m => m.Id, m => (string?)m.Descripcion);

            var pageSubCliIds = pageRows
                .Where(r => r.IdSubCli.HasValue)
                .Select(r => r.IdSubCli!.Value)
                .Distinct()
                .ToArray();
            var userNames = pageSubCliIds.Length == 0
                ? new Dictionary<int, string?>()
                : (await _context.MasterSubClientes
                        .AsNoTracking()
                        .Where(s => pageSubCliIds.Contains(s.Id))
                        .Select(s => new { s.Id, s.Nombre, s.Apellido1, s.Apellido2 })
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .ToDictionary(s => s.Id, s => JoinNameParts(s.Nombre, s.Apellido1, s.Apellido2));

            var almacenes = pageRows
                .Where(r => r.AlmacenDispensadora.HasValue)
                .Select(r => r.AlmacenDispensadora!.Value)
                .Distinct()
                .ToArray();
            var puestos = pageRows
                .Where(r => r.IdPuesto.HasValue)
                .Select(r => r.IdPuesto!.Value)
                .Distinct()
                .ToArray();

            var machineByAlmacen = new Dictionary<int, (int Id, string? Name)>();
            if (almacenes.Length > 0)
            {
                var almacenRows = await (
                        from wh in _context.MovMaquinaDisAlmacenes.AsNoTracking()
                        where almacenes.Contains(wh.IdAlmacen)
                        join maq in _context.MasterMaquinaDispensacions.AsNoTracking()
                            on wh.IdMaquinaDispensacion equals maq.Id
                        select new { wh.IdAlmacen, maq.Id, maq.Descripcion })
                    .ToListAsync()
                    .ConfigureAwait(false);
                machineByAlmacen = almacenRows
                    .GroupBy(x => x.IdAlmacen)
                    .ToDictionary(g => g.Key, g => (g.First().Id, (string?)g.First().Descripcion));
            }

            var machineByPuesto = new Dictionary<int, (int Id, string? Name)>();
            if (puestos.Length > 0)
            {
                var puestoRows = await _context.MasterMaquinaDispensacions.AsNoTracking()
                    .Where(m => m.IdCliente == clientId.Value && m.IdPuesto.HasValue && puestos.Contains(m.IdPuesto.Value))
                    .Select(m => new { Puesto = m.IdPuesto!.Value, m.Id, m.Descripcion })
                    .ToListAsync()
                    .ConfigureAwait(false);
                machineByPuesto = puestoRows
                    .GroupBy(x => x.Puesto)
                    .ToDictionary(g => g.Key, g => (g.First().Id, (string?)g.First().Descripcion));
            }

            var tipoAntenaIds = pageRows
                .Where(r => r.IdTipoAntena.HasValue)
                .Select(r => r.IdTipoAntena!.Value)
                .Distinct()
                .ToArray();
            var tipoAntenaNames = tipoAntenaIds.Length == 0
                ? new Dictionary<int, string?>()
                : (await _context.MasterTipoAntenas
                        .AsNoTracking()
                        .Where(t => tipoAntenaIds.Contains(t.Id))
                        .Select(t => new { t.Id, t.Descripcion })
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .ToDictionary(t => t.Id, t => (string?)t.Descripcion);

            var items = new List<MovementItemDto>(pageRows.Count);
            foreach (var row in pageRows)
            {
                int? resolvedMachineId = null;
                string? machineName = null;
                if (row.AlmacenDispensadora.HasValue
                    && machineByAlmacen.TryGetValue(row.AlmacenDispensadora.Value, out var byAlmacen))
                {
                    resolvedMachineId = byAlmacen.Id;
                    machineName = byAlmacen.Name;
                }
                else if (row.IdPuesto.HasValue
                         && machineByPuesto.TryGetValue(row.IdPuesto.Value, out var byPuesto))
                {
                    resolvedMachineId = byPuesto.Id;
                    machineName = byPuesto.Name;
                }

                string? tipoAntenaName = null;
                if (row.IdTipoAntena.HasValue
                    && tipoAntenaNames.TryGetValue(row.IdTipoAntena.Value, out var taName))
                {
                    tipoAntenaName = taName;
                }

                items.Add(new MovementItemDto
                {
                    Id = row.Id,
                    ModelId = row.IdModeloPrenda,
                    MachineId = resolvedMachineId,
                    LastMovementCode = row.IdTipoAntena,
                    Type = MapLastMovToTypeKey(row.IdTipoAntena),
                    GarmentName = TranslateDisplayText(
                        row.IdModeloPrenda.HasValue && garmentNames.TryGetValue(row.IdModeloPrenda.Value, out var garment)
                            ? garment
                            : null,
                        languageCode),
                    MachineName = TranslateDisplayText(machineName, languageCode),
                    Talla = row.Talla,
                    UserId = row.IdSubCli,
                    UserName = row.IdSubCli.HasValue && userNames.TryGetValue(row.IdSubCli.Value, out var userName)
                        ? userName
                        : null,
                    Quantity = 1,
                    // MasterTipoAntena.descripcion — used by mobile for codes outside 1/50/51/55.
                    Description = TranslateDisplayText(tipoAntenaName, languageCode) ?? "",
                    CreatedAt = row.Fecha ?? DateTime.MinValue
                });
            }

            var response = new MovementsResponse
            {
                Items = items,
                Total = total,
                Page = page,
                Size = size
            };
            _cache.Set(cacheKey, response, DashboardCacheTtl);
            return response;
        }

        public async Task<IReadOnlyList<AdminMovementTypeDto>> GetAdminMovementTypesAsync(
            string adminUserId,
            string? languageCode = null)
        {
            await RequireAdminAsync(adminUserId).ConfigureAwait(false);

            var rows = await _context.MasterTipoAntenas
                .AsNoTracking()
                .Where(a => a.Mostrar)
                .OrderBy(a => a.Descripcion)
                .Select(a => new { a.Id, a.Descripcion })
                .ToListAsync()
                .ConfigureAwait(false);

            return rows.Select(row => new AdminMovementTypeDto
            {
                Id = row.Id,
                Label = TranslateDisplayText(row.Descripcion, languageCode)
                        ?? row.Descripcion?.Trim()
                        ?? $"Type {row.Id}"
            }).ToList();
        }

        private static DateTime? NormalizeFilterFromDate(DateTime? value) =>
            value.HasValue ? value.Value.Date : null;

        /// <summary>Accepts inclusive end dates from clients (date-only or offset timestamps).</summary>
        private static DateTime? NormalizeFilterToDateInclusive(DateTime? value) =>
            value.HasValue ? value.Value.Date : null;

        /// <summary>
        /// <c>MasterModeloPrenda.descripcion</c> when present; "Model {id}" only for orphan ids &gt; 0.
        /// Returns null when <paramref name="idModelo"/> is 0 (no valid model — avoid "Model 0" in UI).
        /// </summary>
        private static string? ResolveModelDisplayName(int idModelo, string? descripcion, string? languageCode)
        {
            if (!string.IsNullOrWhiteSpace(descripcion))
            {
                return TranslateDisplayText(descripcion.Trim(), languageCode)
                       ?? descripcion.Trim();
            }

            if (idModelo > 0)
                return "Model " + idModelo;

            return null;
        }

        private static IReadOnlyList<int> ResolveMovementTypeCodes(string? type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return Array.Empty<int>();
            var t = type.Trim().ToLowerInvariant();
            return t switch
            {
                "received_garment" => new[] { 50 },
                "dispensed_garment" => new[] { 51, 55 },
                "dispensed_lingerie" => new[] { 55 },
                "web_setting" => new[] { 1 },
                _ => int.TryParse(t, out var code) ? new[] { code } : Array.Empty<int>()
            };
        }

        /// <summary>Stable keys for clients; map <see cref="LastMovementCode"/> for localized UI.</summary>
        private static string MapLastMovToTypeKey(int? idLastMov) => idLastMov switch
        {
            50 => "received_garment",
            51 => "dispensed_garment",
            55 => "dispensed_lingerie",
            1 => "web_setting",
            _ => "other"
        };

        // ── Admin: user credits (MasterSubClientes + MovStockPrendasUsu) ───────────

        public async Task<IReadOnlyList<AdminSubClienteSummaryDto>> SearchSubClientesForAdminAsync(
            string adminUserId,
            string? search,
            int take)
        {
            await RequireAdminAsync(adminUserId).ConfigureAwait(false);
            var clientId = await ResolveClientIdAsync(adminUserId).ConfigureAwait(false);
            if (clientId == null)
                return Array.Empty<AdminSubClienteSummaryDto>();

            take = Math.Clamp(take, 1, 80);
            var q = _context.MasterSubClientes.AsNoTracking()
                .Where(s => s.IdCliente == clientId && (s.Borrado == null || s.Borrado == false));

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                q = q.Where(s =>
                    (s.Nombre != null && s.Nombre.Contains(term))
                    || (s.Apellido1 != null && s.Apellido1.Contains(term))
                    || (s.Apellido2 != null && s.Apellido2.Contains(term))
                    || (s.Dni != null && s.Dni.Contains(term))
                    || (s.IdEmpleado != null && s.IdEmpleado.Contains(term)));
            }

            // Project only DB-translatable columns; string.Join / IsNullOrWhiteSpace in Select are not translated to SQL.
            var rows = await q
                .OrderBy(s => s.Apellido1)
                .ThenBy(s => s.Nombre)
                .Take(take)
                .Select(s => new { s.Id, s.Nombre, s.Apellido1, s.Apellido2, s.Puesto, s.Categoria })
                .ToListAsync()
                .ConfigureAwait(false);

            return rows
                .Select(s => new AdminSubClienteSummaryDto
                {
                    Id = s.Id,
                    DisplayName = JoinNameParts(s.Nombre, s.Apellido1, s.Apellido2),
                    RoleLabel = string.IsNullOrWhiteSpace(s.Puesto) ? s.Categoria : s.Puesto
                })
                .ToList();
        }

        private static string JoinNameParts(string? nombre, string? apellido1, string? apellido2)
        {
            return string.Join(" ",
                new[] { nombre, apellido1, apellido2 }.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        public async Task<AdminUserCreditsDetailDto?> GetSubClienteCreditsDetailForAdminAsync(
            string adminUserId,
            int subClienteId,
            string? languageCode = null)
        {
            await RequireAdminAsync(adminUserId).ConfigureAwait(false);
            var clientId = await ResolveClientIdAsync(adminUserId).ConfigureAwait(false);
            if (clientId == null)
                return null;

            var sc = await _context.MasterSubClientes.AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == subClienteId)
                .ConfigureAwait(false);
            if (sc == null || sc.IdCliente != clientId)
                return null;

            DepartmentCreditLimits? deptLimits = null;
            if (sc.IdDepartamentoCliente.HasValue)
            {
                deptLimits = await LoadDepartmentCreditLimitsAsync(sc.IdDepartamentoCliente.Value)
                    .ConfigureAwait(false);
            }

            var stock = await _context.MovStockPrendasUsus
                .AsNoTracking()
                .Include(s => s.IdModeloNavigation)
                .Where(s => s.IdSubCliente == subClienteId)
                .OrderBy(s => s.IdModelo)
                .ThenBy(s => s.Talla)
                .ToListAsync()
                .ConfigureAwait(false);

            var lines = stock.Select(s => new AdminUserCreditLineDto
            {
                StockRowId = s.Id,
                ModelId = s.IdModelo,
                ModelName = TranslateDisplayText(s.IdModeloNavigation?.Descripcion, languageCode),
                Talla = s.Talla ?? "",
                Cantidad = s.Cantidad,
                LimiteCantidadPrendasEnUso = deptLimits?.ResolveForModel(s.IdModelo) ?? 0
            }).ToList();

            int? totalConfigured = deptLimits != null
                ? stock.Select(s => s.IdModelo).Distinct().Sum(m => deptLimits.ResolveForModel(m) ?? 0)
                : null;
            if (totalConfigured is 0)
                totalConfigured = null;

            return new AdminUserCreditsDetailDto
            {
                SubClienteId = sc.Id,
                DisplayName = string.Join(" ",
                    new[] { sc.Nombre, sc.Apellido1, sc.Apellido2 }.Where(x => !string.IsNullOrWhiteSpace(x))),
                RoleLabel = string.IsNullOrWhiteSpace(sc.Puesto) ? sc.Categoria : sc.Puesto,
                TotalConfiguredLimit = totalConfigured,
                Lines = lines
            };
        }

        public async Task<AdminCreditHistoryResponse> GetSubClienteCreditHistoryForAdminAsync(
            string adminUserId,
            int subClienteId,
            DateTime? from,
            DateTime? to,
            int? modelId,
            int page,
            int size,
            string? languageCode = null)
        {
            await RequireAdminAsync(adminUserId).ConfigureAwait(false);
            var clientId = await ResolveClientIdAsync(adminUserId).ConfigureAwait(false);
            if (clientId == null)
                throw new UnauthorizedAccessException("User has no client access.");

            var sc = await _context.MasterSubClientes.AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == subClienteId)
                .ConfigureAwait(false);
            if (sc == null || sc.IdCliente != clientId)
                throw new InvalidOperationException("User not found or not in your organization.");

            page = Math.Max(1, page);
            size = Math.Clamp(size, 1, 100);

            // History companion of MovStockPrendasUsu — has Fecha for date-range filters.
            var query = _context.MovStockPrendasUsuHistoricos.AsNoTracking()
                .Where(h => h.IdSubCliente == subClienteId);

            var fromDate = NormalizeFilterFromDate(from);
            var toDate = NormalizeFilterToDateInclusive(to);
            if (fromDate.HasValue)
                query = query.Where(h => h.Fecha.Date >= fromDate.Value);
            if (toDate.HasValue)
                query = query.Where(h => h.Fecha.Date <= toDate.Value);
            if (modelId.HasValue && modelId.Value > 0)
                query = query.Where(h => h.IdModelo == modelId.Value);

            var total = await query.CountAsync().ConfigureAwait(false);
            var rows = await (
                    from h in query
                    join mod in _context.MasterModeloPrenda.AsNoTracking() on h.IdModelo equals mod.Id into modG
                    from mod in modG.DefaultIfEmpty()
                    join maq in _context.MasterMaquinaDispensacions.AsNoTracking()
                        on h.IdMaquinaMov equals maq.Id into maqG
                    from maq in maqG.DefaultIfEmpty()
                    orderby h.Fecha descending, h.Id descending
                    select new
                    {
                        h.Id,
                        h.Fecha,
                        h.IdModelo,
                        ModelName = mod != null ? mod.Descripcion : null,
                        h.IdMaquinaMov,
                        MachineName = maq != null ? maq.Descripcion : null,
                        h.StockEnUsoActual
                    })
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync()
                .ConfigureAwait(false);

            var items = rows.Select(r => new AdminCreditHistoryItemDto
            {
                Id = r.Id,
                Fecha = r.Fecha,
                ModelId = r.IdModelo,
                ModelName = ResolveModelDisplayName(r.IdModelo, r.ModelName, languageCode) ?? string.Empty,
                MachineId = r.IdMaquinaMov,
                MachineName = TranslateDisplayText(
                    string.IsNullOrWhiteSpace(r.MachineName)
                        ? (r.IdMaquinaMov.HasValue ? ("Machine " + r.IdMaquinaMov.Value) : null)
                        : r.MachineName,
                    languageCode),
                Quantity = r.StockEnUsoActual
            }).ToList();

            return new AdminCreditHistoryResponse
            {
                SubClienteId = sc.Id,
                DisplayName = JoinNameParts(sc.Nombre, sc.Apellido1, sc.Apellido2),
                Items = items,
                Total = total,
                Page = page,
                Size = size
            };
        }

        public async Task UpdateSubClienteCreditForAdminAsync(
            string adminUserId,
            int subClienteId,
            AdminCreditUpdateRequestDto body)
        {
            await RequireAdminAsync(adminUserId).ConfigureAwait(false);
            if (body is null)
                throw new ArgumentNullException(nameof(body));

            var clientId = await ResolveClientIdAsync(adminUserId).ConfigureAwait(false);
            if (clientId == null)
                throw new InvalidOperationException("Cannot resolve client for admin.");

            var sc = await _context.MasterSubClientes
                .FirstOrDefaultAsync(s => s.Id == subClienteId)
                .ConfigureAwait(false);
            if (sc == null || sc.IdCliente != clientId)
                throw new InvalidOperationException("User not found or not in your organization.");

            var row = await _context.MovStockPrendasUsus
                .FirstOrDefaultAsync(r => r.Id == body.StockRowId && r.IdSubCliente == subClienteId)
                .ConfigureAwait(false);
            if (row == null)
                throw new InvalidOperationException("Credit row not found.");

            if (body.Cantidad < 0)
                throw new InvalidOperationException("Quantity cannot be negative.");

            int? limite = null;
            if (sc.IdDepartamentoCliente.HasValue)
            {
                var deptLimits = await LoadDepartmentCreditLimitsAsync(sc.IdDepartamentoCliente.Value)
                    .ConfigureAwait(false);
                limite = deptLimits?.ResolveForModel(row.IdModelo);
            }

            if (limite is > 0 && body.Cantidad > limite.Value)
                throw new InvalidOperationException($"Amount exceeds the configured limit ({limite.Value}) for this garment model.");

            var adminLabel = (await GetProfileAsync(adminUserId).ConfigureAwait(false)).Name;
            if (string.IsNullOrWhiteSpace(adminLabel))
                adminLabel = "admin";

            var previous = row.Cantidad;
            row.Cantidad = body.Cantidad;
            _context.MovStockPrendasUsus.Update(row);

            _context.MovStockPrendasUsuHistoricos.Add(new MovStockPrendasUsuHistorico
            {
                Fecha = DateTime.UtcNow,
                IdSubCliente = subClienteId,
                IdModelo = row.IdModelo,
                StockEnUsoActual = body.Cantidad,
                IdLastMov = 1,
                IdUsuActualizaMov = null,
                IdMaquinaMov = null,
                UsuarioOperacion = $"{adminLabel} | mobile admin | was {previous}"
            });

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task RequireAdminAsync(string userId)
        {
            var me = await GetProfileAsync(userId).ConfigureAwait(false);
            if (!string.Equals(me.Role, "admin", StringComparison.OrdinalIgnoreCase))
                throw new UnauthorizedAccessException("Administrator role required.");
        }

        // ── Helpers ────────────────────────────────────────────────────────────────

        private static string TranslateDisplayText(string? value, string? languageCode)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value ?? string.Empty;

            var lang = NormalizeLanguageCode(languageCode);
            if (!string.Equals(lang, "en", StringComparison.OrdinalIgnoreCase))
                return value;

            var source = NormalizeSourceForTranslation(value);
            if (ExactTranslations.TryGetValue(source, out var exact))
                return exact;

            var translated = source;
            foreach (var pair in TokenTranslations)
                translated = translated.Replace(pair.Key, pair.Value, StringComparison.Ordinal);

            translated = Regex.Replace(translated, @"\s{2,}", " ").Trim();
            translated = Regex.Replace(translated, @"\b(blue|green|light blue|navy|dark)\s+(shirt|pants|dress|coat|gown|apron)\b",
                m =>
                {
                    var color = m.Groups[1].Value;
                    var noun = m.Groups[2].Value;
                    return $"{CapitalizeWord(color)} {noun}";
                },
                RegexOptions.IgnoreCase);
            translated = Regex.Replace(translated, @"\b(pants|shirt|dress|coat|gown|apron)\s+(blue|green|light blue|navy|dark)\b",
                m =>
                {
                    var noun = m.Groups[1].Value.ToLowerInvariant();
                    var color = m.Groups[2].Value.ToLowerInvariant();
                    return $"{CapitalizeWord(color)} {noun}";
                },
                RegexOptions.IgnoreCase);

            // If Hebrew still remains after token replacement, strip leftover Hebrew words
            // so English UI never shows mixed Hebrew fragments.
            translated = Regex.Replace(translated, @"[\u0590-\u05FF]+", string.Empty).Trim();
            translated = Regex.Replace(translated, @"\s{2,}", " ").Trim();
            if (string.IsNullOrWhiteSpace(translated))
                translated = source;
            return translated;
        }

        private static string NormalizeSourceForTranslation(string value)
        {
            var normalized = value.Trim();
            normalized = normalized.Replace("\r", " ").Replace("\n", " ");
            normalized = normalized.Replace("־", " ");
            normalized = normalized.Replace("״", "\"").Replace("׳", "'");
            normalized = Regex.Replace(normalized, @"\s{2,}", " ").Trim();
            return normalized;
        }

        private static string CapitalizeWord(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;
            var lower = value.Trim().ToLowerInvariant();
            return char.ToUpperInvariant(lower[0]) + lower[1..];
        }

        private static string NormalizeLanguageCode(string? languageCode)
        {
            if (string.IsNullOrWhiteSpace(languageCode))
                return "en";

            var first = languageCode.Split(',')[0].Trim();
            var semi = first.IndexOf(';');
            if (semi >= 0)
                first = first[..semi].Trim();
            return first;
        }

        /// <summary>
        /// Returns true when the userId was issued for a SubCliente (prefix "sc:").
        /// </summary>
        private static bool TryParseSubClienteId(string userId, out int subClienteId)
        {
            subClienteId = 0;
            if (userId.StartsWith("sc:", StringComparison.Ordinal))
                return int.TryParse(userId[3..], out subClienteId);
            return false;
        }

        private static bool TryParseApplicationUserId(string userId, out int applicationUserId)
        {
            applicationUserId = 0;
            if (string.IsNullOrEmpty(userId) || !userId.StartsWith("au:", StringComparison.Ordinal))
                return false;
            return int.TryParse(userId[3..], out applicationUserId);
        }

        private static bool TryParseQrAdminUserId(string userId, out string appUserId)
        {
            appUserId = "";
            if (string.IsNullOrEmpty(userId) || !userId.StartsWith("qa:", StringComparison.Ordinal))
                return false;
            appUserId = userId[3..];
            return !string.IsNullOrWhiteSpace(appUserId);
        }

        private static string NormalizePeriod(string? period)
        {
            if (string.Equals(period, "month", StringComparison.OrdinalIgnoreCase))
                return "month";
            if (string.Equals(period, "year", StringComparison.OrdinalIgnoreCase))
                return "year";
            return "day";
        }

        private static (DateTime Start, DateTime End) GetPeriodBounds(string period, DateTime today)
        {
            return period switch
            {
                "month" => (
                    new DateTime(today.Year, today.Month, 1),
                    new DateTime(today.Year, today.Month, 1).AddMonths(1)
                ),
                "year" => (
                    new DateTime(today.Year, 1, 1),
                    new DateTime(today.Year + 1, 1, 1)
                ),
                _ => (today.Date, today.Date.AddDays(1))
            };
        }

        private async Task<int?> ResolveClientIdAsync(string userId)
        {
            var cacheKey = $"client:id:{userId}";
            if (_cache.TryGetValue(cacheKey, out int? cachedClientId))
                return cachedClientId;

            int? clientId = await ResolveClientIdCoreAsync(userId).ConfigureAwait(false);
            _cache.Set(cacheKey, clientId, ClientIdCacheTtl);
            return clientId;
        }

        private async Task<int?> ResolveClientIdCoreAsync(string userId)
        {
            if (TryParseSubClienteId(userId, out var subClienteId))
            {
                return await _context.MasterSubClientes
                    .Where(s => s.Id == subClienteId)
                    .Select(s => s.IdCliente)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
            }

            if (TryParseQrAdminUserId(userId, out var qrAdminUserId))
            {
                var appCustomerId = await _context.Users
                    .Where(u => u.Id == qrAdminUserId)
                    .Select(u => u.CustomerId)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                if (appCustomerId.HasValue)
                    return appCustomerId.Value;

                var fallbackCustomerId = await FindUsersTableCustomerIdAsync(qrAdminUserId).ConfigureAwait(false);
                if (fallbackCustomerId.HasValue)
                    return fallbackCustomerId.Value;

                var linkedCustomerId = await _context.AppUserCustomers
                    .AsNoTracking()
                    .Where(uc => uc.AppUserId == qrAdminUserId && uc.MasterClientesId > 0)
                    .OrderBy(uc => uc.MasterClientesId)
                    .Select(uc => (int?)uc.MasterClientesId)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                if (linkedCustomerId.HasValue)
                    return linkedCustomerId.Value;

                return await ResolvePrimaryTenantIdAsync().ConfigureAwait(false);
            }

            if (TryParseApplicationUserId(userId, out _))
            {
                // ApplicationUsers admins are not linked to MasterSubClientes (typical single-site install).
                return await ResolvePrimaryTenantIdAsync().ConfigureAwait(false);
            }

            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.CustomerId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Tenant used by admins that are not linked to a customer (QR admins, ApplicationUsers admins).
        /// Picked by machine count among customers that still exist and are not deleted, because databases
        /// contain machines pointing at orphan/test customer ids that would otherwise win a lowest-id pick.
        /// </summary>
        private async Task<int?> ResolvePrimaryTenantIdAsync()
        {
            if (IsPrimaryTenantCacheFresh())
                return _primaryTenantId;

            await PrimaryTenantGate.WaitAsync().ConfigureAwait(false);
            try
            {
                if (IsPrimaryTenantCacheFresh())
                    return _primaryTenantId;

                var tenantId = await (
                    from machine in _context.MasterMaquinaDispensacions.AsNoTracking()
                    join client in _context.MasterClientes.AsNoTracking()
                        on machine.IdCliente equals (int?)client.Id
                    where machine.IdCliente > 0 && (client.Borrado == null || client.Borrado == false)
                    group machine by machine.IdCliente!.Value into byClient
                    orderby byClient.Count() descending, byClient.Key
                    select (int?)byClient.Key)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                tenantId ??= await _context.MasterMaquinaDispensacions
                    .AsNoTracking()
                    .Where(m => m.IdCliente > 0)
                    .GroupBy(m => m.IdCliente!.Value)
                    .OrderByDescending(g => g.Count())
                    .ThenBy(g => g.Key)
                    .Select(g => (int?)g.Key)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                _primaryTenantId = tenantId;
                _primaryTenantCachedAtUtc = DateTime.UtcNow;
                return tenantId;
            }
            finally
            {
                PrimaryTenantGate.Release();
            }
        }

        private static bool IsPrimaryTenantCacheFresh()
            => _primaryTenantId.HasValue && DateTime.UtcNow - _primaryTenantCachedAtUtc < PrimaryTenantCacheTtl;

        private async Task<(int? Few, int? LowPercent, int? Collector)> GetAvisoLimitsBundleAsync()
        {
            const string cacheKey = "aviso:limits:bundle";
            if (_cache.TryGetValue(cacheKey, out (int? Few, int? LowPercent, int? Collector) cached))
                return cached;

            var rows = await _context.MasterAvisosPorEmails
                .AsNoTracking()
                .Where(a => (a.Tipo == 2 || a.Tipo == 4 || a.Tipo == 11) && a.LimiteNumerico.HasValue)
                .GroupBy(a => a.Tipo)
                .Select(g => new { Tipo = g.Key, Limit = g.Max(x => x.LimiteNumerico) })
                .ToListAsync()
                .ConfigureAwait(false);

            int? Pick(int tipo) => rows.FirstOrDefault(r => r.Tipo == tipo)?.Limit;
            var bundle = (Pick(2), Pick(11), Pick(4));
            _cache.Set(cacheKey, bundle, DashboardCacheTtl);
            return bundle;
        }

        private async Task<int?> GetAvisoLimitAsync(int tipo)
        {
            var (few, low, collector) = await GetAvisoLimitsBundleAsync().ConfigureAwait(false);
            return tipo switch
            {
                2 => few,
                11 => low,
                4 => collector,
                _ => await _context.MasterAvisosPorEmails
                    .AsNoTracking()
                    .Where(a => a.Tipo == tipo && a.LimiteNumerico.HasValue)
                    .Select(a => a.LimiteNumerico)
                    .OrderByDescending(v => v)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false)
            };
        }

        private async Task<Dictionary<int, int>> LoadLatestCollectorCountersAsync(int[] machineIds, int clientId, bool tenantScoped)
        {
            if (machineIds.Length == 0)
                return new Dictionary<int, int>();

            var query = _context.MovMaquinaContadorPrendasSucias
                .AsNoTracking()
                .Where(c => machineIds.Contains(c.IdMaquina));
            if (tenantScoped)
                query = query.Where(c => c.IdCliente == clientId);

            var rows = await query
                .Select(c => new
                {
                    c.IdMaquina,
                    c.Contador,
                    c.FechaHoraUltimoCambio,
                    c.Id
                })
                .ToListAsync()
                .ConfigureAwait(false);

            return rows
                .GroupBy(r => r.IdMaquina)
                .ToDictionary(
                    g => g.Key,
                    g => g
                        .OrderByDescending(x => x.FechaHoraUltimoCambio)
                        .ThenByDescending(x => x.Id)
                        .Select(x => x.Contador)
                        .FirstOrDefault());
        }

        private sealed class DispenserStockRow
        {
            public int MachineId { get; set; }
            public int StockReal { get; set; }
            public int MaxCapacity { get; set; }
        }

        private async Task<(string Id, string? Name, string? SurName1, string? SurName2)?> FindUsersTableRowByIdAsync(string id)
        {
            var conn = _context.Database.GetDbConnection();
            var wasOpen = conn.State == System.Data.ConnectionState.Open;
            if (!wasOpen)
                await conn.OpenAsync().ConfigureAwait(false);
            try
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    SELECT TOP 1 [Id], [Name], [SurName1], [SurName2]
                    FROM dbo.[Users]
                    WHERE [Id] = @id AND ([Deleted] = 0 OR [Deleted] IS NULL)";
                var p = cmd.CreateParameter();
                p.ParameterName = "@id";
                p.Value = id;
                cmd.Parameters.Add(p);
                using var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);
                if (!await reader.ReadAsync().ConfigureAwait(false))
                    return null;
                return (
                    reader.IsDBNull(0) ? "" : reader.GetString(0),
                    reader.IsDBNull(1) ? null : reader.GetString(1),
                    reader.IsDBNull(2) ? null : reader.GetString(2),
                    reader.IsDBNull(3) ? null : reader.GetString(3));
            }
            finally
            {
                if (!wasOpen)
                    await conn.CloseAsync().ConfigureAwait(false);
            }
        }

        private async Task<int?> FindUsersTableCustomerIdAsync(string id)
        {
            var conn = _context.Database.GetDbConnection();
            var wasOpen = conn.State == System.Data.ConnectionState.Open;
            if (!wasOpen)
                await conn.OpenAsync().ConfigureAwait(false);
            try
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    SELECT TOP 1 [CustomerId]
                    FROM dbo.[Users]
                    WHERE [Id] = @id AND ([Deleted] = 0 OR [Deleted] IS NULL)";
                var p = cmd.CreateParameter();
                p.ParameterName = "@id";
                p.Value = id;
                cmd.Parameters.Add(p);
                var result = await cmd.ExecuteScalarAsync().ConfigureAwait(false);
                if (result == null || result == DBNull.Value)
                    return null;
                return Convert.ToInt32(result);
            }
            finally
            {
                if (!wasOpen)
                    await conn.CloseAsync().ConfigureAwait(false);
            }
        }
    }
}
