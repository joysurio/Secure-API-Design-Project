using LFS.InfraStructure;
using LFS.Shared.Entities;
using LFS.SecureAPI.Endpoints;
using LFS.SecureAPI.Middleware;
using LFS.SecureAPI.Models;
using LFS.SecureAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MobileQrPairingOptions>(
    builder.Configuration.GetSection(MobileQrPairingOptions.SectionName));

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var bearerScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter: Bearer {your JWT token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition("Bearer", bearerScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { bearerScheme, Array.Empty<string>() }
    });
});

// Database Context — longer command timeout for remote SQL Express (MovPren aggregates).
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.CommandTimeout(90)));

// Identity (for user login)
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

// JWT Configuration
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "LFS.SecureAPI";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "LFS.Clients";

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        // Keep JWT claim types as-is ("sub", "auth_type"). Default remaps sub -> NameIdentifier and breaks FindFirst("sub").
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            NameClaimType = "sub"
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var authType = context.Principal?.FindFirst("auth_type")?.Value;
                if (authType == "user" || authType == "admin_user")
                {
                    // User/admin login tokens — not tied to a device row; skip device check.
                    return;
                }

                var deviceId = context.Principal?.FindFirst("sub")?.Value;
                if (!string.IsNullOrEmpty(deviceId))
                {
                    var dbContext = context.HttpContext.RequestServices.GetRequiredService<DataContext>();
                    var device = await dbContext.Devices.FirstOrDefaultAsync(d => d.DeviceId == deviceId);

                    if (device == null || !device.IsActive)
                    {
                        context.Fail("Device is not active or not found");
                        return;
                    }

                    device.LastSeen = DateTime.UtcNow;
                    await dbContext.SaveChangesAsync();
                }
            }
        };
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    // API should return 401/403, not HTML redirects.
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});

// Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Scope:rfid.read", policy => policy.RequireAssertion(ctx =>
        ctx.User.HasClaim(c => c.Type == "scope" && c.Value.Split(' ').Contains("rfid:read"))));
    
    options.AddPolicy("Scope:rfid.write", policy => policy.RequireAssertion(ctx =>
        ctx.User.HasClaim(c => c.Type == "scope" && c.Value.Split(' ').Contains("rfid:write"))));
    
    options.AddPolicy("Scope:items.read", policy => policy.RequireAssertion(ctx =>
        ctx.User.HasClaim(c => c.Type == "scope" && c.Value.Split(' ').Contains("items:read"))));
    
    options.AddPolicy("Scope:items.write", policy => policy.RequireAssertion(ctx =>
        ctx.User.HasClaim(c => c.Type == "scope" && c.Value.Split(' ').Contains("items:write"))));
    
    options.AddPolicy("Scope:machines.read", policy => policy.RequireAssertion(ctx =>
        ctx.User.HasClaim(c => c.Type == "scope" && c.Value.Split(' ').Contains("machines:read"))));
    
    options.AddPolicy("Scope:machines.write", policy => policy.RequireAssertion(ctx =>
        ctx.User.HasClaim(c => c.Type == "scope" && c.Value.Split(' ').Contains("machines:write"))));
    
    options.AddPolicy("Scope:offline.sync", policy => policy.RequireAssertion(ctx =>
        ctx.User.HasClaim(c => c.Type == "scope" && c.Value.Split(' ').Contains("offline:sync"))));
    
    options.AddPolicy("Scope:audit.read", policy => policy.RequireAssertion(ctx =>
        ctx.User.HasClaim(c => c.Type == "scope" && c.Value.Split(' ').Contains("audit:read"))));

    // SubCliente / AppUser logins use auth_type "user"; ApplicationUsers-only QR admins use "admin_user".
    options.AddPolicy("RequireUserAuth", policy => policy.RequireAssertion(ctx =>
        ctx.User.HasClaim("auth_type", "user")
        || ctx.User.HasClaim("auth_type", "admin_user")));
});

// Register Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRfidService, RfidService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IMovementService, MovementService>();
builder.Services.AddScoped<IMachineService, MachineService>();
builder.Services.AddScoped<ILookupService, LookupService>();
builder.Services.AddScoped<IAccessLogService, AccessLogService>();
builder.Services.AddScoped<IMasterDataService, MasterDataService>();
builder.Services.AddScoped<IMovPrenService, MovPrenService>();
builder.Services.AddScoped<ILinenService, LinenService>();
builder.Services.AddScoped<IUserAuthService, UserAuthService>();
builder.Services.AddScoped<ISubClienteAuthService, SubClienteAuthService>();
builder.Services.AddScoped<IApplicationUsersAdminService, ApplicationUsersAdminService>();
builder.Services.AddScoped<IAuthSessionService, AuthSessionService>();
builder.Services.AddScoped<IRefreshTokenStore, EfRefreshTokenStore>();
builder.Services.AddScoped<IMeService, MeService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ISupportTicketService, SupportTicketService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Rate Limiting (simple in-memory implementation)
builder.Services.AddMemoryCache();

// Reverse proxy / load balancer — run early so Scheme/Host/PathBase match the public URL.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor
        | ForwardedHeaders.XForwardedProto
        | ForwardedHeaders.XForwardedHost
        | ForwardedHeaders.XForwardedPrefix;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();

app.UseForwardedHeaders();

// IIS virtual app or manual: same URL prefix for every API call (fixes login 200 + others 404 in Swagger).
// Set Hosting:PathBase in appsettings OR environment variable ASPNETCORE_PATHBASE (e.g. "/LfsSecureApi").
var hostingPathBase = builder.Configuration["Hosting:PathBase"];
if (string.IsNullOrWhiteSpace(hostingPathBase))
    hostingPathBase = Environment.GetEnvironmentVariable("ASPNETCORE_PATHBASE");
if (!string.IsNullOrWhiteSpace(hostingPathBase))
    app.UsePathBase(hostingPathBase.TrimEnd('/'));

// Initialize database tables if they don't exist
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    
    try
    {
        // Check if AccessLog table exists, if not create it
        dbContext.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccessLog]') AND type in (N'U'))
            BEGIN
                CREATE TABLE [dbo].[AccessLog] (
                    [Id] INT IDENTITY(1,1) PRIMARY KEY,
                    [TimestampUtc] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
                    [UserId] INT NULL,
                    [DeviceId] NVARCHAR(100) NULL,
                    [Endpoint] NVARCHAR(200) NOT NULL,
                    [Method] NVARCHAR(10) NOT NULL,
                    [ParamsJson] NVARCHAR(MAX) NULL,
                    [Success] BIT NOT NULL,
                    [ResponseStatus] INT NOT NULL,
                    [IpAddress] NVARCHAR(45) NULL
                );
                
                CREATE INDEX IX_AccessLog_TimestampUtc ON [AccessLog]([TimestampUtc]);
                CREATE INDEX IX_AccessLog_DeviceId ON [AccessLog]([DeviceId]);
                CREATE INDEX IX_AccessLog_UserId ON [AccessLog]([UserId]);
            END
        ");
        
        // Check if Devices table exists, if not create it
        dbContext.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Devices]') AND type in (N'U'))
            BEGIN
                CREATE TABLE [dbo].[Devices] (
                    [Id] INT IDENTITY(1,1) PRIMARY KEY,
                    [DeviceId] NVARCHAR(100) NOT NULL UNIQUE,
                    [SecretHash] NVARCHAR(256) NOT NULL,
                    [OwnerClientId] INT NULL,
                    [AllowedScopes] NVARCHAR(MAX) NULL,
                    [IsActive] BIT NOT NULL DEFAULT 1,
                    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
                    [LastSeen] DATETIME2 NULL
                );
                
                CREATE UNIQUE INDEX IX_Devices_DeviceId ON [Devices]([DeviceId]);
            END
        ");
        
        // Check if OperationsLog table exists, if not create it
        dbContext.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OperationsLog]') AND type in (N'U'))
            BEGIN
                CREATE TABLE [dbo].[OperationsLog] (
                    [OperationId] UNIQUEIDENTIFIER PRIMARY KEY,
                    [DeviceId] NVARCHAR(100) NULL,
                    [UserId] INT NULL,
                    [Endpoint] NVARCHAR(200) NOT NULL,
                    [PayloadHash] NVARCHAR(64) NOT NULL,
                    [ProcessedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE()
                );
                
                CREATE INDEX IX_OperationsLog_DeviceId ON [OperationsLog]([DeviceId]);
                CREATE INDEX IX_OperationsLog_ProcessedAt ON [OperationsLog]([ProcessedAt]);
            END
        ");
        
        // Check if LinenOperations table exists, if not create it
        // Note: Foreign key constraint will be created by Entity Framework Core through model configuration
        dbContext.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LinenOperations]') AND type in (N'U'))
            BEGIN
                CREATE TABLE [dbo].[LinenOperations] (
                    [Id] INT IDENTITY(1,1) PRIMARY KEY,
                    [RFIDCode] NVARCHAR(100) NOT NULL,
                    [ItemId] INT NOT NULL,
                    [OperationType] NVARCHAR(50) NOT NULL,
                    [Remark] NVARCHAR(500) NULL,
                    [DeviceId] NVARCHAR(100) NULL,
                    [UserId] INT NULL,
                    [OperationDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
                    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
                    [Deleted] BIT NOT NULL DEFAULT 0
                );
                
                CREATE INDEX IX_LinenOperations_RFIDCode ON [LinenOperations]([RFIDCode]);
                CREATE INDEX IX_LinenOperations_ItemId ON [LinenOperations]([ItemId]);
                CREATE INDEX IX_LinenOperations_OperationType ON [LinenOperations]([OperationType]);
                CREATE INDEX IX_LinenOperations_OperationDate ON [LinenOperations]([OperationDate]);
            END
        ");
        
        // Create foreign key constraint separately if table exists and constraint doesn't exist
        // This allows the table to be created even if Items table doesn't exist yet
        dbContext.Database.ExecuteSqlRaw(@"
            IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LinenOperations]') AND type in (N'U'))
            AND NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_LinenOperations_Items')
            AND EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Items]') AND type in (N'U'))
            BEGIN
                ALTER TABLE [dbo].[LinenOperations]
                ADD CONSTRAINT FK_LinenOperations_Items FOREIGN KEY ([ItemId]) REFERENCES [Items]([Id]);
            END
        ");
        
        // AuthRefreshTokens — persisted mobile refresh sessions (see EfRefreshTokenStore)
        dbContext.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuthRefreshTokens]') AND type in (N'U'))
            BEGIN
                CREATE TABLE [dbo].[AuthRefreshTokens] (
                    [Token] VARCHAR(64) NOT NULL,
                    [Subject] NVARCHAR(450) NOT NULL,
                    [ExpiresAtUtc] DATETIME2 NOT NULL,
                    [Revoked] BIT NOT NULL CONSTRAINT [DF_AuthRefreshTokens_Revoked] DEFAULT (0),
                    [CreatedAtUtc] DATETIME2 NOT NULL CONSTRAINT [DF_AuthRefreshTokens_CreatedAtUtc] DEFAULT (SYSUTCDATETIME()),
                    CONSTRAINT [PK_AuthRefreshTokens] PRIMARY KEY CLUSTERED ([Token])
                );

                CREATE NONCLUSTERED INDEX [IX_AuthRefreshTokens_Revoked_Expires]
                    ON [dbo].[AuthRefreshTokens] ([Revoked], [ExpiresAtUtc]);
            END
        ");

        // MovPren tenant/date lookups (admin consumption, home dashboard) — full scans time out on remote SQL Express.
        try
        {
            dbContext.Database.ExecuteSqlRaw(@"
                IF OBJECT_ID(N'[dbo].[MovPren]', N'U') IS NOT NULL
                AND NOT EXISTS (
                    SELECT 1 FROM sys.indexes
                    WHERE name = N'IX_MovPren_idCli_Fecha' AND object_id = OBJECT_ID(N'[dbo].[MovPren]'))
                BEGIN
                    CREATE NONCLUSTERED INDEX [IX_MovPren_idCli_Fecha]
                        ON [dbo].[MovPren] ([idCli], [Fecha] DESC)
                        INCLUDE ([idModeloPrenda], [id_TipoAntena], [idSubCli], [talla], [AlmacenDispensadora], [id_Puesto]);
                END
            ");
        }
        catch (Exception indexEx)
        {
            logger.LogWarning(indexEx, "Could not create IX_MovPren_idCli_Fecha (may already exist).");
        }

        logger.LogInformation("Security tables verified/created successfully.");
    }
    catch (Exception ex)
    {
        logger.LogWarning(ex, "Could not verify/create security tables. They may need to be created manually. See CREATE_SECURITY_TABLES.sql");
    }
}

// Configure the HTTP request pipeline
var enableSwagger = app.Environment.IsDevelopment()
    || builder.Configuration.GetValue("Hosting:EnableSwagger", false);
if (enableSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Relative to the Swagger UI page — fixes 404 when the app is hosted as an IIS application under a folder.
        // (Absolute "/swagger/v1/swagger.json" loads from site root and breaks for virtual apps.)
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint("v1/swagger.json", "LFS.SecureAPI v1");
        c.DisplayRequestDuration();
    });
}

if (!builder.Configuration.GetValue("Hosting:DisableHttpsRedirection", false))
    app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

// Access Logging Middleware (must be before authentication)
app.UseMiddleware<AccessLoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// Same path as basic health check; use query to read sizes if other URLs 404 (proxy/IIS).
app.MapGet("/api/health", async (HttpContext ctx, IMeService me) =>
{
    var action = ctx.Request.Query["action"].FirstOrDefault();
    if (string.Equals(action, "subcliente-sizes", StringComparison.OrdinalIgnoreCase))
        return await MeSizesEndpoints.GetSubClienteOnlySizes(ctx, me);
    if (string.Equals(action, "me-sizes", StringComparison.OrdinalIgnoreCase))
        return await MeSizesEndpoints.GetMeSizes(ctx, me);

    return Results.Json(new
    {
        ok = true,
        assembly = typeof(Program).Assembly.GetName().Name,
        environment = ctx.RequestServices.GetRequiredService<IHostEnvironment>().EnvironmentName,
        pathBase = ctx.Request.PathBase.Value ?? "",
        path = ctx.Request.Path.Value ?? "",
        hint404 = "If login works but other APIs 404 in Swagger, PathBase here must match your IIS app path — set Hosting:PathBase or ASPNETCORE_PATHBASE. Open this URL in the browser where Swagger runs and compare.",
        sizesViaSameUrl = "GET /api/health?action=subcliente-sizes (Bearer) for APK garment list; PUT /api/health?action=subcliente-sizes with JSON body to save",
        sizesDirectUrls = new[]
        {
            "/api/health/subcliente-sizes",
            "/api/subcliente/garment-sizes",
            "/api/me/sizes"
        }
    });
}).AllowAnonymous().ExcludeFromDescription();

app.MapPut("/api/health", async (HttpContext ctx, IMeService me, JsonElement body) =>
{
    var action = ctx.Request.Query["action"].FirstOrDefault();
    if (string.Equals(action, "subcliente-sizes", StringComparison.OrdinalIgnoreCase))
        return await MeSizesEndpoints.PutSubClienteOnlySizes(ctx, me, body);
    if (string.Equals(action, "me-sizes", StringComparison.OrdinalIgnoreCase))
        return await MeSizesEndpoints.PutMeSizes(ctx, me, body);
    return Results.BadRequest(new { error = "Add ?action=subcliente-sizes or ?action=me-sizes" });
}).AllowAnonymous().ExcludeFromDescription();

// Sizes: minimal APIs — hidden from Swagger; use URLs documented in Auth/Me or GET /api/health JSON hints.
app.MapMeSizesEndpoints();

app.MapControllers();

app.Run();
