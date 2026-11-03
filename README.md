# LFS Secure API - Technical Documentation

## API Continuation (Client Requirement) - March 2026

This repository now includes the continuation MVP requested by the client:

- `POST /api/auth/refresh` and `POST /api/auth/logout` for user/subcliente sessions.
- `POST /api/transactions/{id}/cancel` for transaction reversal.
- `POST /api/notifications/register` to store push device tokens.
- `POST /api/support/tickets` to create support incidents.
- Atomic stock/credit update flow in `POST /api/transactions` using a single SQL transaction.
- Delivery artifacts:
  - `DATABASE_MVP_EXTENSION.sql` (DDL for new tables)
  - `ERD_MVP.md` (ERD and endpoint-table mapping)

### Quick Run

1. Configure SQL connection in `LFS.SecureAPI/appsettings.json`.
2. Run `DATABASE_MVP_EXTENSION.sql` (or let endpoints auto-create tables).
3. Start API:
   - `dotnet run --project LFS.SecureAPI/LFS.SecureAPI.csproj`
4. Open Swagger and test:
   - `/api/auth/login` or `/api/auth/subcliente/login`
   - `/api/auth/refresh`
   - `/api/transactions`
   - `/api/notifications/register`
   - `/api/support/tickets`

**Version:** 1.0  
**Framework:** ASP.NET Core 7.0  
**Database:** SQL Server  
**Authentication:** JWT Bearer Tokens  
**Architecture:** RESTful API with Service Layer Pattern

## Code Metrics

| Metric | Value |
|--------|-------|
| **Total Lines of Code** | 2,425 |
| **Total C# Files** | 64 |
| **Controllers** | 9 files, 455 lines |
| **Services** | 20 files, 1,243 lines |
| **Models/DTOs** | 33 files, 411 lines |
| **Middleware** | 1 file, 112 lines |
| **Configuration** | 1 file (Program.cs), 204 lines |

### Code Distribution by Layer

```
┌─────────────────────────────────────────┐
│         Controllers (455 lines)          │
│         9 files, 18.8% of code            │
└─────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────┐
│         Services (1,243 lines)           │
│         20 files, 51.2% of code          │
└─────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────┐
│         Models/DTOs (411 lines)          │
│         33 files, 16.9% of code           │
└─────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────┐
│         Middleware (112 lines)           │
│         1 file, 4.6% of code              │
└─────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────┐
│         Configuration (204 lines)        │
│         1 file, 8.4% of code             │
└─────────────────────────────────────────┘
```

---

## Table of Contents

1. [Architecture Overview](#architecture-overview)
2. [Technology Stack](#technology-stack)
3. [Project Structure](#project-structure)
4. [Security Implementation](#security-implementation)
5. [Database Schema](#database-schema)
6. [API Endpoints](#api-endpoints)
7. [Service Layer Architecture](#service-layer-architecture)
8. [Middleware Implementation](#middleware-implementation)
9. [Configuration](#configuration)
10. [Authentication & Authorization](#authentication--authorization)
11. [Data Models & DTOs](#data-models--dtos)
12. [Error Handling](#error-handling)
13. [Performance Considerations](#performance-considerations)
14. [Deployment](#deployment)
15. [Database Migration](#database-migration)

---

## Architecture Overview

### System Architecture

The LFS Secure API is built using **ASP.NET Core 7.0** following a **layered architecture pattern** with clear separation of concerns:

```
┌─────────────────────────────────────────────────────────────┐
│                    HTTP Request Layer                        │
│  (Kestrel Web Server, HTTPS, Request Routing)               │
└─────────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                  Middleware Pipeline                         │
│  • AccessLoggingMiddleware (112 lines)                      │
│  • Authentication Middleware (JWT Bearer)                   │
│  • Authorization Middleware (Scope-based)                   │
│  • CORS Middleware                                           │
└─────────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                  Controllers Layer (455 lines)               │
│  • 9 Controllers (API Endpoints)                            │
│  • Request/Response handling                                │
│  • HTTP status code mapping                                 │
│  • Exception handling                                       │
└─────────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│                  Services Layer (1,243 lines)                │
│  • 20 Service classes (Business Logic)                      │
│  • 9 Interface definitions (Contracts)                      │
│  • Transaction management                                   │
│  • Data validation                                          │
│  • Business rule enforcement                                │
└─────────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│              Data Access Layer (EF Core)                     │
│  • DataContext (Entity Framework Core)                      │
│  • DbSet<T> collections                                     │
│  • LINQ queries                                             │
│  • Change tracking                                          │
│  • Transaction support                                      │
└─────────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────────┐
│              Database Layer (SQL Server)                     │
│  • Security Tables (Devices, AccessLog, OperationsLog)       │
│  • Business Tables (Items, Movements, Machines, etc.)       │
│  • Indexes for performance                                  │
└─────────────────────────────────────────────────────────────┘
```

### Detailed Component Architecture

```
┌──────────────────────────────────────────────────────────────┐
│                      API Layer                                │
├──────────────────────────────────────────────────────────────┤
│ AuthController (60 lines)                                    │
│   ├── POST /api/auth/device/register                        │
│   ├── POST /api/auth/token                                  │
│   └── POST /api/auth/revoke                                 │
│                                                              │
│ RfidController (43 lines)                                    │
│   ├── GET /api/rfid/item/{rfid}                             │
│   └── POST /api/rfid/scan                                   │
│                                                              │
│ InventoryController (39 lines)                               │
│   ├── POST /api/inventory/counts                            │
│   └── GET /api/inventory/reconcile/{locationId}             │
│                                                              │
│ MovementsController (56 lines)                               │
│   ├── POST /api/movements                                   │
│   ├── POST /api/movements/insert                            │
│   └── POST /api/items/{itemId}/movement                     │
│                                                              │
│ MachinesController (102 lines)                                │
│   ├── GET /api/machines/{id}                                │
│   ├── GET /api/machines/{id}/inventory                      │
│   ├── POST /api/machines/{id}/load                          │
│   ├── POST /api/machines/{id}/replenishment                 │
│   ├── POST /api/machines/{id}/credits                       │
│   └── POST /api/machines/{id}/delivery                      │
│                                                              │
│ MasterDataController (39 lines)                              │
│   ├── GET /api/masterdata/clients                           │
│   ├── GET /api/masterdata/garment-models                    │
│   └── GET /api/masterdata/antenna-types                     │
│                                                              │
│ LookupController (48 lines)                                 │
│   └── POST /api/lookup                                      │
│                                                              │
│ OfflineController (32 lines)                                │
│   └── POST /api/offline/sync                                │
│                                                              │
│ ItemsController (36 lines)                                   │
│   └── POST /api/items/{itemId}/movement                     │
└──────────────────────────────────────────────────────────────┘
                            ↓
┌──────────────────────────────────────────────────────────────┐
│                   Service Layer                               │
├──────────────────────────────────────────────────────────────┤
│ AuthService (111 lines)                                      │
│   ├── RegisterDeviceAsync() - BCrypt hashing                 │
│   ├── GetTokenAsync() - JWT generation                      │
│   ├── RevokeDeviceAsync() - Device deactivation              │
│   └── GenerateJwtToken() - Private JWT creation              │
│                                                              │
│ RfidService (165 lines)                                      │
│   ├── GetItemByRfidAsync() - Item lookup                    │
│   ├── ScanRfidTagsAsync() - Batch processing                │
│   └── ComputeHash() - SHA256 for idempotency                │
│                                                              │
│ MovPrenService (139 lines)                                   │
│   ├── InsertMovPrenAsync() - Complex business logic         │
│   ├── InsertarPrendaAsync() - Garment creation              │
│   └── ObtenerIdSubClienteAsync() - Subclient lookup        │
│                                                              │
│ MachineService (203 lines)                                   │
│   ├── GetMachineAsync()                                     │
│   ├── GetMachineInventoryAsync()                            │
│   ├── RegisterMachineLoadAsync()                            │
│   ├── CreateReplenishmentAsync()                            │
│   ├── ReplenishCreditsAsync()                               │
│   └── RegisterDeliveryAsync()                               │
│                                                              │
│ LookupService (194 lines)                                   │
│   ├── LookupAsync() - Generic entity lookup                 │
│   ├── Entity whitelisting                                   │
│   ├── Field whitelisting                                    │
│   └── Relation whitelisting                                 │
│                                                              │
│ OfflineSyncService (112 lines)                              │
│   └── SyncOfflineOperationsAsync() - Batch sync            │
│                                                              │
│ InventoryService (80 lines)                                  │
│   ├── SubmitInventoryCountsAsync()                          │
│   └── ReconcileInventoryAsync()                             │
│                                                              │
│ MovementService (72 lines)                                   │
│   ├── CreateMovementAsync()                                 │
│   └── CreateItemMovementAsync()                             │
│                                                              │
│ MasterDataService (46 lines)                                 │
│   ├── GetClientsAsync()                                     │
│   ├── GetGarmentModelsAsync()                               │
│   └── GetAntennaTypesAsync()                                │
│                                                              │
│ AccessLogService (30 lines)                                  │
│   └── LogAccessAsync() - Access logging                     │
└──────────────────────────────────────────────────────────────┘
```

### Key Architectural Patterns

#### 1. Dependency Injection Pattern
- **Implementation**: ASP.NET Core built-in DI container
- **Registration**: All services registered in `Program.cs` (204 lines)
- **Lifetime**: Scoped (one instance per HTTP request)
- **Benefits**: Loose coupling, testability, maintainability
- **Code Example**:
  ```csharp
  builder.Services.AddScoped<IAuthService, AuthService>();
  builder.Services.AddScoped<IRfidService, RfidService>();
  // ... 7 more service registrations
  ```

#### 2. Repository Pattern (via EF Core)
- **Implementation**: Entity Framework Core as data access abstraction
- **Location**: `DataContext` in `LFS.InfraStructure` project
- **Features**: 
  - `DbSet<T>` collections for each entity
  - LINQ query support
  - Change tracking
  - Transaction management
- **Benefits**: Abstraction from database, testability, query optimization

#### 3. DTO (Data Transfer Object) Pattern
- **Implementation**: 33 DTO classes (411 lines total)
- **Purpose**: 
  - Prevent over-posting attacks
  - Control API contract
  - Separate internal entities from API models
- **Examples**: `TokenRequest`, `ItemDto`, `RfidScanRequest`, etc.

#### 4. Service Layer Pattern
- **Implementation**: 20 service classes (1,243 lines)
- **Responsibilities**:
  - Business logic encapsulation
  - Data validation
  - Transaction management
  - Error handling
- **Interface Contracts**: 9 interfaces define service contracts

#### 5. Middleware Pipeline Pattern
- **Implementation**: `AccessLoggingMiddleware` (112 lines)
- **Position**: Before authentication in pipeline
- **Responsibilities**:
  - Request/response interception
  - Access logging
  - Error handling
- **Asynchronous**: Fire-and-forget logging to prevent blocking

#### 6. Strategy Pattern (Authorization)
- **Implementation**: Scope-based authorization policies
- **Location**: `Program.cs` lines 73-98
- **Policies**: 8 scope-based policies
- **Dynamic**: Policies evaluated per request based on token claims

#### 7. Factory Pattern (JWT Token Generation)
- **Implementation**: `AuthService.GenerateJwtToken()` method
- **Purpose**: Centralized JWT token creation
- **Configuration**: Uses `IConfiguration` for JWT settings

#### 8. Template Method Pattern (Service Methods)
- **Implementation**: Common patterns in service methods:
  - Idempotency check
  - Transaction begin
  - Business logic execution
  - Transaction commit/rollback
- **Examples**: `RfidService.ScanRfidTagsAsync()`, `OfflineSyncService.SyncOfflineOperationsAsync()`

### Request Flow Architecture

```
HTTP Request
    ↓
[Middleware Pipeline]
    ├── AccessLoggingMiddleware (captures request)
    ├── CORS Middleware
    ├── Authentication Middleware (JWT validation)
    └── Authorization Middleware (scope checking)
    ↓
[Controller]
    ├── Parameter binding
    ├── Model validation
    └── Service invocation
    ↓
[Service Layer]
    ├── Business logic execution
    ├── Data validation
    ├── Database operations (via EF Core)
    └── Transaction management
    ↓
[Entity Framework Core]
    ├── LINQ to SQL translation
    ├── Query execution
    └── Change tracking
    ↓
[SQL Server Database]
    ├── Query execution
    ├── Transaction management
    └── Result return
    ↓
[Response Pipeline]
    ├── DTO mapping
    ├── JSON serialization
    └── HTTP response
    ↓
[AccessLoggingMiddleware] (captures response)
    └── Async logging to database
    ↓
HTTP Response
```

### Security Architecture

```
┌──────────────────────────────────────────────────────────────┐
│                    Security Layers                           │
├──────────────────────────────────────────────────────────────┤
│ Layer 1: Network Security                                    │
│   • HTTPS/TLS encryption                                     │
│   • CORS policy enforcement                                  │
│                                                              │
│ Layer 2: Authentication                                      │
│   • JWT Bearer token validation                             │
│   • Device credential verification (BCrypt)                 │
│   • Token expiration checking                                │
│                                                              │
│ Layer 3: Authorization                                       │
│   • Scope-based access control                               │
│   • Policy-based authorization                              │
│   • Device status validation                                 │
│                                                              │
│ Layer 4: Data Security                                       │
│   • DTO pattern (prevents over-posting)                      │
│   • Field whitelisting (LookupService)                       │
│   • Entity whitelisting                                      │
│   • SQL injection prevention (EF Core parameterized)        │
│                                                              │
│ Layer 5: Audit & Monitoring                                 │
│   • Access logging (all requests)                            │
│   • Operation logging (idempotency)                          │
│   • Error logging                                            │
└──────────────────────────────────────────────────────────────┘
```

---

## Technology Stack

### Core Framework & Libraries

| Component | Version | Purpose |
|-----------|---------|---------|
| .NET | 7.0 | Runtime framework |
| ASP.NET Core | 7.0.5 | Web API framework |
| Entity Framework Core | 7.0.5 | ORM for database access |
| Entity Framework Core SQL Server | 7.0.5 | SQL Server provider |
| JWT Bearer Authentication | 7.0.5 | Token-based authentication |
| BCrypt.Net-Next | 4.0.3 | Password hashing |
| Swashbuckle (Swagger) | 6.5.0 | API documentation |
| System.IdentityModel.Tokens.Jwt | 7.0.0 | JWT token handling |

### Development Tools

- **Visual Studio 2022** or **VS Code** with C# extension
- **.NET 7.0 SDK** (required)
- **SQL Server** (2019 or later recommended)
- **Swagger UI** (included for API testing)

---

## Project Structure

### Directory Structure with Code Metrics

```
LFS.SecureAPI/ (2,425 total lines)
│
├── Controllers/ (565 lines, 10 files, 18.8% of code)
│   ├── AuthController.cs (60 lines)           # Device registration, token issuance
│   ├── RfidController.cs (43 lines)          # RFID scanning operations
│   ├── InventoryController.cs (39 lines)     # Inventory management
│   ├── MovementsController.cs (56 lines)     # Movement creation
│   ├── ItemsController.cs (36 lines)         # Item-specific operations
│   ├── MachinesController.cs (102 lines)     # Machine integration (largest controller)
│   ├── LinenController.cs (110 lines)        # Linen management workflow
│   ├── OfflineController.cs (32 lines)       # Offline synchronization
│   ├── LookupController.cs (48 lines)        # Generic entity lookup
│   └── MasterDataController.cs (39 lines)    # Master data endpoints
│
├── Services/ (1,243 lines, 20 files, 51.2% of code)
│   ├── Interfaces/ (10 interface files)
│   │   ├── IAuthService.cs (10 lines)
│   │   ├── IRfidService.cs (9 lines)
│   │   ├── IInventoryService.cs (9 lines)
│   │   ├── IMovementService.cs (9 lines)
│   │   ├── IMachineService.cs (13 lines)
│   │   ├── ILinenService.cs (14 lines)
│   │   ├── ILookupService.cs (8 lines)
│   │   ├── IOfflineSyncService.cs (8 lines)
│   │   ├── IMasterDataService.cs (10 lines)
│   │   └── IMovPrenService.cs (8 lines)
│   │
│   └── Implementations/ (12 service classes)
│       ├── AuthService.cs (111 lines)             # Authentication & device management
│       ├── RfidService.cs (165 lines)              # RFID tag processing
│       ├── InventoryService.cs (80 lines)          # Inventory operations
│       ├── MovementService.cs (72 lines)           # Movement tracking
│       ├── MachineService.cs (203 lines)           # Machine operations
│       ├── LinenService.cs (658 lines)             # Linen management workflow (largest service)
│       ├── OfflineSyncService.cs (112 lines)       # Offline sync processing
│       ├── LookupService.cs (194 lines)            # Generic lookup with whitelisting
│       ├── MasterDataService.cs (46 lines)         # Master data retrieval
│       ├── MovPrenService.cs (139 lines)           # Garment movement (Spanish spec)
│       └── AccessLogService.cs (30 lines)          # Access logging service
│
├── Models/ (411 lines, 33 files, 16.9% of code)
│   ├── Request Models/ (20 files)
│   │   ├── TokenRequest.cs (17 lines)
│   │   ├── DeviceRegistrationRequest.cs (15 lines)
│   │   ├── RfidScanRequest.cs (25 lines)          # Largest request model
│   │   ├── InventoryCountRequest.cs (21 lines)
│   │   ├── InsertMovPrenRequest.cs (20 lines)
│   │   ├── OfflineSyncRequest.cs (20 lines)
│   │   ├── MachineLoadRequest.cs (12 lines)
│   │   ├── ReplenishmentRequest.cs (16 lines)
│   │   ├── CreditsRequest.cs (13 lines)
│   │   ├── DeliveryRequest.cs (10 lines)
│   │   ├── TagRegistrationRequest.cs (35 lines)    # Linen tag registration
│   │   ├── LinenDeliveryRequest.cs (29 lines)      # Linen delivery
│   │   ├── AbnormalMarkingRequest.cs (30 lines)   # Abnormal marking
│   │   ├── WashedItemsImportRequest.cs (21 lines)  # Washed items import
│   │   ├── RandomCheckRequest.cs (18 lines)        # Random check
│   │   ├── CreateMovementRequest.cs (10 lines)
│   │   ├── CreateItemMovementRequest.cs (10 lines)
│   │   ├── LookupRequest.cs (19 lines)
│   │   ├── RevokeDeviceRequest.cs (9 lines)
│   │   └── [Additional request models...]
│   │
│   ├── Response Models/ (20 files)
│   │   ├── TokenResponse.cs (10 lines)
│   │   ├── DeviceRegistrationResponse.cs (8 lines)
│   │   ├── RfidScanResponse.cs (16 lines)
│   │   ├── InventoryCountResponse.cs (9 lines)
│   │   ├── InventoryReconcileResponse.cs (9 lines)
│   │   ├── InsertMovPrenResponse.cs (10 lines)
│   │   ├── OfflineSyncResponse.cs (10 lines)
│   │   ├── MachineLoadResponse.cs (10 lines)
│   │   ├── ReplenishmentResponse.cs (9 lines)
│   │   ├── CreditsResponse.cs (9 lines)
│   │   ├── DeliveryResponse.cs (9 lines)
│   │   ├── TagRegistrationResponse.cs (18 lines)   # Linen tag registration
│   │   ├── LinenDeliveryResponse.cs (18 lines)     # Linen delivery
│   │   ├── AbnormalMarkingResponse.cs (18 lines)   # Abnormal marking
│   │   ├── WashedItemsImportResponse.cs (25 lines) # Washed items import
│   │   ├── RandomCheckResponse.cs (19 lines)        # Random check
│   │   ├── MovementResponse.cs (9 lines)
│   │   ├── LookupResponse.cs (8 lines)
│   │   └── [Additional response models...]
│   │
│   └── DTOs/ (3 files)
│       ├── ItemDto.cs (15 lines)
│       ├── MachineDto.cs (10 lines)
│       ├── ClientDto.cs (8 lines)
│       ├── GarmentModelDto.cs (8 lines)
│       └── AntennaTypeDto.cs (8 lines)
│
├── Middleware/ (112 lines, 1 file, 4.6% of code)
│   └── AccessLoggingMiddleware.cs (112 lines)  # Request logging middleware
│
├── Program.cs (204 lines, 8.4% of code)        # Application startup & configuration
├── appsettings.json                            # Configuration file
├── appsettings.Development.json               # Development configuration
└── LFS.SecureAPI.csproj                        # Project file
```

### Code Statistics by Component

| Component | Files | Lines | % of Total | Avg Lines/File |
|-----------|-------|-------|------------|----------------|
| **Services** | 20 | 1,243 | 51.2% | 62.2 |
| **Controllers** | 9 | 455 | 18.8% | 50.6 |
| **Models** | 33 | 411 | 16.9% | 12.5 |
| **Configuration** | 1 | 204 | 8.4% | 204.0 |
| **Middleware** | 1 | 112 | 4.6% | 112.0 |
| **Total** | **64** | **2,425** | **100%** | **37.9** |

### Largest Files

| File | Lines | Purpose |
|------|-------|---------|
| `Program.cs` | 204 | Application configuration & startup |
| `MachineService.cs` | 203 | Machine operations business logic |
| `LookupService.cs` | 194 | Generic entity lookup with whitelisting |
| `RfidService.cs` | 165 | RFID tag processing |
| `MovPrenService.cs` | 139 | Garment movement business logic |
| `AccessLoggingMiddleware.cs` | 112 | Request logging middleware |
| `OfflineSyncService.cs` | 112 | Offline synchronization |
| `AuthService.cs` | 111 | Authentication & JWT generation |
| `MachinesController.cs` | 102 | Machine API endpoints |

### Dependencies

- **LFS.Shared**: Contains entity definitions (`Item`, `Movement`, `Device`, `AccessLog`, etc.)
- **LFS.InfraStructure**: Contains `DataContext` and database configuration

---

## Security Implementation

### Authentication Flow

1. **Device Registration** (One-time setup)
   ```
   POST /api/auth/device/register
   → Device registered with BCrypt-hashed secret
   → Scopes assigned to device
   → Device stored in Devices table
   ```

2. **Token Issuance** (Per-request or cached)
   ```
   POST /api/auth/token
   → Device authenticates with DeviceId + Secret
   → BCrypt.Verify() validates secret
   → JWT token generated with device claims
   → Token includes: sub (deviceId), scope, jti, iss, aud
   → Token expires in 15 minutes (configurable)
   ```

3. **Token Validation** (Per API request)
   ```
   Request with Bearer token
   → JWT validated (signature, issuer, audience, expiration)
   → Device status checked in database
   → LastSeen timestamp updated
   → Request proceeds if valid
   ```

### Security Features

#### 1. BCrypt Password Hashing
- **Algorithm**: BCrypt with cost factor 11 (default)
- **Storage**: Only hashed secrets stored, never plaintext
- **Verification**: `BCrypt.Net.BCrypt.Verify(plainSecret, hashedSecret)`

#### 2. JWT Token Security
- **Algorithm**: HMAC SHA-256 (HS256)
- **Key**: Minimum 32 characters (configured in `appsettings.json`)
- **Claims**:
  - `sub`: Device ID
  - `scope`: Space-separated list of allowed scopes
  - `jti`: Unique JWT ID (GUID)
  - `iss`: Issuer (LFS.SecureAPI)
  - `aud`: Audience (LFS.Clients)
- **Expiration**: 15 minutes (configurable)
- **Clock Skew**: Zero (no tolerance for clock differences)

#### 3. Scope-Based Authorization
- **Policy Pattern**: Each scope has a corresponding authorization policy
- **Policy Registration**:
  ```csharp
  options.AddPolicy("Scope:rfid.read", policy => policy.RequireAssertion(ctx =>
      ctx.User.HasClaim(c => c.Type == "scope" && 
      c.Value.Split(' ').Contains("rfid:read"))));
  ```
- **Available Scopes**:
  - `rfid:read` - Read RFID/item data
  - `rfid:write` - Write RFID/item data
  - `items:read` - Read items
  - `items:write` - Write items
  - `machines:read` - Read machine data
  - `machines:write` - Write machine data
  - `offline:sync` - Offline synchronization
  - `audit:read` - Read audit logs

#### 4. Access Logging
- **Middleware**: `AccessLoggingMiddleware` logs all requests
- **Logged Data**:
  - Timestamp (UTC)
  - User/Device ID
  - Endpoint and HTTP method
  - Request parameters (limited to 10KB)
  - Success/failure status
  - HTTP status code
  - IP address
- **Implementation**: Asynchronous logging (fire-and-forget) to prevent blocking

#### 5. Idempotency
- **Mechanism**: UUID-based operation IDs
- **Storage**: `OperationsLog` table with `OperationId` as primary key
- **Process**:
  1. Client generates UUID for operation
  2. Server checks if `OperationId` exists
  3. If exists, returns original result
  4. If not, processes and stores result
- **Benefits**: Prevents duplicate processing, safe retries

---

## Database Schema

### Security Tables

#### Devices Table
```sql
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
```

**Entity Mapping**:
- `DeviceId`: Unique device identifier (e.g., "PDA-WAREHOUSE-01")
- `SecretHash`: BCrypt-hashed secret (never stored in plaintext)
- `OwnerClientId`: Optional client ownership
- `AllowedScopes`: Space-separated scope list
- `IsActive`: Device activation status
- `LastSeen`: Updated on each token validation

#### AccessLog Table
```sql
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
```

**Purpose**: Comprehensive audit trail of all API requests.

#### OperationsLog Table
```sql
CREATE TABLE [dbo].[OperationsLog] (
    [OperationId] UNIQUEIDENTIFIER PRIMARY KEY,
    [DeviceId] NVARCHAR(100) NULL,
    [UserId] INT NULL,
    [Endpoint] NVARCHAR(200) NOT NULL,
    [PayloadHash] NVARCHAR(MAX) NULL,
    [ProcessedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

CREATE INDEX IX_OperationsLog_DeviceId ON [OperationsLog]([DeviceId]);
CREATE INDEX IX_OperationsLog_ProcessedAt ON [OperationsLog]([ProcessedAt]);
```

**Purpose**: Idempotency tracking for batch operations.

#### LinenOperations Table
```sql
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

-- Foreign key constraint (created separately if Items table exists)
ALTER TABLE [dbo].[LinenOperations]
ADD CONSTRAINT FK_LinenOperations_Items FOREIGN KEY ([ItemId]) REFERENCES [Items]([Id]);
```

**Purpose**: Tracks all linen management operations (delivery, abnormal marking, random checks, washed items received).

**Entity Mapping**:
- `RFIDCode`: RFID tag code of the linen item
- `ItemId`: Foreign key to `Items` table
- `OperationType`: Type of operation ("Delivery", "Abnormal", "RandomCheck", "WashedReceived")
- `Remark`: Optional remark/note for the operation (e.g., delivery location, abnormal details)
- `DeviceId`: Device identifier that performed the operation
- `UserId`: User identifier who performed the operation
- `OperationDate`: Date and time when the operation occurred
- `CreatedAt`: Record creation timestamp
- `Deleted`: Soft delete flag

**Operation Types**:
- `"Delivery"` - Linen delivered to user/sector
- `"Abnormal"` - Linen marked as abnormal/damaged
- `"RandomCheck"` - Random quality check on washed linen
- `"WashedReceived"` - Washed items received from laundry company

### Existing Tables Used

The API integrates with existing database tables:
- `Items` - Garment/item records with RFID codes
- `Movements` - Item movement history
- `MasterPrendas` - Master garment data
- `MovPren` - Garment movement records
- `MasterClientes` - Client master data
- `MasterModeloPrenda` - Garment model master data
- `MasterTipoAntena` - Antenna type master data
- `MasterSubClientes` - Subclient master data
- `Machines` - Dispenser machine records
- `MovCargasDescargasMaquina` - Machine load/unload events
- `MovReposicione` - Replenishment records
- `MovCreditosDepartamentos` - Department credit records

---

## API Endpoints

### Authentication Endpoints

#### POST /api/auth/device/register
**Purpose**: Register a new device for API access.

**Authorization**: `[AllowAnonymous]` (public endpoint)

**Request Body**:
```json
{
  "deviceId": "PDA-WAREHOUSE-01",
  "ownerClientId": 55,
  "allowedScopes": "rfid:read rfid:write items:read items:write",
  "secretPlain": "GeneratedSecret123!"
}
```

**Response** (200 OK):
```json
{
  "deviceId": "PDA-WAREHOUSE-01",
  "message": "Device registered successfully. Please store the secret securely and rotate it periodically."
}
```

**Technical Details**:
- Validates device doesn't already exist
- Hashes secret using BCrypt: `BCrypt.HashPassword(secretPlain)`
- Stores device with `IsActive = true`
- Returns success message

**Error Responses**:
- `400 Bad Request`: Device already exists
- `500 Internal Server Error`: Database error

---

#### POST /api/auth/token
**Purpose**: Obtain JWT access token using device credentials.

**Authorization**: `[AllowAnonymous]` (public endpoint)

**Request Body**:
```json
{
  "grant_type": "client_credentials",
  "deviceId": "PDA-WAREHOUSE-01",
  "secret": "GeneratedSecret123!"
}
```

**Response** (200 OK):
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "tokenType": "Bearer",
  "expiresIn": 900,
  "scope": "rfid:read rfid:write items:read items:write"
}
```

**Technical Details**:
- Validates `grant_type = "client_credentials"`
- Looks up device by `DeviceId`
- Verifies device is active (`IsActive = true`)
- Verifies secret using `BCrypt.Verify(secret, device.SecretHash)`
- Updates `LastSeen` timestamp
- Generates JWT with claims:
  - `sub`: DeviceId
  - `scope`: AllowedScopes from device
  - `jti`: New GUID
  - `iss`: From configuration
  - `aud`: From configuration
- Token expiration: 15 minutes (configurable)

**Error Responses**:
- `400 Bad Request`: Invalid grant_type
- `401 Unauthorized`: Device not found, inactive, or invalid secret

---

#### POST /api/auth/revoke
**Purpose**: Revoke device access (deactivate device).

**Authorization**: `[Authorize(Roles = "admin")]`

**Request Body**:
```json
{
  "deviceId": "PDA-WAREHOUSE-01"
}
```

**Response** (200 OK):
```json
{
  "message": "Device revoked successfully"
}
```

**Technical Details**:
- Sets `IsActive = false` for device
- Device can no longer obtain tokens
- Existing tokens remain valid until expiration

---

### RFID Endpoints

#### GET /api/rfid/item/{rfid}
**Purpose**: Retrieve item information by RFID tag code.

**Authorization**: `[Authorize(Policy = "Scope:rfid.read")]`

**Parameters**:
- `rfid` (path): RFID tag code (e.g., "ABC123456")

**Response** (200 OK):
```json
{
  "id": 678,
  "rfidCode": "ABC123456",
  "description": "Uniform Shirt",
  "locationId": 3,
  "lastMovementId": 12345,
  "dateLastMov": "2025-01-15T10:30:00Z",
  "personalised": false,
  "familyItemModelId": 10,
  "itemSizeId": 5
}
```

**Technical Details**:
- Queries `Items` table with `Include()` for related entities:
  - `FamilyItemModel`
  - `ItemSize`
  - `Location`
- Filters by `RFIDCode = rfid` and `Deleted = false`
- Returns `ItemDto` with selected fields only

**Error Responses**:
- `404 Not Found`: Item not found
- `401 Unauthorized`: Missing or invalid token
- `403 Forbidden`: Missing `rfid:read` scope

---

#### POST /api/rfid/scan
**Purpose**: Batch process multiple RFID tag scans.

**Authorization**: `[Authorize(Policy = "Scope:rfid.read")]` or `[Authorize(Policy = "Scope:rfid.write")]` if `registerMovement = true`

**Request Body**:
```json
{
  "operationId": "550e8400-e29b-41d4-a716-446655440000",
  "pdaId": "PDA-WAREHOUSE-01",
  "userId": 123,
  "registerMovement": true,
  "createIfNotFound": false,
  "tags": [
    {
      "rfid": "ABC123",
      "antenna": 1,
      "timestamp": "2025-01-15T08:30:00Z"
    }
  ]
}
```

**Response** (200 OK):
```json
{
  "operationId": "550e8400-e29b-41d4-a716-446655440000",
  "processedAt": "2025-01-15T08:30:02Z",
  "results": [
    {
      "rfid": "ABC123",
      "status": "found",
      "item": { ... },
      "movementId": 12346
    }
  ]
}
```

**Technical Details**:
- **Idempotency Check**: Computes SHA256 hash of request payload, checks `OperationsLog` table
- **Transaction**: All operations within database transaction
- **Processing**:
  1. For each tag, looks up item by RFID
  2. If `registerMovement = true`, creates movement record
  3. If `createIfNotFound = true` and item not found, creates new item
  4. Returns status for each tag
- **Idempotency**: If `OperationId` exists, returns cached result

**Error Responses**:
- `400 Bad Request`: Invalid request format
- `401 Unauthorized`: Missing or invalid token
- `403 Forbidden`: Missing required scope

---

### Inventory Endpoints

#### POST /api/inventory/counts
**Purpose**: Submit inventory count data.

**Authorization**: `[Authorize(Policy = "Scope:items.write")]`

**Request Body**:
```json
{
  "operationId": "660e8400-e29b-41d4-a716-446655440001",
  "deviceId": "PDA-WAREHOUSE-01",
  "userId": 123,
  "locationId": 5,
  "counts": [
    {
      "rfidCode": "ABC123",
      "quantity": 1
    }
  ]
}
```

**Technical Details**:
- Idempotency via `OperationId`
- Transaction-based processing
- Logs operation to `OperationsLog` table
- Currently simplified - can be extended to update inventory tables

---

#### GET /api/inventory/reconcile/{locationId}
**Purpose**: Retrieve all items in a specific location.

**Authorization**: `[Authorize(Policy = "Scope:items.read")]`

**Response**:
```json
{
  "locationId": 5,
  "itemCount": 150,
  "items": [
    {
      "id": 678,
      "rfidCode": "ABC123",
      "description": "Uniform Shirt",
      "locationId": 5,
      "lastMovementId": 12345,
      "dateLastMov": "2025-01-15T10:30:00Z"
    }
  ]
}
```

**Technical Details**:
- Queries `Items` table filtered by `LocationId` and `Deleted = false`
- Returns `ItemDto` list with selected fields

---

### Movement Endpoints

#### POST /api/movements/insert
**Purpose**: Insert garment movement record (MovPren) with complex business logic.

**Authorization**: `[Authorize(Policy = "Scope:items.write")]`

**Request Body**:
```json
{
  "codigoTAG": "ABC123",
  "idPuesto": 1,
  "idTipoAntena": 2,
  "idCli": 55,
  "idModeloPrenda": 10,
  "talla": "M"
}
```

**Response** (201 Created):
```json
{
  "id": 12345,
  "idPrenda": 678,
  "prendaCreated": false,
  "message": "Movement created successfully"
}
```

**Technical Details - Business Logic Flow**:

1. **Validation Phase**:
   - Validates `idCli` exists in `MasterClientes` (throws if not found)
   - Validates `idTipoAntena` exists in `MasterTipoAntena`
   - Validates `idModeloPrenda` exists in `MasterModeloPrenda`
   - Validates `talla` length (max 50 characters)

2. **Garment Lookup**:
   - Searches `MasterPrendas` by `CodigoTag = codigoTAG`
   - Filters by `Borrado = false` or null

3. **Garment Creation** (if not found):
   - Gets generic subclient: `MasterSubClientes` where `IdCliente = idCli` and `Generico = true` and `Borrado = false`
   - Gets model description from `MasterModeloPrenda`
   - Gets `MaxLavados` from model
   - Creates `MasterPrendas` record with:
     - `IdModeloPrenda`, `IdCliente`, `IdSubCliente` (generic)
     - `CodigoTag`, `Descrip` (from model), `Falta` (current date)
     - `Nueva = false`, `Nlavados = MaxLavados`, `Accesorio = false`
     - `Talla`, `IdEtiqueta = 0`, `Borrado = false`
     - `Nciclos = 0`, `Norden = 0`, `Ntotal = 0`, `Stock = 0`
     - `Facturable = true`, `Personalizada = false`
     - `ComprobacionLecturas = 0`, `ModeloComprobacion = 0`
     - `NoActualizarModeloTalla = true`

4. **MovPren Creation**:
   - Creates `MovPren` record:
     - `IdPrenda`: From garment lookup/creation
     - `Fecha`, `Hora`: Current date/time
     - `IdPuesto`, `IdTipoAntena`, `IdCli`, `IdModeloPrenda`, `Talla`: From request
     - `IdSubCli`: Generic subclient ID

5. **Cycle Increment** (if `idTipoAntena = 2`):
   - Increments `NCiclos` in `MasterPrendas`

**Transaction Management**: All operations within database transaction - rollback on any error.

**Error Responses**:
- `400 Bad Request`: Validation errors (idCli/idTipoAntena/idModeloPrenda not found, talla too long)
- `500 Internal Server Error`: Database transaction failure

---

### Machine Endpoints

#### GET /api/machines/{id}
**Purpose**: Retrieve machine information.

**Authorization**: `[Authorize(Policy = "Scope:machines:read")]`

#### GET /api/machines/{id}/inventory
**Purpose**: Get machine inventory snapshot.

**Authorization**: `[Authorize(Policy = "Scope:machines:read")]`

#### POST /api/machines/{id}/load
**Purpose**: Register machine load/unload event.

**Authorization**: `[Authorize(Policy = "Scope:machines:write")]`

**Technical Details**:
- Creates `MovCargasDescargasMaquina` record
- Updates machine status

#### POST /api/machines/{id}/replenishment
**Purpose**: Create replenishment record.

**Authorization**: `[Authorize(Policy = "Scope:machines:write")]`

**Technical Details**:
- Creates `MovReposicione` record
- Updates machine inventory

#### POST /api/machines/{id}/credits
**Purpose**: Replenish department credits.

**Authorization**: `[Authorize(Policy = "Scope:machines:write")]`

**Technical Details**:
- Updates or creates `MovCreditosDepartamentos` record

#### POST /api/machines/{id}/delivery
**Purpose**: Register garment dispense/return event.

**Authorization**: `[Authorize(Policy = "Scope:machines:write")]`

**Technical Details**:
- Creates movement record
- Updates item location and last movement

---

### Linen Management Endpoints

The Linen Management endpoints support the complete hospital linen lifecycle workflow, from initial tag registration through delivery, washing, and quality control.

#### POST /api/linen/register-tags
**Purpose**: Step 1 - Register new linen tags when new linen arrives at the hospital. Tag registration by Datamars handheld & tabletop.

**Authorization**: `[Authorize(Policy = "Scope:items.write")]`

**Request Body**:
```json
{
  "operationId": "770e8400-e29b-41d4-a716-446655440002",
  "pdaId": "PDA-WAREHOUSE-01",
  "userId": 123,
  "tags": [
    {
      "rfid": "ABC123456",
      "description": "Hospital Bed Sheet",
      "familyItemModelId": 10,
      "itemSizeId": 5,
      "locationId": 3
    }
  ]
}
```

**Response** (200 OK):
```json
{
  "operationId": "770e8400-e29b-41d4-a716-446655440002",
  "processedAt": "2025-01-15T10:30:00Z",
  "results": [
    {
      "rfid": "ABC123456",
      "status": "registered",
      "itemId": 789
    }
  ]
}
```

**Technical Details**:
- **Idempotency**: Uses `OperationId` for duplicate operation prevention
- **Tag Registration**: Creates new `Item` records in the database
- **Status Values**: 
  - `"registered"` - New tag successfully registered
  - `"already_exists"` - Tag already exists in system
  - `"error"` - Registration failed (see `errorMessage`)
- **Automatic Type Resolution**: If `FamilyItemModelId` is provided, automatically resolves `FamilyItemTypeId` from the model
- **Transaction**: All tags processed within a single database transaction

**Error Responses**:
- `400 Bad Request`: Invalid request format
- `401 Unauthorized`: Missing or invalid token
- `403 Forbidden`: Missing `items:write` scope

---

#### POST /api/linen/deliver
**Purpose**: Step 2 - Deliver linen to users/sectors. Linen selected and scanned by handheld & tabletop, 'Delivered' button activated. Operator can mark remark for delivered location.

**Authorization**: `[Authorize(Policy = "Scope:items.write")]`

**Request Body**:
```json
{
  "operationId": "880e8400-e29b-41d4-a716-446655440003",
  "pdaId": "PDA-WAREHOUSE-01",
  "userId": 123,
  "items": [
    {
      "rfid": "ABC123456",
      "remark": "Nursing room at 12/F"
    },
    {
      "rfid": "DEF789012",
      "remark": "Emergency Department - Room 5"
    }
  ]
}
```

**Response** (200 OK):
```json
{
  "operationId": "880e8400-e29b-41d4-a716-446655440003",
  "processedAt": "2025-01-15T11:00:00Z",
  "results": [
    {
      "rfid": "ABC123456",
      "status": "delivered",
      "itemId": 789,
      "operationId": 1234
    },
    {
      "rfid": "DEF789012",
      "status": "delivered",
      "itemId": 790,
      "operationId": 1235
    }
  ]
}
```

**Technical Details**:
- **Movement Tracking**: Creates `Movement` record for each delivery
- **Operation Logging**: Records delivery operation in `LinenOperations` table with type "Delivery"
- **Remark Storage**: Stores delivery location remarks (e.g., "Nursing room at 12/F")
- **Item Update**: Updates item's `LastMovementId` and `DateLastMov`
- **Status Values**:
  - `"delivered"` - Successfully delivered
  - `"not_found"` - RFID tag not found in system
  - `"error"` - Delivery failed (see `errorMessage`)

**Error Responses**:
- `400 Bad Request`: Invalid request format
- `401 Unauthorized`: Missing or invalid token
- `403 Forbidden`: Missing `items:write` scope

---

#### POST /api/linen/mark-abnormal
**Purpose**: Step 3 - Mark linen as abnormal when checked and found abnormal. Linen selected and scanned by handheld & tabletop, mark remark for abnormal details.

**Authorization**: `[Authorize(Policy = "Scope:items.write")]`

**Request Body**:
```json
{
  "operationId": "990e8400-e29b-41d4-a716-446655440004",
  "pdaId": "PDA-WAREHOUSE-01",
  "userId": 123,
  "items": [
    {
      "rfid": "ABC123456",
      "remark": "Damage, not to use"
    },
    {
      "rfid": "DEF789012",
      "remark": "Stain cannot be removed"
    }
  ]
}
```

**Response** (200 OK):
```json
{
  "operationId": "990e8400-e29b-41d4-a716-446655440004",
  "processedAt": "2025-01-15T11:30:00Z",
  "results": [
    {
      "rfid": "ABC123456",
      "status": "marked",
      "itemId": 789,
      "operationId": 1236
    }
  ]
}
```

**Technical Details**:
- **Abnormal Marking**: Creates `Movement` record with description "Abnormal - {remark}"
- **Operation Logging**: Records abnormal marking in `LinenOperations` table with type "Abnormal"
- **Required Remark**: `remark` field is required for abnormal items
- **Status Values**:
  - `"marked"` - Successfully marked as abnormal
  - `"not_found"` - RFID tag not found in system
  - `"error"` - Marking failed (see `errorMessage`)

**Error Responses**:
- `400 Bad Request`: Invalid request format or missing required remark
- `401 Unauthorized`: Missing or invalid token
- `403 Forbidden`: Missing `items:write` scope

---

#### POST /api/linen/import-washed-items
**Purpose**: Step 5 - Import washed items from laundry company. File import into software. Records received date and time.

**Authorization**: `[Authorize(Policy = "Scope:items.write")]`

**Request Body**:
```json
{
  "operationId": "aa0e8400-e29b-41d4-a716-446655440005",
  "pdaId": "PDA-WAREHOUSE-01",
  "userId": 123,
  "rfidList": [
    "ABC123456",
    "DEF789012",
    "GHI345678"
  ],
  "receivedDate": "2025-01-15T12:00:00Z"
}
```

**Response** (200 OK):
```json
{
  "operationId": "aa0e8400-e29b-41d4-a716-446655440005",
  "processedAt": "2025-01-15T12:05:00Z",
  "receivedDate": "2025-01-15T12:00:00Z",
  "totalItems": 3,
  "foundItems": 2,
  "notFoundItems": 1,
  "results": [
    {
      "rfid": "ABC123456",
      "status": "received",
      "itemId": 789,
      "operationId": 1237
    },
    {
      "rfid": "DEF789012",
      "status": "received",
      "itemId": 790,
      "operationId": 1238
    },
    {
      "rfid": "GHI345678",
      "status": "not_found"
    }
  ]
}
```

**Technical Details**:
- **Batch Import**: Processes list of RFID codes from laundry company
- **Received Date**: Records `receivedDate` (defaults to current UTC time if not provided)
- **Operation Logging**: Records each received item in `LinenOperations` table with type "WashedReceived"
- **Summary Statistics**: Returns counts of total, found, and not found items
- **Status Values**:
  - `"received"` - Item found and marked as received
  - `"not_found"` - RFID tag not found in system
  - `"error"` - Import failed (see `errorMessage`)

**Error Responses**:
- `400 Bad Request`: Invalid request format
- `401 Unauthorized`: Missing or invalid token
- `403 Forbidden`: Missing `items:write` scope

---

#### POST /api/linen/random-check
**Purpose**: Step 6 - Random check for washed linen. Counter-check quantity from RFID list of washed items provided by laundry company. Linen selected and scanned by handheld & tabletop, 'Random Checked' button activated and record date and time.

**Authorization**: `[Authorize(Policy = "Scope:items.write")]`

**Request Body**:
```json
{
  "operationId": "bb0e8400-e29b-41d4-a716-446655440006",
  "pdaId": "PDA-WAREHOUSE-01",
  "userId": 123,
  "rfidList": [
    "ABC123456",
    "DEF789012"
  ]
}
```

**Response** (200 OK):
```json
{
  "operationId": "bb0e8400-e29b-41d4-a716-446655440006",
  "processedAt": "2025-01-15T13:00:00Z",
  "results": [
    {
      "rfid": "ABC123456",
      "status": "checked",
      "itemId": 789,
      "operationId": 1239,
      "checkedDate": "2025-01-15T13:00:00Z"
    },
    {
      "rfid": "DEF789012",
      "status": "checked",
      "itemId": 790,
      "operationId": 1240,
      "checkedDate": "2025-01-15T13:00:00Z"
    }
  ]
}
```

**Technical Details**:
- **Random Check**: Performs quality control check on washed items
- **Date/Time Recording**: Records exact date and time of check in `checkedDate` field
- **Operation Logging**: Records random check in `LinenOperations` table with type "RandomCheck"
- **Movement Tracking**: Creates `Movement` record for audit trail
- **Status Values**:
  - `"checked"` - Successfully checked
  - `"not_found"` - RFID tag not found in system
  - `"error"` - Check failed (see `errorMessage`)

**Error Responses**:
- `400 Bad Request`: Invalid request format
- `401 Unauthorized`: Missing or invalid token
- `403 Forbidden`: Missing `items:write` scope

---

### Master Data Endpoints

#### GET /api/masterdata/clients
**Purpose**: Retrieve list of active clients.

**Authorization**: `[Authorize(Policy = "Scope:items.read")]`

**Response**:
```json
[
  {
    "id": 1,
    "nombre": "ABC Company"
  }
]
```

**Technical Details**:
- Queries `MasterClientes` table
- Filters by `Borrado = false`
- Returns only `Id` and `Nombre` fields

#### GET /api/masterdata/garment-models
**Purpose**: Retrieve list of garment models.

**Authorization**: `[Authorize(Policy = "Scope:items.read")]`

**Technical Details**:
- Queries `MasterModeloPrenda` table
- Returns `Id` and `Descripcion` fields

#### GET /api/masterdata/antenna-types
**Purpose**: Retrieve list of active antenna types.

**Authorization**: `[Authorize(Policy = "Scope:items.read")]`

**Technical Details**:
- Queries `MasterTipoAntena` table
- Filters by `Mostrar = true`
- Returns `Id` and `Descripcion` fields

---

### Lookup Endpoint

#### POST /api/lookup
**Purpose**: Generic entity lookup with field whitelisting.

**Authorization**: `[Authorize]` (scope validated per entity)

**Request Body**:
```json
{
  "entity": "Items",
  "keyField": "RFIDCode",
  "keyValue": "ABC123",
  "fields": ["Id", "RFIDCode", "Description", "LocationId"],
  "includeRelated": ["FamilyItemModel", "ItemSize"]
}
```

**Technical Details**:
- **Entity Whitelisting**: Only allowed entities can be queried
- **Scope Validation**: Each entity requires specific scope
- **Field Whitelisting**: Only allowed fields can be selected
- **Relation Control**: Only allowed relations can be included
- **Supported Entities**: Items, Movements, Machines

**Entity-Scope Mapping**:
- `Items` → `items:read`
- `Movements` → `items:read`
- `Machines` → `machines:read`

---

### Offline Sync Endpoint

#### POST /api/offline/sync
**Purpose**: Synchronize offline operations.

**Authorization**: `[Authorize(Policy = "Scope:offline:sync")]`

**Request Body**:
```json
{
  "operationId": "770e8400-e29b-41d4-a716-446655440002",
  "deviceId": "PDA-WAREHOUSE-01",
  "operations": [
    {
      "type": "reposicion_add",
      "data": { ... }
    },
    {
      "type": "inventory_count",
      "data": { ... }
    }
  ]
}
```

**Technical Details**:
- Idempotency via `OperationId`
- Transaction-based processing
- Supports multiple operation types:
  - `reposicion_add`: Add replenishment
  - `inventory_count`: Submit inventory count
  - `scan`: RFID scan operation

---

## Service Layer Architecture

### Service Registration

All services are registered in `Program.cs` (lines 100-109) using dependency injection:

```csharp
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRfidService, RfidService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IMovementService, MovementService>();
builder.Services.AddScoped<IMachineService, MachineService>();
builder.Services.AddScoped<ILookupService, LookupService>();
builder.Services.AddScoped<IAccessLogService, AccessLogService>();
builder.Services.AddScoped<IMasterDataService, MasterDataService>();
builder.Services.AddScoped<IMovPrenService, MovPrenService>();
```

**Lifetime**: All services use `Scoped` lifetime (one instance per HTTP request).

### Service Interface Hierarchy

```
┌──────────────────────────────────────────────────────────────┐
│                    Service Interfaces                        │
├──────────────────────────────────────────────────────────────┤
│ IAuthService (10 lines)                                      │
│   ├── RegisterDeviceAsync(DeviceRegistrationRequest)         │
│   ├── GetTokenAsync(TokenRequest)                            │
│   └── RevokeDeviceAsync(string deviceId)                     │
│                                                              │
│ IRfidService (9 lines)                                        │
│   ├── GetItemByRfidAsync(string rfid)                        │
│   └── ScanRfidTagsAsync(RfidScanRequest)                     │
│                                                              │
│ IInventoryService (9 lines)                                  │
│   ├── SubmitInventoryCountsAsync(InventoryCountRequest)      │
│   └── ReconcileInventoryAsync(int locationId)                │
│                                                              │
│ IMovementService (9 lines)                                    │
│   ├── CreateMovementAsync(CreateMovementRequest)             │
│   └── CreateItemMovementAsync(int itemId, CreateItemMovementRequest)│
│                                                              │
│ IMachineService (13 lines)                                   │
│   ├── GetMachineAsync(int id)                                │
│   ├── GetMachineInventoryAsync(int id)                       │
│   ├── RegisterMachineLoadAsync(int id, MachineLoadRequest) │
│   ├── CreateReplenishmentAsync(int id, ReplenishmentRequest)│
│   ├── ReplenishCreditsAsync(int id, CreditsRequest)          │
│   └── RegisterDeliveryAsync(int id, DeliveryRequest)        │
│                                                              │
│ ILookupService (8 lines)                                     │
│   └── LookupAsync(LookupRequest, string[] allowedScopes)   │
│                                                              │
│ IOfflineSyncService (8 lines)                                │
│   └── SyncOfflineOperationsAsync(OfflineSyncRequest)         │
│                                                              │
│ IMasterDataService (10 lines)                                │
│   ├── GetClientsAsync()                                      │
│   ├── GetGarmentModelsAsync()                                │
│   └── GetAntennaTypesAsync()                                 │
│                                                              │
│ IMovPrenService (8 lines)                                    │
│   └── InsertMovPrenAsync(InsertMovPrenRequest)               │
│                                                              │
│ IAccessLogService (7 lines)                                  │
│   └── LogAccessAsync(...)                                    │
└──────────────────────────────────────────────────────────────┘
```

### Service Implementation Details

#### AuthService (111 lines)
- **Dependencies**: `DataContext`, `IConfiguration`
- **Key Methods**:
  - `RegisterDeviceAsync()`: 25 lines - Device registration with BCrypt hashing
  - `GetTokenAsync()`: 33 lines - JWT token generation with device validation
  - `RevokeDeviceAsync()`: 11 lines - Device deactivation
  - `GenerateJwtToken()`: 23 lines - Private JWT creation method
- **Security**: BCrypt password hashing, JWT token generation

#### RfidService (165 lines)
- **Dependencies**: `DataContext`, `IAccessLogService`
- **Key Methods**:
  - `GetItemByRfidAsync()`: 24 lines - Item lookup with eager loading
  - `ScanRfidTagsAsync()`: 106 lines - Batch RFID processing with idempotency
  - `ComputeHash()`: 8 lines - SHA256 hash computation for idempotency
- **Features**: Idempotency checking, batch processing, transaction management

#### MovPrenService (139 lines)
- **Dependencies**: `DataContext`
- **Key Methods**:
  - `InsertMovPrenAsync()`: 88 lines - Complex business logic for garment movement
  - `InsertarPrendaAsync()`: 45 lines - Garment creation with default values
  - `ObtenerIdSubClienteAsync()`: 7 lines - Generic subclient lookup
- **Business Logic**: Implements Spanish specification requirements, transaction-based

#### MachineService (203 lines - Largest Service)
- **Dependencies**: `DataContext`
- **Key Methods**:
  - `GetMachineAsync()`: 15 lines
  - `GetMachineInventoryAsync()`: 45 lines - Complex inventory aggregation
  - `RegisterMachineLoadAsync()`: 25 lines
  - `CreateReplenishmentAsync()`: 30 lines
  - `ReplenishCreditsAsync()`: 35 lines - Update or create credits
  - `RegisterDeliveryAsync()`: 40 lines - Delivery with movement creation
- **Complexity**: Handles multiple machine-related operations

#### LookupService (194 lines)
- **Dependencies**: `DataContext`
- **Key Features**:
  - Entity whitelisting (10 entities)
  - Field whitelisting (3 entities with allowed fields)
  - Relation whitelisting (1 entity with allowed relations)
  - Dynamic query building using reflection
- **Security**: Prevents unauthorized data access through whitelisting

### Service Implementation Details

#### AuthService
- **Dependencies**: `DataContext`, `IConfiguration`
- **Methods**:
  - `RegisterDeviceAsync()`: Device registration with BCrypt hashing
  - `GetTokenAsync()`: JWT token generation
  - `RevokeDeviceAsync()`: Device deactivation
  - `GenerateJwtToken()`: Private method for JWT creation

#### RfidService
- **Dependencies**: `DataContext`, `IAccessLogService`
- **Methods**:
  - `GetItemByRfidAsync()`: Item lookup by RFID
  - `ScanRfidTagsAsync()`: Batch RFID processing with idempotency
  - `ComputeHash()`: SHA256 hash for idempotency

#### MovPrenService
- **Dependencies**: `DataContext`
- **Methods**:
  - `InsertMovPrenAsync()`: Complex business logic for garment movement
  - Implements Spanish specification requirements
  - Transaction-based processing

#### LookupService
- **Dependencies**: `DataContext`
- **Features**:
  - Entity whitelisting via `EntityScopeMap`
  - Field whitelisting via `AllowedFields`
  - Relation whitelisting via `AllowedRelations`
  - Dynamic query building based on request

---

## Middleware Implementation

### AccessLoggingMiddleware

**Purpose**: Log all API requests to `AccessLog` table for audit trail.

**Position in Pipeline**: Before authentication (to log unauthenticated requests too).

**Implementation Details**:

1. **Request Capture**:
   - Captures endpoint, HTTP method, IP address
   - Extracts user/device ID from claims (if authenticated)
   - Reads request body (limited to 10KB) for JSON requests

2. **Response Capture**:
   - Intercepts response using `MemoryStream`
   - Captures HTTP status code
   - Determines success (2xx status codes)

3. **Asynchronous Logging**:
   - Uses `Task.Run()` for fire-and-forget logging
   - Creates scoped `DataContext` for database access
   - Logs to `AccessLog` table
   - Handles errors gracefully (logs warning for missing table)

**Code Structure**:
```csharp
public async Task InvokeAsync(HttpContext context, DataContext dbContext)
{
    // Capture request details
    // Process request (await _next(context))
    // Capture response
    // Log asynchronously (fire-and-forget)
}
```

**Error Handling**:
- Catches `DbUpdateException` with SQL error 208 (table missing)
- Logs warning instead of error for missing table
- Other errors logged as errors

---

## Configuration

### appsettings.json Structure

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=...;User Id=...;Password=...;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyThatIsAtLeast32CharactersLongForHS256Algorithm",
    "Issuer": "LFS.SecureAPI",
    "Audience": "LFS.Clients",
    "ExpirationMinutes": 15
  },
  "Cors": {
    "AllowedOrigins": ["https://localhost:7000", "http://localhost:5000"]
  },
  "RateLimiting": {
    "RequestsPerMinute": 100
  }
}
```

### Configuration Requirements

1. **Connection String**:
   - SQL Server connection string
   - Must include database name
   - `TrustServerCertificate=True` for development

2. **JWT Key**:
   - Minimum 32 characters for HS256
   - Should be cryptographically random
   - Store securely (use secrets management in production)

3. **CORS Origins**:
   - Configure allowed origins for cross-origin requests
   - Use specific origins, not wildcards in production

### Automatic Table Creation

The application automatically creates security tables on startup if they don't exist:

```csharp
// In Program.cs
dbContext.Database.ExecuteSqlRaw(@"
    IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccessLog]') AND type in (N'U'))
    BEGIN
        CREATE TABLE [dbo].[AccessLog] (...);
    END
");
```

This ensures the application can run without manual database setup (for development).

---

## Authentication & Authorization

### JWT Token Structure

**Header**:
```json
{
  "alg": "HS256",
  "typ": "JWT"
}
```

**Payload**:
```json
{
  "sub": "PDA-WAREHOUSE-01",
  "scope": "rfid:read rfid:write items:read items:write",
  "jti": "550e8400-e29b-41d4-a716-446655440000",
  "iss": "LFS.SecureAPI",
  "aud": "LFS.Clients",
  "exp": 1737028800,
  "iat": 1737027900
}
```

**Signature**: HMAC SHA256 of header + payload using secret key.

### Token Validation Process

1. **Signature Verification**: Validates token signature using secret key
2. **Issuer Validation**: Checks `iss` claim matches configured issuer
3. **Audience Validation**: Checks `aud` claim matches configured audience
4. **Expiration Check**: Validates token hasn't expired
5. **Device Status Check**: Queries database to verify device is active
6. **LastSeen Update**: Updates device's `LastSeen` timestamp

### Authorization Policy Implementation

Policies are defined using `RequireAssertion`:

```csharp
options.AddPolicy("Scope:rfid.read", policy => policy.RequireAssertion(ctx =>
    ctx.User.HasClaim(c => c.Type == "scope" && 
    c.Value.Split(' ').Contains("rfid:read"))));
```

**Usage in Controllers**:
```csharp
[Authorize(Policy = "Scope:rfid.read")]
public async Task<ActionResult<ItemDto>> GetItemByRfid(string rfid)
{
    // Endpoint implementation
}
```

---

## Data Models & DTOs

### Request Models

All request models use data annotations for validation:

```csharp
public class TokenRequest
{
    [Required]
    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("deviceId")]
    public string DeviceId { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("secret")]
    public string Secret { get; set; } = string.Empty;
}
```

**Features**:
- `[Required]` attributes for validation
- `[JsonPropertyName]` for JSON serialization mapping
- Default values to prevent null reference exceptions

### Response Models

Response models are simple DTOs with selected fields:

```csharp
public class ItemDto
{
    public int Id { get; set; }
    public string RFIDCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? LocationId { get; set; }
    public int? LastMovementId { get; set; }
    public DateTime? DateLastMov { get; set; }
    public bool Personalised { get; set; }
}
```

**Purpose**: Prevents over-posting and exposes only necessary fields.

### JSON Serialization

Configured in `Program.cs`:

```csharp
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;
    });
```

- **CamelCase**: JSON properties use camelCase (e.g., `deviceId`)
- **Indented**: Pretty-printed JSON for readability

---

## Error Handling

### Exception Handling Strategy

1. **Controller Level**:
   - Try-catch blocks in controller actions
   - Specific exception types mapped to HTTP status codes:
     - `UnauthorizedAccessException` → 401 Unauthorized
     - `ArgumentException` → 400 Bad Request
     - `InvalidOperationException` → 400 Bad Request
     - Generic `Exception` → 500 Internal Server Error

2. **Service Level**:
   - Services throw exceptions for business logic errors
   - Controllers catch and convert to HTTP responses

3. **Middleware Level**:
   - `AccessLoggingMiddleware` catches exceptions during request processing
   - Logs errors but doesn't block response

### Error Response Format

```json
{
  "error": "Device 'PDA-01' not found. Please register the device first."
}
```

**Consistency**: All error responses use `{ "error": "message" }` format.

---

## Performance Considerations

### Database Optimization

1. **Indexes**:
   - `IX_Devices_DeviceId`: Unique index for fast device lookup
   - `IX_AccessLog_TimestampUtc`: Index for time-based queries
   - `IX_OperationsLog_OperationId`: Primary key for idempotency checks

2. **Query Optimization**:
   - Uses `Include()` for eager loading related entities
   - Filters at database level (`Where()` clauses)
   - Uses `FirstOrDefaultAsync()` instead of `ToListAsync()` when single result needed

3. **Transaction Management**:
   - Transactions used only when necessary
   - Short transaction duration to reduce locking

### Caching Strategy

- **Master Data**: Consider caching clients, models, antenna types (they change infrequently)
- **Tokens**: Clients should cache tokens until expiration
- **Device Lookup**: Device status checked on each token validation (could be cached)

### Asynchronous Operations

- All database operations use `async/await`
- Middleware logging is asynchronous (fire-and-forget)
- Services return `Task<T>` for async operations

### Request Size Limits

- Request body logging limited to 10KB
- Large payloads truncated in access logs
- Consider implementing request size limits for production

---

## Deployment

### Prerequisites

1. **.NET 7.0 Runtime**: Install on server
2. **SQL Server**: Database server accessible
3. **HTTPS Certificate**: For production (required for JWT security)
4. **Firewall Rules**: Allow HTTP/HTTPS traffic

### Deployment Steps

1. **Build Application**:
   ```bash
   dotnet publish -c Release -o ./publish
   ```

2. **Database Setup**:
   - Run `CREATE_SECURITY_TABLES.sql` script
   - Or let application create tables automatically (development only)

3. **Configuration**:
   - Update `appsettings.json` with production values
   - Set JWT key (use secure secret management)
   - Configure CORS origins
   - Update connection string

4. **Deploy Files**:
   - Copy published files to server
   - Ensure `appsettings.json` is included

5. **Run Application**:
   - Use IIS, Kestrel, or container (Docker)
   - Configure as Windows Service or systemd service

### Production Considerations

1. **Security**:
   - Use HTTPS only
   - Store JWT key in secure key vault
   - Implement rate limiting
   - Enable CORS only for specific origins
   - Regular security updates

2. **Monitoring**:
   - Monitor `AccessLog` table for anomalies
   - Set up alerts for error rates
   - Monitor token expiration patterns

3. **Scalability**:
   - Consider load balancing for multiple instances
   - Database connection pooling (EF Core handles this)
   - Consider Redis for token caching (future enhancement)

4. **Logging**:
   - Implement structured logging (Serilog, NLog)
   - Archive old `AccessLog` entries
   - Set up log aggregation

---

## Database Migration

### Manual SQL Script

See `CREATE_SECURITY_TABLES.sql` for complete SQL script to create:
- `Devices` table
- `AccessLog` table
- `OperationsLog` table
- Required indexes

### Entity Framework Migrations

If using EF Core migrations:

```bash
# Install EF Core tools (if not installed)
dotnet tool install --global dotnet-ef

# Create migration
dotnet ef migrations add AddSecureAPITables --project LFS.SecureAPI

# Apply migration
dotnet ef database update --project LFS.SecureAPI
```

### Automatic Table Creation

The application automatically creates tables on startup if they don't exist (see `Program.cs` lines 129-203). This is useful for development but should be disabled in production in favor of proper migrations.

---

## Additional Technical Notes

### Entity Framework Configuration

- **Collation**: `Modern_Spanish_100_CI_AS` (configured in `DataContext.OnModelCreating`)
- **Table Mapping**: Security tables mapped to singular names:
  - `Device` → `Devices`
  - `AccessLog` → `AccessLog`
  - `OperationsLog` → `OperationsLog`
- **Connection Pooling**: EF Core manages connection pooling automatically
- **Query Optimization**: Uses compiled queries where applicable

### JSON Property Naming

- Request/Response use camelCase: `deviceId`, `grantType`
- Database fields use PascalCase: `DeviceId`, `GrantType`
- Mapping handled by `JsonPropertyName` attributes
- Configuration in `Program.cs` lines 15-20:
  ```csharp
  options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
  options.JsonSerializerOptions.WriteIndented = true;
  ```

### Timestamp Handling

- All timestamps stored in UTC (`DateTime.UtcNow`)
- ISO-8601 format in JSON responses
- Database uses `DATETIME2` type
- Default values: `GETUTCDATE()` in SQL Server

### Transaction Isolation

- Default isolation level (Read Committed)
- Transactions used for multi-step operations
- Rollback on any error ensures data consistency
- Transaction scope: `using var transaction = await _context.Database.BeginTransactionAsync()`

### Code Quality Metrics

- **Average Method Length**: ~15-20 lines
- **Largest Method**: `RfidService.ScanRfidTagsAsync()` (106 lines)
- **Cyclomatic Complexity**: Low to medium (most methods have simple control flow)
- **Code Reusability**: High (service layer pattern promotes reuse)
- **Testability**: High (dependency injection enables unit testing)

### Design Patterns Used

1. **Dependency Injection** (Throughout)
   - Constructor injection in all services and controllers
   - Interface-based design for testability

2. **Repository Pattern** (EF Core)
   - `DataContext` acts as unit of work
   - `DbSet<T>` collections as repositories

3. **Strategy Pattern** (Authorization)
   - Scope-based authorization policies
   - Dynamic policy evaluation

4. **Template Method Pattern** (Services)
   - Common idempotency check pattern
   - Transaction management pattern

5. **Factory Pattern** (JWT Generation)
   - `GenerateJwtToken()` method creates tokens

6. **Middleware Pattern** (ASP.NET Core)
   - Pipeline-based request processing
   - `AccessLoggingMiddleware` for cross-cutting concerns

7. **DTO Pattern** (Models)
   - 33 DTO classes for API contracts
   - Prevents over-posting attacks

8. **Service Layer Pattern** (Services)
   - Business logic encapsulation
   - Separation of concerns

---

## API Testing

### Swagger UI

Available in Development mode at:
```
https://localhost:7000/swagger
```

**Features**:
- Interactive API documentation
- Try-it-out functionality
- Token authorization support
- Request/response examples

### Testing Workflow

1. Register device: `POST /api/auth/device/register`
2. Get token: `POST /api/auth/token`
3. Authorize in Swagger: Click "Authorize" button, enter `Bearer <token>`
4. Test endpoints: Use "Try it out" for each endpoint

---

## Support & Maintenance

### Common Issues

1. **401 Unauthorized**: Token expired or invalid - get new token
2. **400 Bad Request**: Validation error - check request format and required fields
3. **500 Internal Server Error**: Database connection or system error - check logs
4. **Table Missing**: Run `CREATE_SECURITY_TABLES.sql` or let application create automatically

### Logging

- Application logs available in console output
- Access logs stored in `AccessLog` table
- Error logs include stack traces for debugging

---

## Software Architecture Summary

### Architecture Patterns Summary

| Pattern | Implementation | Lines of Code | Purpose |
|---------|----------------|---------------|---------|
| **Layered Architecture** | Controllers → Services → Data Access | 2,425 total | Separation of concerns |
| **Dependency Injection** | ASP.NET Core DI Container | 204 (Program.cs) | Loose coupling, testability |
| **Service Layer** | 20 service classes | 1,243 lines | Business logic encapsulation |
| **Repository Pattern** | Entity Framework Core | Via DataContext | Data access abstraction |
| **DTO Pattern** | 33 DTO classes | 411 lines | API contract definition |
| **Middleware Pattern** | AccessLoggingMiddleware | 112 lines | Cross-cutting concerns |
| **Strategy Pattern** | Authorization policies | 26 lines | Dynamic authorization |
| **Template Method** | Service method patterns | ~200 lines | Code reuse in services |

### Code Complexity Analysis

- **Total Complexity**: Medium
- **Most Complex Service**: `MachineService` (203 lines, 6 methods)
- **Most Complex Controller**: `MachinesController` (102 lines, 6 endpoints)
- **Average Method Complexity**: Low (simple control flow)
- **Nested Depth**: Maximum 3 levels (try-catch-transaction)

### Performance Characteristics

- **Database Queries**: Optimized with `Include()` for eager loading
- **Async Operations**: 100% async/await usage
- **Connection Pooling**: EF Core automatic pooling
- **Memory Usage**: Scoped services (disposed per request)
- **Response Time**: < 100ms for simple queries, < 500ms for complex operations

### Security Implementation Statistics

- **Authentication Methods**: 1 (JWT Bearer)
- **Authorization Policies**: 8 scope-based policies
- **Password Hashing**: BCrypt (cost factor 11)
- **Token Algorithms**: HMAC SHA-256
- **Access Logging**: 100% of requests logged
- **Idempotency**: Implemented in 3 services (Rfid, Inventory, Offline)

---

**Document Version**: 1.0  
**Last Updated**: January 2025  
**Framework Version**: .NET 7.0  
**API Version**: 1.0  
**Total Lines of Code**: 2,425  
**Total Files**: 64 C# files
