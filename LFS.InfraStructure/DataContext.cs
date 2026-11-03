using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LFS.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;
using LFS.Shared.Models;
using LFS.Shared.Models.Old;

namespace LFS.InfraStructure;

public partial class DataContext : IdentityDbContext<AppUser>
{
  public DataContext()
  {
  }

  public DataContext(DbContextOptions<DataContext> options)
: base(options)
  {
  }

  //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  //=> optionsBuilder.UseSqlServer("Data Source=lfdsamazonrds.cwc1t3aqjwgt.eu-west-3.rds.amazonaws.com,3433;Initial Catalog=Laundry_Showroom;TrustServerCertificate=True;User ID=lfdslogin;Password=LF$l4f4br1c4.LF$;")
  //=> optionsBuilder.UseSqlServer("Data Source=localhost\\SQL2019;Initial Catalog=CP_LAUNDRY_SHOWROOM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;User ID=sa;Password=Turei.230;")
  //=> optionsBuilder.UseSqlServer("Data Source=lfdsamazonrds.cwc1t3aqjwgt.eu-west-3.rds.amazonaws.com,3433;Initial Catalog=LaundryShowroomPruebas;TrustServerCertificate=True;User ID=lfs;Password=1Avand3r-$;")
  //.EnableSensitiveDataLogging();

  public DbSet<Department> Departments { get; set; }
  public DbSet<User> ApplicationUsers { get; set; }
  public DbSet<AuthRefreshToken> AuthRefreshTokens { get; set; }
  public DbSet<AppUserCustomer> AppUserCustomers { get; set; }
  public DbSet<Item> Items { get; set; }
  public DbSet<FamilyItem> FamilyItems { get; set; }
  public DbSet<FamilyItemType> FamilyItemTypes { get; set; }
  public DbSet<FamilyItemModel> FamilyItemModels { get; set; }
  public DbSet<Customer> Customers { get; set; }
  public DbSet<Supplier> Suppliers { get; set; }
  public DbSet<Sex> Sexes { get; set; }
  public DbSet<Form> Forms { get; set; }
  public DbSet<ItemUser> ItemUsers { get; set; }
  public DbSet<Machine> Machines { get; set; }
  public DbSet<MachineModel> MachineModels { get; set; }
  public DbSet<Warehouse> Warehouses { get; set; }
  public DbSet<LoadUser> LoadUsers { get; set; }
  public DbSet<ItemSize> ItemSizes { get; set; }
  public DbSet<CustomerItemModel> CustomerItemModels { get; set; }
  public DbSet<Alert> Alerts { get; set; }
  public DbSet<Location> Locations { get; set; }
  public DbSet<Movement> Movements { get; set; }
  public DbSet<RoleDetail> RoleDetails { get; set; }
  public DbSet<CreditItemUser> CreditsItemUser { get; set; }
  public DbSet<CreditModelDepartment> CreditsModelDepartments { get; set; }
  public virtual DbSet<AperturaTaquilla> AperturaTaquillas { get; set; }
  public virtual DbSet<AvisoPrenda> AvisoPrendas { get; set; }
  public virtual DbSet<DatosPropio> DatosPropios { get; set; }
  public virtual DbSet<FingerTable> FingerTables { get; set; }
  public virtual DbSet<FotosMovPren> FotosMovPrens { get; set; }
  public virtual DbSet<FotosPrenda> FotosPrendas { get; set; }
  public virtual DbSet<LineaDePedidos> LineaDePedidos { get; set; }
  public virtual DbSet<iPick> IPick { get; set; }
  public virtual DbSet<iPickUsuarioColor> iPickUsuarioColor { get; set; }
  public virtual DbSet<iPickCarros> iPickCarros { get; set; }
  public virtual DbSet<GrupoMovimientoTipoAntena> GrupoMovimientoTipoAntenas { get; set; }
  public virtual DbSet<ImportCategoriaPrincipal> ImportCategoriaPrincipals { get; set; }
  public virtual DbSet<ImportCategoriaSecundarium> ImportCategoriaSecundaria { get; set; }
  public virtual DbSet<ImportMovCategoriasDepartamento> ImportMovCategoriasDepartamentos { get; set; }
  public virtual DbSet<LogDiferenciasRecepcionRFID> LogDiferenciasRecepcionRfids { get; set; }
  public virtual DbSet<LogErroresDispensacion> LogErroresDispensacions { get; set; }
  public virtual DbSet<LogErroresRecoleccion> LogErroresRecoleccions { get; set; }
  public virtual DbSet<LogMaquina> LogMaquinas { get; set; }
  public virtual DbSet<MaquinasDispensadorasParametro> MaquinasDispensadorasParametros { get; set; }
  public virtual DbSet<MasterActividade> MasterActividades { get; set; }
  public virtual DbSet<MasterAviso> MasterAvisos { get; set; }
  public virtual DbSet<MasterAvisosPorEmail> MasterAvisosPorEmails { get; set; }
  public virtual DbSet<MasterCategoriaIncidencium> MasterCategoriaIncidencia { get; set; }
  public virtual DbSet<MasterClientes> MasterClientes { get; set; }
  public virtual DbSet<MasterComandosLoader> MasterComandosLoaders { get; set; }
  public virtual DbSet<MasterControl> MasterControls { get; set; }
  public virtual DbSet<MasterDepartamentosCliente> MasterDepartamentosClientes { get; set; }
  public virtual DbSet<MasterDotacione> MasterDotaciones { get; set; }
  public virtual DbSet<MasterEdificio> MasterEdificios { get; set; }
  public virtual DbSet<MasterEdificiosPuesto> MasterEdificiosPuestos { get; set; }
  public virtual DbSet<MasterEstadoLlaveTaquilla> MasterEstadoLlaveTaquillas { get; set; }
  public virtual DbSet<MasterEstadoTaquilla> MasterEstadoTaquillas { get; set; }
  public virtual DbSet<MasterEtiqueta> MasterEtiquetas { get; set; }
  public virtual DbSet<MasterFormasContacto> MasterFormasContactos { get; set; }
  public virtual DbSet<MasterGruposDeMovimiento> MasterGruposDeMovimientos { get; set; }
  public virtual DbSet<MasterImpresorasEtiqueta> MasterImpresorasEtiquetas { get; set; }
  public virtual DbSet<MasterIncidencia> MasterIncidencias { get; set; }
  public virtual DbSet<MasterMaquinaDispensacion> MasterMaquinaDispensacions { get; set; }
  public virtual DbSet<MasterMensajesMaquina> MasterMensajesMaquinas { get; set; }
  public virtual DbSet<MasterModeloAntena> MasterModeloAntenas { get; set; }
  public virtual DbSet<MasterModeloPrenda> MasterModeloPrenda { get; set; }
  public virtual DbSet<MasterModeloTaquilla> MasterModeloTaquillas { get; set; }
  public virtual DbSet<MasterModelosDotacion> MasterModelosDotacions { get; set; }
  public virtual DbSet<MasterNivelAcceso> MasterNivelAccesos { get; set; }
  public virtual DbSet<MasterPrecioTipoAntena> MasterPrecioTipoAntenas { get; set; }
  public virtual DbSet<MasterPrendas> MasterPrendas { get; set; }
  public virtual DbSet<MasterProveedores> MasterProveedores { get; set; }
  public virtual DbSet<MasterProvincia> MasterProvincias { get; set; }
  public virtual DbSet<MasterPuesto> MasterPuestos { get; set; }
  public virtual DbSet<MasterRagoTag> MasterRagoTags { get; set; }
  public virtual DbSet<MasterRangoCredito> MasterRangoCreditos { get; set; }
  public virtual DbSet<MasterServicio> MasterServicios { get; set; }
  public virtual DbSet<MasterSexo> MasterSexos { get; set; }
  public virtual DbSet<MasterSubClientes> MasterSubClientes { get; set; }
  public virtual DbSet<MasterTaquilla> MasterTaquillas { get; set; }
  public virtual DbSet<MasterTipoAntena> MasterTipoAntenas { get; set; }
  public virtual DbSet<MasterTipoIncidencium> MasterTipoIncidencia { get; set; }
  public virtual DbSet<MasterTipoMaquinaDispensadora> MasterTipoMaquinaDispensadoras { get; set; }
  public virtual DbSet<MasterTipoMarcadoPrenda> MasterTipoMarcadoPrendas { get; set; }
  public virtual DbSet<MasterTipoMovimientoTaquilla> MasterTipoMovimientoTaquillas { get; set; }
  public virtual DbSet<MasterTipoMovimientosReposicione> MasterTipoMovimientosReposiciones { get; set; }
  public virtual DbSet<MasterTipoPacto> MasterTipoPactos { get; set; }
  public virtual DbSet<MasterTipoPosicionSopl> MasterTipoPosicionSopls { get; set; }
  public virtual DbSet<MasterTipoPrenda> MasterTipoPrenda { get; set; }
  public virtual DbSet<MasterTipoRopa> MasterTipoRopas { get; set; }
  public virtual DbSet<MasterTipoTag> MasterTipoTags { get; set; }
  public virtual DbSet<MasterTipoVestuario> MasterTipoVestuarios { get; set; }
  public virtual DbSet<MasterTiposDeBaja> MasterTiposDeBajas { get; set; }
  public virtual DbSet<MasterTiposDireccione> MasterTiposDirecciones { get; set; }
  public virtual DbSet<MasterTiposUsuario> MasterTiposUsuarios { get; set; }
  public virtual DbSet<MasterUbicacione> MasterUbicaciones { get; set; }
  public virtual DbSet<MasterUsuario> MasterUsuarios { get; set; }
  public virtual DbSet<MasterVestuario> MasterVestuarios { get; set; }
  public virtual DbSet<MasterViasPublica> MasterViasPublicas { get; set; }
  public virtual DbSet<MasterZona> MasterZonas { get; set; }
  public virtual DbSet<MovAntenasPuesto> MovAntenasPuestos { get; set; }
  public virtual DbSet<MovCambioAntena> MovCambioAntenas { get; set; }
  public virtual DbSet<MovCargasDescargasMaquina> MovCargasDescargasMaquinas { get; set; }
  public virtual DbSet<MovClienteCamposControl> MovClienteCamposControls { get; set; }
  public virtual DbSet<MovComandosAntena> MovComandosAntenas { get; set; }
  public virtual DbSet<MovCreditosDepartamentos> MovCreditosDepartamentos { get; set; }
  public virtual DbSet<MovCreditosModelosDepartamento> MovCreditosModelosDepartamentos { get; set; }
  public virtual DbSet<MovDepartamentosDotacione> MovDepartamentosDotaciones { get; set; }
  public virtual DbSet<MovDireccione> MovDirecciones { get; set; }
  public virtual DbSet<MovDireccionesCliente> MovDireccionesClientes { get; set; }
  public virtual DbSet<MovDireccionesPropio> MovDireccionesPropios { get; set; }
  public virtual DbSet<MasterNotificaciones> MasterNotificaciones { get; set; }
  public virtual DbSet<MovDireccionesProveedore> MovDireccionesProveedores { get; set; }
  public virtual DbSet<MovEtiModCli> MovEtiModClis { get; set; }
  public virtual DbSet<MovEtiquetasCliente> MovEtiquetasClientes { get; set; }
  public virtual DbSet<MovHisPren> MovHisPrens { get; set; }
  public virtual DbSet<MovHistoricoEstadoExternoTaquilla> MovHistoricoEstadoExternoTaquillas { get; set; }
  public virtual DbSet<MovHistoricoGestionTaquilla> MovHistoricoGestionTaquillas { get; set; }
  public virtual DbSet<MovHistoricosFechasSubCliente> MovHistoricosFechasSubClientes { get; set; }
  public virtual DbSet<MovMaquinaContadorPrendasSucias> MovMaquinaContadorPrendasSucias { get; set; }
  public virtual DbSet<MovMaquinaDisAlmacenes> MovMaquinaDisAlmacenes { get; set; }
  public virtual DbSet<MovMaquinaDepartamentos> MovMaquinaDepartamentos { get; set; }
  public virtual DbSet<MovMaquinaDisReposicione> MovMaquinaDisReposiciones { get; set; }
  public virtual DbSet<MovMaquinaModeloReferenciaAlbarane> MovMaquinaModeloReferenciaAlbaranes { get; set; }
  public virtual DbSet<MovMaquinasIncidencia> MovMaquinasIncidencias { get; set; }
  public virtual DbSet<MovMaquinasPuerta> MovMaquinasPuertas { get; set; }
  public virtual DbSet<MovModCli> MovModClis { get; set; }  
  public virtual DbSet<MovModSubCliente> MovModSubClientes { get; set; }
  public virtual DbSet<MovModeloTallaSub> MovModeloTallaSubs { get; set; }
  public virtual DbSet<MovMostrarAntenasPuesto> MovMostrarAntenasPuestos { get; set; }
  public virtual DbSet<MovPlcli> MovPlclis { get; set; }
  public virtual DbSet<MovPren> MovPrens { get; set; }
  public virtual DbSet<MovSimbolizadoModeloTallaFord> MovSimbolizadoModeloTallaFords { get; set; }
  public virtual DbSet<MovReposicione> MovReposiciones { get; set; }
  public virtual DbSet<MovSopl> MovSopls { get; set; }
  public virtual DbSet<MovSopl2> MovSopl2s { get; set; }
  public virtual DbSet<MovStockPrendasUsu> MovStockPrendasUsus { get; set; }
  public virtual DbSet<MovStockPrendasUsuHistorico> MovStockPrendasUsuHistoricos { get; set; }
  public virtual DbSet<MovSubClienteIdentificacionManual> MovSubClienteIdentificacionManuals { get; set; }
  public virtual DbSet<MovSubClienteIdioma> MovSubClienteIdiomas { get; set; }
  public virtual DbSet<MovSubClientesDeGuardium> MovSubClientesDeGuardia { get; set; }
  public virtual DbSet<MovSubClientesGestionTaquilla> MovSubClientesGestionTaquillas { get; set; }
  public virtual DbSet<MovSubClientesTipoBaja> MovSubClientesTipoBajas { get; set; }
  public virtual DbSet<MovSubClientesTraslado> MovSubClientesTraslados { get; set; }
  public virtual DbSet<MovTallasModelo> MovTallasModelos { get; set; }
  public virtual DbSet<MovTaquilla> MovTaquilla { get; set; }
  public virtual DbSet<MovTaquillas> MovTaquillas { get; set; }
  public virtual DbSet<MovTelefono> MovTelefonos { get; set; }
  public virtual DbSet<MovTelefonosCliente> MovTelefonosClientes { get; set; }
  public virtual DbSet<MovTelefonosPropio> MovTelefonosPropios { get; set; }
  public virtual DbSet<MovTelefonosProveedore> MovTelefonosProveedores { get; set; }
  public virtual DbSet<MovTipoAntenaPdum> MovTipoAntenaPda { get; set; }
  public virtual DbSet<OldCode> OldCodes { get; set; }
  public virtual DbSet<PruebasVictor1> PruebasVictor1s { get; set; }
  public virtual DbSet<PuertaDispensacione> PuertaDispensaciones { get; set; }
  public virtual DbSet<PuertaEstado> PuertaEstados { get; set; }
  public virtual DbSet<RptFrecuenciaMovimientosPrenda> RptFrecuenciaMovimientosPrendas { get; set; }
  public virtual DbSet<TLabelsAcumuladasLaundry> TLabelsAcumuladasLaundries { get; set; }
  public virtual DbSet<TTerminosFijo> TTerminosFijos { get; set; }
  public virtual DbSet<TmpCuad> TmpCuads { get; set; }
  public virtual DbSet<VerificacionFuncionamiento> VerificacionFuncionamientos { get; set; }
  public virtual DbSet<VistaGfh> VistaGfhs { get; set; }
  public virtual DbSet<VistaInformePrendaUsuario> VistaInformePrendaUsuarios { get; set; }
  public virtual DbSet<VistaMovPre> VistaMovPres { get; set; }
  public virtual DbSet<VistaPrendasYultimoMovimiento> VistaPrendasYultimoMovimientos { get; set; }
  public virtual DbSet<PrendaDisponibleEnLenceria> PrendaDisponibleEnLencerias { get; set; }
  public virtual DbSet<PrendasDisponiblesSubCliente> PrendasDisponiblesSubClientes { get; set; }
  public virtual DbSet<SubClientHistoryModel> SubClientHistoryModels { get; set; }
  public virtual DbSet<RegistroIdentificacionUsuario> RegistroIdentificacionUsuario { get; set; }
  public virtual DbSet<PrendasDisponiblesEnTaquillasInteligentes> PrendasDisponiblesEnTaquillasInteligentes { get; set; }
  public virtual DbSet<PrendasDisponiblesEnMaquinaDispensacion> PrendasDisponiblesEnMaquinaDispensacion { get; set; }
  public virtual DbSet<PrendasDisponiblesEnQuesitoConRFID> PrendasDisponiblesEnQuesitoConRFIDs { get; set; }
  public virtual DbSet<ImportUserCampoControl> ImportUserCampoControl { get; set; }
  public virtual DbSet<MasterSubClienteTarjeta> MasterSubClientesTarjetas { get; set; }

  public virtual DbSet<IntentosLoginFallidos> IntentosLoginFallidos { get; set; }
  public virtual DbSet<CreditosDisponiblesModel> CreditosDisponiblesModels { get; set; }

  public virtual DbSet<MovMaquinaTiempoDispensado> MovMaquinaTiempoDispensado { get; set; }
  public virtual DbSet<MasterModeloPrendaComposicion> MasterModeloPrendaComposicions { get; set; }
  public virtual DbSet<MovPrendasNumeroCiclosExternos> MovPrendasNumeroCiclosExternos { get; set; }

  // Security entities for Secure API
  public virtual DbSet<Device> Devices { get; set; }
  public virtual DbSet<AccessLog> AccessLogs { get; set; }
  public virtual DbSet<OperationsLog> OperationsLogs { get; set; }
  public virtual DbSet<LinenOperation> LinenOperations { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.UseCollation("Modern_Spanish_100_CI_AS");

    modelBuilder.Entity<LFS.Shared.Entities.Sex>().HasData(
               new LFS.Shared.Entities.Sex { Id = 1, Description = "Male", SortOrder = 1 },
               new LFS.Shared.Entities.Sex { Id = 2, Description = "Female", SortOrder = 2 }
           );
    modelBuilder.Entity<LFS.Shared.Entities.Alert>().HasData(
            new LFS.Shared.Entities.Alert { Id = 1, Description = "End of stock", Enabled = true },
            new LFS.Shared.Entities.Alert { Id = 2, Description = "At stock level", Enabled = true },
            new LFS.Shared.Entities.Alert { Id = 3, Description = "Cart full", Enabled = true },
            new LFS.Shared.Entities.Alert { Id = 4, Description = "Soiled items", Enabled = true }
        );

    modelBuilder.Entity<LFS.Shared.Entities.MenuGroup>().HasData(
        new LFS.Shared.Entities.MenuGroup { Id = 1, Description = "Users", SortOrder = 1, Active = true, ClassName = "fas fa-users", ResourceString = "strUsers" },
        new LFS.Shared.Entities.MenuGroup { Id = 2, Description = "Items", SortOrder = 2, Active = true, ClassName = "fas fa-puzzle-piece", ResourceString = "strItems" },
        new LFS.Shared.Entities.MenuGroup { Id = 3, Description = "Machines", SortOrder = 3, Active = true, ClassName = "fas fa-industry", ResourceString = "strMachines" },
        new LFS.Shared.Entities.MenuGroup { Id = 4, Description = "System", SortOrder = 4, Active = true, ClassName = "fa fa-desktop", ResourceString = "strSystem" },
        new LFS.Shared.Entities.MenuGroup { Id = 5, Description = "Reports", SortOrder = 5, Active = true, ClassName = "fa fa-file", ResourceString = "strReports" },
        new LFS.Shared.Entities.MenuGroup { Id = 6, Description = "Roles", SortOrder = 1, Active = true, ClassName = "fa fa-tasks", ResourceString = "strRoles", ParentId = 1 }
        );

    modelBuilder.Entity<LFS.Shared.Entities.Form>().HasData(
        new LFS.Shared.Entities.Form { Id = 1, Name = "Home", Url = "/", StringMatch = "/", ResourceString = "strHome", ClassString = "fas fa-chart-pie", SortOrder = 1, Active = true, AuthenticationRequired = false },
        new LFS.Shared.Entities.Form { Id = 2, Name = "Users", Url = "/Users/Index", StringMatch = "/users", ResourceString = "strUsers", ClassString = "fas fa-users", MenuGroupId = 1, SortOrder = 2, Active = true, Create = true, Edit = true, Delete = true },
        new LFS.Shared.Entities.Form { Id = 3, Name = "Roles", Url = "/Roles/Index", StringMatch = "/roles", ResourceString = "strRoles", ClassString = "", MenuGroupId = 6, SortOrder = 2, Active = true, Create = true, Edit = false, Delete = false, OnlyAdmin = true },
        new LFS.Shared.Entities.Form { Id = 4, Name = "User Roles", Url = "/UserRoles/Index", StringMatch = "/userroles", ResourceString = "strUserRoles", ClassString = "", MenuGroupId = 6, SortOrder = 3, Active = true, Create = false, Edit = true, Delete = false, OnlyAdmin = true },
        new LFS.Shared.Entities.Form { Id = 5, Name = "Role Details", Url = "/Admin/RoleDetails/Index", StringMatch = "/admin/roledetails", ResourceString = "strRoleDetails", ClassString = "", MenuGroupId = 6, SortOrder = 4, Active = true, Create = false, Edit = true, Delete = false, OnlyAdmin = true },
        new LFS.Shared.Entities.Form { Id = 6, Name = "Departments", Url = "/Departments/Index", StringMatch = "/departments", ResourceString = "strDepartments", ClassString = "fa fa-sitemap", MenuGroupId = 1, SortOrder = 6, Active = true, Create = true, Edit = true, Delete = true },
        new LFS.Shared.Entities.Form { Id = 7, Name = "Item Users", Url = "/Admin/ItemUsers/Index", StringMatch = "/admin/itemusers", ResourceString = "strItemUsers", ClassString = "", MenuGroupId = 1, SortOrder = 7, Active = true, Create = true, Edit = true, Delete = true },
        new LFS.Shared.Entities.Form { Id = 8, Name = "Load Users", Url = "/LoadUsers/Index", StringMatch = "/loadusers", ResourceString = "strLoadUsers", ClassString = "", MenuGroupId = 1, SortOrder = 8, Active = true, Create = true, Edit = true, Delete = true },
        new LFS.Shared.Entities.Form { Id = 23, Name = "Upload Item Users", Url = "/Admin/ImportUsers/Index", StringMatch = "/admin/importusers", ResourceString = "strImportUsers", ClassString = "", MenuGroupId = 1, SortOrder = 9, Active = true, Create = true, Edit = false, Delete = false },
        new LFS.Shared.Entities.Form { Id = 26, Name = "Application Users", Url = "/Admin/ApplicationUsers/Index", StringMatch = "/admin/applicationusers", ResourceString = "strApplicationUsers", ClassString = "", MenuGroupId = 1, SortOrder = 10, Active = true, Create = true, Edit = false, Delete = false },
        //new LFS.Shared.Entities.Form { Id = 27, Name = "Listings", Url = "/Listings/ConsumptionList/Index", StringMatch = "/listings/consumptionlist", ResourceString = "strConsumptionList", ClassString = "", MenuGroupId = 5, SortOrder = 10, Active = true, Create = true, Edit = false, Delete = false },
        //new LFS.Shared.Entities.Form { Id = 28, Name = "Listings", Url = "/Listings/DetailedListConsumption/Index", StringMatch = "/listings/consumptionlist", ResourceString = "strDetailedListConsumption", ClassString = "", MenuGroupId = 5, SortOrder = 10, Active = true, Create = true, Edit = false, Delete = false },
        //new LFS.Shared.Entities.Form { Id = 29, Name = "Listings", Url = "/Listings/StockOfMachines/Index", StringMatch = "/listings/stockofmachines", ResourceString = "strStockOfMachines", ClassString = "", MenuGroupId = 5, SortOrder = 10, Active = true, Create = true, Edit = false, Delete = false },
        //new LFS.Shared.Entities.Form { Id = 30, Name = "Listings", Url = "/Listings/GarmentsInCollectors/Index", StringMatch = "/listings/garmentsincollectors", ResourceString = "strGarmentsInCollectors", ClassString = "", MenuGroupId = 5, SortOrder = 10, Active = true, Create = true, Edit = false, Delete = false },
        //new LFS.Shared.Entities.Form { Id = 31, Name = "Listings", Url = "/Listings/DownLoadList/Index", StringMatch = "/listings/downloadlist", ResourceString = "strDownloadList", ClassString = "", MenuGroupId = 5, SortOrder = 10, Active = true, Create = true, Edit = false, Delete = false },

        new LFS.Shared.Entities.Form { Id = 9, Name = "Family Item Types", Url = "/Admin/FamilyItemTypes/Index", StringMatch = "/admin/familyitemtypes", ResourceString = "strFamilyItemTypes", ClassString = "", MenuGroupId = 2, SortOrder = 1, Active = true, Create = true, Edit = true, Delete = true },
        new LFS.Shared.Entities.Form { Id = 10, Name = "Family Item Models", Url = "/Admin/FamilyItemModels/Index", StringMatch = "/admin/familyitemmodels", ResourceString = "strFamilyItemModels", ClassString = "", MenuGroupId = 2, SortOrder = 2, Active = true, Create = true, Edit = true, Delete = true },
        new LFS.Shared.Entities.Form { Id = 11, Name = "Item Sizes", Url = "/ItemSizes/Index", StringMatch = "/itemsizes", ResourceString = "strItemSizes", ClassString = "", MenuGroupId = 2, SortOrder = 3, Active = true, Create = true, Edit = true, Delete = true },
        new LFS.Shared.Entities.Form { Id = 12, Name = "Family Items", Url = "/FamilyItems/Index", StringMatch = "/familyitems", ResourceString = "strFamilyItems", ClassString = "fa fa-bezier-curve", MenuGroupId = 2, SortOrder = 4, Active = true, Create = true, Edit = true, Delete = true },
        new LFS.Shared.Entities.Form { Id = 13, Name = "Items", Url = "/Items/Index", StringMatch = "/items", ResourceString = "strItems", ClassString = "fas fa-puzzle-piece", MenuGroupId = 2, SortOrder = 5, Active = true, Create = true, Edit = true, Delete = true },
        new LFS.Shared.Entities.Form { Id = 14, Name = "Customer Item Models", Url = "/CustomerItemModels/Index", StringMatch = "/customeritemmodels", ResourceString = "strCustomerItemModels", ClassString = "fa fa-cubes", MenuGroupId = 2, SortOrder = 6, Active = true, Create = false, Edit = true, Delete = false },

        new LFS.Shared.Entities.Form { Id = 15, Name = "Machine Models", Url = "/Admin/MachineModels/Index", StringMatch = "/admin/machinemodels", ResourceString = "strMachineModels", ClassString = "", MenuGroupId = 3, SortOrder = 1, Active = true, Create = true, Edit = true, Delete = true },
        new LFS.Shared.Entities.Form { Id = 16, Name = "Machines", Url = "/Admin/Machines/Index", StringMatch = "/admin/machines/", ResourceString = "strMachines", ClassString = "", MenuGroupId = 3, SortOrder = 2, Active = true, Create = true, Edit = true, Delete = true },
        new LFS.Shared.Entities.Form { Id = 17, Name = "Warehouses", Url = "/Warehouses/Index", StringMatch = "/warehouses", ResourceString = "strWarehouses", ClassString = "", MenuGroupId = 3, SortOrder = 3, Active = true, Create = true, Edit = true, Delete = true },
        new LFS.Shared.Entities.Form { Id = 18, Name = "Alerts", Url = "/Alerts/Index", StringMatch = "/alerts", ResourceString = "strAlerts", ClassString = "", MenuGroupId = 3, SortOrder = 4, Active = true, Create = false, Edit = true, Delete = false },

        new LFS.Shared.Entities.Form { Id = 19, Name = "Customers", Url = "/Admin/Customers/Index", StringMatch = "/admin/customers", ResourceString = "strCustomers", ClassString = "", MenuGroupId = 4, SortOrder = 1, Active = true, Create = true, Edit = true, Delete = true },
        new LFS.Shared.Entities.Form { Id = 20, Name = "Suppliers", Url = "/Admin/Suppliers/Index", StringMatch = "/admin/suppliers", ResourceString = "strSuppliers", ClassString = "", MenuGroupId = 5, SortOrder = 10, Active = true, Create = true, Edit = true, Delete = true },
        new LFS.Shared.Entities.Form { Id = 21, Name = "Listings", Url = "/Admin/Movements/Index", StringMatch = "/admin/movements/", ResourceString = "strMovements", ClassString = "", MenuGroupId = 4, SortOrder = 3, Active = true, Create = true, Edit = true, Delete = true },
        new LFS.Shared.Entities.Form { Id = 22, Name = "Locations", Url = "/Admin/Locations/Index", StringMatch = "/admin/locations/", ResourceString = "strLocations", ClassString = "", MenuGroupId = 4, SortOrder = 4, Active = true, Create = true, Edit = true, Delete = true },

        new LFS.Shared.Entities.Form { Id = 24, Name = "Stock Report", Url = "/ReportViewer?reportName=MachineStock&title=Stock&id=24", StringMatch = "reportname=machinestock", ResourceString = "strStockReport", ClassString = "", MenuGroupId = 5, SortOrder = 1, Active = true, Create = true, Edit = false, Delete = false },
        new LFS.Shared.Entities.Form { Id = 25, Name = "Item Users Report", Url = "/ReportViewer?reportName=ItemUsers&title=Users&id=25", StringMatch = "reportname=itemusers", ResourceString = "strItemUsersReport", ClassString = "", MenuGroupId = 5, SortOrder = 2, Active = true, Create = true, Edit = false, Delete = false }
    );

    modelBuilder.Entity<IdentityUserLogin<string>>()
    .HasKey(l => new { l.LoginProvider, l.ProviderKey });
    modelBuilder.Entity<IdentityUserRole<string>>()
    .HasKey(l => new { l.UserId, l.RoleId });
    modelBuilder.Entity<IdentityUserToken<string>>()
    .HasKey(l => new { l.UserId, l.LoginProvider, l.Name });

    modelBuilder.Entity<AppUserCustomer>()
            .HasKey(uc => new { uc.AppUserId, uc.MasterClientesId });

    // dbo.ApplicationUsers (admin list for mobile QR); entity class name is User.
    modelBuilder.Entity<LFS.Shared.Entities.User>(entity =>
    {
      entity.ToTable("ApplicationUsers");
    });

    modelBuilder.Entity<AperturaTaquilla>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Fecha)
              .HasColumnType("datetime")
              .HasColumnName("fecha");
      entity.Property(e => e.IdAlmacen).HasColumnName("idAlmacen");
      entity.Property(e => e.IdModelo).HasColumnName("idModelo");
      entity.Property(e => e.IdSubCli).HasColumnName("idSubCli");
      entity.Property(e => e.Talla)
              .HasMaxLength(10)
              .HasColumnName("talla");
    });

    modelBuilder.Entity<AvisoPrenda>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__AvisoPre__3213E83FF75C65ED");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Borra).HasDefaultValueSql("((0))");
      entity.Property(e => e.Comments).HasMaxLength(250);
      entity.Property(e => e.DateClose).HasColumnType("datetime");
      entity.Property(e => e.DateOpen).HasColumnType("datetime");
      entity.ToTable(e =>
          {
          e.HasTrigger("AvisoPrenda_insert_trigger");
          e.HasTrigger("AvisoPrenda_update_trigger");
          e.HasTrigger("AvisoPrenda_delete_trigger");
        });
    });


    modelBuilder.Entity<DatosPropio>(entity =>
{
  entity.HasKey(e => e.Id)
              .HasName("aaaaaDatosPropios_PK")
              .IsClustered(false);

  entity.HasIndex(e => e.Id, "Id");

  entity.Property(e => e.Cif)
              .HasMaxLength(13)
              .HasColumnName("CIF");
  entity.Property(e => e.ContadorLotes).HasColumnName("Contador_Lotes");
  entity.Property(e => e.Iva).HasColumnName("IVA");
  entity.Property(e => e.Nombre).HasMaxLength(50);
  entity.Property(e => e.RazonSocial)
              .HasMaxLength(50)
              .HasColumnName("Razon_Social");
});

    modelBuilder.Entity<FingerTable>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_fingertable");

      entity.ToTable("FingerTable");

      entity.Property(e => e.Id)
              .ValueGeneratedNever()
              .HasColumnName("id");
      entity.Property(e => e.Blob)
              .IsRequired()
              .HasColumnName("blob");
    });

    modelBuilder.Entity<FotosMovPren>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.ToTable("FotosMovPren");

      entity.Property(e => e.Foto)
              .IsRequired()
              .HasColumnType("image");
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<FotosPrenda>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Foto)
              .IsRequired()
              .HasColumnType("image");
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<GrupoMovimientoTipoAntena>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("GrupoMovimientoTipoAntena");
    });

    modelBuilder.Entity<ImportCategoriaPrincipal>(entity =>
    {
      entity.HasKey(e => e.Clave);

      entity.ToTable("Import_CategoriaPrincipal");

      entity.Property(e => e.Clave).ValueGeneratedNever();
      entity.Property(e => e.Descripcion)
              .IsRequired()
              .HasMaxLength(250);
    });

    modelBuilder.Entity<ImportCategoriaSecundarium>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.ToTable("Import_CategoriaSecundaria");

      entity.Property(e => e.Descripcion)
              .IsRequired()
              .HasMaxLength(250);
    });

    modelBuilder.Entity<ImportMovCategoriasDepartamento>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.ToTable("Import_MovCategoriasDepartamento");
    });

    modelBuilder.Entity<LogDiferenciasRecepcionRFID>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LogDiferenciasRecepcionRFID");

      entity.Property(e => e.CodigoTagLeido).HasMaxLength(50);
      entity.Property(e => e.Fecha).HasColumnType("datetime");
      entity.Property(e => e.FechaDispensacionTag).HasColumnType("datetime");
      entity.Property(e => e.Id)
              .ValueGeneratedOnAdd()
              .HasColumnName("id");
      entity.Property(e => e.IdMaquina).HasColumnName("idMaquina");
      entity.Property(e => e.IdModeloLeido).HasColumnName("idModeloLeido");
      entity.Property(e => e.IdModeloSeleccionado).HasColumnName("idModeloSeleccionado");
      entity.Property(e => e.IdUsuarioLeido).HasColumnName("idUsuarioLeido");
      entity.Property(e => e.IdUsuarioSeleccionado).HasColumnName("idUsuarioSeleccionado");
    });

    modelBuilder.Entity<LogErroresDispensacion>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.ToTable("LogErroresDispensacion");

      entity.Property(e => e.Fecha).HasColumnType("datetime");
      entity.Property(e => e.Id)
              .ValueGeneratedOnAdd()
              .HasColumnName("id");
      entity.Property(e => e.IdAlmacen).HasColumnName("idAlmacen");
      entity.Property(e => e.IdMaquina).HasColumnName("idMaquina");
      entity.Property(e => e.IdModelo).HasColumnName("idModelo");
      entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
      entity.Property(e => e.Observaciones).HasMaxLength(150);
      entity.Property(e => e.Talla)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<LogErroresRecoleccion>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("LogErroresRecoleccion");

      entity.Property(e => e.Fecha).HasColumnType("datetime");
      entity.Property(e => e.HaLeidoModeloDiferente).HasColumnName("haLeidoModeloDiferente");
      entity.Property(e => e.HaLeidoUsuarioDiferente).HasColumnName("haLeidoUsuarioDiferente");
      entity.Property(e => e.Id)
              .ValueGeneratedOnAdd()
              .HasColumnName("id");
      entity.Property(e => e.IdMaquina).HasColumnName("idMaquina");
      entity.Property(e => e.IdModelo).HasColumnName("idModelo");
      entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
      entity.Property(e => e.Observaciones).HasMaxLength(150);
    });

    modelBuilder.Entity<LogMaquina>(entity =>
    {
      entity.HasKey(e => e.IdLog);

      entity.Property(e => e.IdLog).HasColumnName("Id_log");
      entity.Property(e => e.ComentarioLog)
              .HasMaxLength(500)
              .HasColumnName("Comentario_log");
      entity.Property(e => e.FechaLog)
              .HasColumnType("datetime")
              .HasColumnName("Fecha_log");
      entity.Property(e => e.IdMaquinaLog).HasColumnName("IdMaquina_log");
      entity.Property(e => e.TipoLog)
              .IsRequired()
              .HasMaxLength(50)
              .HasColumnName("Tipo_log");
      entity.Property(e => e.TrazaLog)
              .IsRequired()
              .HasMaxLength(50)
              .HasColumnName("Traza_log");
    });

    modelBuilder.Entity<MaquinasDispensadorasParametro>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_MovMaquinasDispensadorasParametros");

      entity.Property(e => e.LecturaTagpantallaPrincipalLenceria).HasColumnName("LecturaTAGPantallaPrincipalLenceria");

      entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.MaquinasDispensadorasParametros)
              .HasForeignKey(d => d.IdMaquina)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_t_MaquinasDispensadorasParametros_MasterMaquinaDispensacion");
      entity.ToTable(e =>
          {
          e.HasTrigger("MaquinasDispensadorasParametro_insert_trigger");
          e.HasTrigger("MaquinasDispensadorasParametro_update_trigger");
          e.HasTrigger("MaquinasDispensadorasParametro_delete_trigger");
        });
    });

    modelBuilder.Entity<MasterActividade>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__MasterAc__3213E83F3C3063AF");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Descripcion)
              .IsRequired()
              .HasMaxLength(100);
    });

    modelBuilder.Entity<MasterAviso>(entity =>
    {
      entity.HasKey(e => e.IdAvis).HasName("PK__MasterAv__2CD940C7A8AB20BC");

      entity.Property(e => e.IdAvis).ValueGeneratedNever();
      entity.Property(e => e.DesAvi).HasMaxLength(100);
    });

    modelBuilder.Entity<MasterAvisosPorEmail>(entity =>
    {
      entity.HasKey(e => e.Tipo).HasName("PK__MasterAv__8E762CB54E1CE041");

      entity.ToTable("MasterAvisosPorEmail");

      entity.Property(e => e.Tipo).ValueGeneratedNever();
      entity.Property(e => e.Aviso)
              .IsRequired()
              .HasMaxLength(50);
      entity.Property(e => e.DireccionEmail).HasMaxLength(200);
      entity.Property(e => e.Habilitado)
              .IsRequired()
              .HasDefaultValueSql("((1))");
      entity.Property(e => e.LimiteEsMinimo).HasDefaultValueSql("((1))");
      entity.Property(e => e.NumerosSms).HasColumnName("NumerosSMS");
      entity.ToTable(e =>
          {
          e.HasTrigger("MasterAvisosPorEmail_insert_trigger");
          e.HasTrigger("MasterAvisosPorEmail_update_trigger");
          e.HasTrigger("MasterAvisosPorEmail_delete_trigger");
        });

    });

    modelBuilder.Entity<MasterCategoriaIncidencium>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterCategoriaIncidencia_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.Id, "id");

      entity.Property(e => e.Id)
              .ValueGeneratedNever()
              .HasColumnName("id");
      entity.Property(e => e.Descripcion).HasMaxLength(50);
      entity.Property(e => e.System)
              .HasDefaultValueSql("((0))")
              .HasColumnName("system");
    });

    modelBuilder.Entity<MasterClientes>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterClientes_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdContactoPrincipal, "id_ContactoPrincipal");

      entity.HasIndex(e => e.IdEtiquetaDefecto, "id_etiqueta_defecto");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Actividad).HasMaxLength(50);
      entity.Property(e => e.Borrado).HasDefaultValueSql("((0))");
      entity.Property(e => e.CantidadTaquillas)
              .HasDefaultValueSql("((1))")
              .HasColumnName("cantidadTaquillas");
      entity.Property(e => e.Cif)
              .HasMaxLength(30)
              .HasColumnName("CIF");
      entity.Property(e => e.DenominacionSubCliente).HasMaxLength(50);
      entity.Property(e => e.FechaAlta).HasColumnType("datetime");
      entity.Property(e => e.FechaBaja).HasColumnType("datetime");
      entity.Property(e => e.IdActividad).HasColumnName("id_Actividad");
      entity.Property(e => e.IdClienteLavander).HasColumnName("idClienteLavander");
      entity.Property(e => e.IdContactoPrincipal).HasColumnName("id_ContactoPrincipal");
      entity.Property(e => e.IdEtiquetaDefecto).HasColumnName("id_etiqueta_defecto");
      entity.Property(e => e.Iva).HasColumnName("IVA");
      entity.Property(e => e.MaquinasRfid)
              .IsRequired()
              .HasDefaultValueSql("((1))")
              .HasColumnName("MaquinasRFID");
      entity.Property(e => e.Nombre).HasMaxLength(100);
      entity.Property(e => e.OrdenacionSoplsoloConPosicionesFijas).HasColumnName("OrdenacionSOPLSoloConPosicionesFijas");
      entity.Property(e => e.RazonSocial)
              .HasMaxLength(150)
              .HasColumnName("Razon_Social");
      entity.Property(e => e.TextoEtiquetaPrenda).HasMaxLength(200);
      entity.Property(e => e.TipoMarcadoPrenda).HasDefaultValueSql("((1))");

      entity.HasOne(d => d.TipoMarcadoPrendaNavigation).WithMany(p => p.MasterClientes)
              .HasForeignKey(d => d.TipoMarcadoPrenda)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MasterClientes_MasterTipoMarcadoPrendas");

      entity.ToTable(tb => 
      {
        tb.HasTrigger("MasterClientes_insert_trigger");
        tb.HasTrigger("MasterClientes_update_trigger");
        tb.HasTrigger("MasterClientes_delete_trigger");
      });
      
    });   

    modelBuilder.Entity<MasterComandosLoader>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterComandosLoader_PK")
              .IsClustered(false);

      entity.ToTable("MasterComandosLoader");

      entity.HasIndex(e => e.Id, "Id");

      entity.Property(e => e.Descrip).HasMaxLength(50);
    });

    modelBuilder.Entity<MasterControl>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__MasterCo__3213E83FC5B41FF6");

      entity.ToTable("MasterControl");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.CantMaxPrendasUso).HasDefaultValueSql("((1))");
      entity.Property(e => e.EmailEmpresa)
              .HasMaxLength(60)
              .IsUnicode(false);
      entity.Property(e => e.EmailPasswd)
              .HasMaxLength(60)
              .IsUnicode(false);
      entity.Property(e => e.EmailServidor)
              .HasMaxLength(60)
              .IsUnicode(false);
      entity.Property(e => e.EmailSsl).HasColumnName("EmailSSL");
      entity.Property(e => e.EmailUser)
              .HasMaxLength(60)
              .IsUnicode(false);
      entity.Property(e => e.TextoEtiquetaPrenda)
              .HasMaxLength(200)
              .IsUnicode(false);
      entity.Property(e => e.VersionwebCtr).HasColumnName("versionweb_ctr");
    });

    modelBuilder.Entity<MasterDepartamentosCliente>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterDepartamentosCliente_PK")
              .IsClustered(false);

      entity.ToTable("MasterDepartamentosCliente");

      entity.HasIndex(e => e.IdCliente, "MasterClientesMasterDepartamentosCliente");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdCliente, "id_cliente");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Descripcion).HasMaxLength(50);
      entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

      entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.MasterDepartamentosClientes)
              .HasForeignKey(d => d.IdCliente)
              .HasConstraintName("MasterDepartamentosClient_FK00");
      entity.ToTable(e =>
          {
          e.HasTrigger("MasterDepartamentosCliente_insert_trigger");
          e.HasTrigger("MasterDepartamentosCliente_update_trigger");
          e.HasTrigger("MasterDepartamentosCliente_delete_trigger");
        });
    });

    modelBuilder.Entity<MasterDotacione>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_Dotaciones");

      entity.Property(e => e.Descripcion).HasMaxLength(100);
      entity.ToTable(e =>
          {
          e.HasTrigger("MasterDotacione_insert_trigger");
          e.HasTrigger("MasterDotacione_update_trigger");
          e.HasTrigger("MasterDotacione_delete_trigger");
        });
    });

    modelBuilder.Entity<MasterEdificio>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Descripcion)
              .IsRequired()
              .HasMaxLength(50);
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<MasterEdificiosPuesto>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<MasterEstadoLlaveTaquilla>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MasterEstadoLlaveTaquilla");

      entity.Property(e => e.Descripcion)
              .HasMaxLength(10)
              .IsFixedLength();
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<MasterEstadoTaquilla>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MasterEstadoTaquilla");

      entity.Property(e => e.Descripcion)
              .HasMaxLength(20)
              .IsFixedLength();
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<MasterEtiqueta>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterEtiquetas_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.Id, "id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.ArchivoRpt)
              .HasMaxLength(50)
              .HasColumnName("archivo_rpt");
      entity.Property(e => e.Descripcion)
              .HasMaxLength(50)
              .HasColumnName("descripcion");
    });

    modelBuilder.Entity<MasterFormasContacto>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterFormasContacto_PK")
              .IsClustered(false);

      entity.ToTable("MasterFormasContacto");

      entity.HasIndex(e => e.Id, "id2");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Denominacion)
              .HasMaxLength(50)
              .HasColumnName("denominacion");
      entity.Property(e => e.System)
              .HasDefaultValueSql("((0))")
              .HasColumnName("system");
      entity.Property(e => e.Tipo)
              .HasComment("-1=Ninguno;1=TelÃ©fono,2=movil,3=fax,4=email (0=def por usuario)")
              .HasColumnName("tipo");
    });

    modelBuilder.Entity<MasterGruposDeMovimiento>(entity =>
    {
      entity.Property(e => e.Descripcion)
              .IsRequired()
              .HasMaxLength(30);
    });

    modelBuilder.Entity<MasterImpresorasEtiqueta>(entity =>
    {
      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.Descripcion)
              .IsRequired()
              .HasMaxLength(50);
    });

    modelBuilder.Entity<MasterIncidencia>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterIncidencias_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.Id, "Id");

      entity.Property(e => e.Id).ValueGeneratedNever();
      entity.Property(e => e.Descrip).HasMaxLength(150);
    });

    modelBuilder.Entity<MasterMaquinaDispensacion>(entity =>
    {
      entity.ToTable("MasterMaquinaDispensacion");

      entity.Property(e => e.CantHuecos).HasColumnName("cantHuecos");
      entity.Property(e => e.CtoServTecnico).HasMaxLength(100);
      entity.Property(e => e.Descripcion)
              .IsRequired()
              .HasMaxLength(50);
      entity.Property(e => e.EmailAviso).HasColumnName("emailAviso");
      entity.Property(e => e.HoraReposicion).HasColumnType("datetime");
      entity.Property(e => e.IdAlmacenCentral).HasColumnName("idAlmacenCentral");
      entity.Property(e => e.IdModeloDftDer).HasColumnName("idModeloDftDer");
      entity.Property(e => e.IdModeloDftIzq).HasColumnName("idModeloDftIzq");
      entity.Property(e => e.IdPuesto).HasColumnName("idPuesto");
      entity.Property(e => e.IdentificadorLogo).HasMaxLength(50);
      entity.Property(e => e.Iplector)
              .HasMaxLength(100)
              .HasColumnName("IPLector");
      entity.Property(e => e.Observaciones).HasMaxLength(200);
      entity.Property(e => e.PedirCodigoNuevoUsuario)
              .IsRequired()
              .HasDefaultValueSql("((1))");
      entity.Property(e => e.PuertoPlc).HasColumnName("PuertoPLC");
      entity.Property(e => e.PuertoPlc2).HasColumnName("PuertoPLC2");
      entity.Property(e => e.ResolucionPantalla)
              .HasMaxLength(20)
              .IsUnicode(false);
      entity.Property(e => e.TipoAntenaUhf).HasColumnName("TipoAntenaUHF");
      entity.Property(e => e.NumeroPrendasSucias).HasColumnName("NumeroPrendasSucias");

      entity.HasOne(d => d.IdPuestoNavigation).WithMany(p => p.MasterMaquinaDispensacions)
              .HasForeignKey(d => d.IdPuesto)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MasterMaquinaDispensacion_MasterPuestos");

      entity.HasOne(d => d.IdTipoMaquinaNavigation).WithMany(p => p.MasterMaquinaDispensacions)
              .HasForeignKey(d => d.IdTipoMaquina)
              .HasConstraintName("FK_MasterMaquinaDispensacion_MasterTipoMaquinaDispensadora");

      entity.ToTable(e =>
          {
          e.HasTrigger("MasterMaquinaDispensacion_insert_trigger");
          e.HasTrigger("MasterMaquinaDispensacion_update_trigger");
          e.HasTrigger("MasterMaquinaDispensacion_delete_trigger");
        });
    });

    modelBuilder.Entity<MasterMensajesMaquina>(entity =>
    {
      entity.HasNoKey();
      entity.Property(e => e.IdMensajeMaquina).ValueGeneratedOnAdd();
      entity.Property(e => e.IdMaquina)
            .IsRequired()
            .HasColumnType("int");
      entity.Property(e => e.Mensaje)
            .HasMaxLength(150)
            .IsUnicode(false);
      entity.Property(e => e.IdMensaje).HasDefaultValueSql("((1))");
    });

    modelBuilder.Entity<MasterModeloAntena>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__MasterMo__3213E83F7E1EDBDE");

      entity.ToTable("MasterModeloAntena");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Descripcion)
              .IsRequired()
              .HasMaxLength(100);
      entity.Property(e => e.Numero).HasColumnName("numero");
    });

    modelBuilder.Entity<MasterModeloPrenda>(entity =>
    {
      entity.ToTable(tb => tb.HasTrigger("MasterModeloPrenda_insert_trigger"));

      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterModeloPrenda_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.IdProveedor, "MasterProveedoresMasterTiposPrenda");

      entity.HasIndex(e => e.IdTipoPrenda, "MasterTipoPrendaMasterModeloPrenda");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdProveedor, "id_Proveedor");

      entity.HasIndex(e => e.IdTipoPrenda, "id_TipoPrenda");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Descripcion).HasMaxLength(50);
      entity.Property(e => e.IdProveedor).HasColumnName("id_Proveedor");
      entity.Property(e => e.IdTipoPrenda).HasColumnName("id_TipoPrenda");
      entity.Property(e => e.TipoPosicionSopl).HasColumnName("TipoPosicionSOPL");
      entity.Property(e => e.Precio).HasPrecision(18, 2);

      entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.MasterModeloPrenda)
              .HasForeignKey(d => d.IdProveedor)
              .HasConstraintName("MasterModeloPrenda_FK00");

      entity.HasOne(d => d.IdTipoPrendaNavigation).WithMany(p => p.MasterModeloPrenda)
              .HasForeignKey(d => d.IdTipoPrenda)
              .HasConstraintName("MasterModeloPrenda_FK01");
    });

    modelBuilder.Entity<MasterModeloTaquilla>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MasterModeloTaquilla");

      entity.Property(e => e.Descripcion)
              .IsRequired()
              .HasMaxLength(100);
      entity.Property(e => e.Id)
              .ValueGeneratedOnAdd()
              .HasColumnName("id");
    });

    modelBuilder.Entity<MasterModelosDotacion>(entity =>
    {
      entity.ToTable("MasterModelosDotacion");

      entity.Property(e => e.Color).HasMaxLength(50);
      entity.Property(e => e.CrearSerie)
              .IsRequired()
              .HasDefaultValueSql("((1))");
      entity.Property(e => e.Descripcion).HasMaxLength(50);
      entity.Property(e => e.Fbaja)
              .HasColumnType("datetime")
              .HasColumnName("FBaja");
      entity.Property(e => e.Nlavados).HasColumnName("NLavados");

      entity.HasOne(d => d.IdDotacionNavigation).WithMany(p => p.MasterModelosDotacions)
              .HasForeignKey(d => d.IdDotacion)
              .HasConstraintName("FK_MasterModelosDotacion_MasterDotaciones");

      entity.HasOne(d => d.IdEtiquetaNavigation).WithMany(p => p.MasterModelosDotacions)
              .HasForeignKey(d => d.IdEtiqueta)
              .HasConstraintName("FK_MasterModelosDotacion_MasterEtiquetas");

      entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.MasterModelosDotacions)
              .HasForeignKey(d => d.IdModelo)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MasterModelosDotacion_MasterModeloPrenda");
      entity.ToTable(e =>
          {
          e.HasTrigger("MasterModelosDotacion_insert_trigger");
          e.HasTrigger("MasterModelosDotacion_update_trigger");
          e.HasTrigger("MasterModelosDotacion_delete_trigger");
        });
    });

    modelBuilder.Entity<MasterNivelAcceso>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterNivelAcceso_PK")
              .IsClustered(false);

      entity.ToTable("MasterNivelAcceso");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Descripcion)
              .HasMaxLength(50)
              .HasColumnName("descripcion");
    });

    modelBuilder.Entity<MasterPrecioTipoAntena>(entity =>
    {
      entity.ToTable("MasterPrecioTipoAntena");

      entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.MasterPrecioTipoAntenas)
              .HasForeignKey(d => d.IdCliente)
              .HasConstraintName("FK_MasterPrecioTipoAntena_MasterClientes");

      entity.HasOne(d => d.IdModeloPrendaNavigation).WithMany(p => p.MasterPrecioTipoAntenas)
              .HasForeignKey(d => d.IdModeloPrenda)
              .HasConstraintName("FK_MasterPrecioTipoAntena_MasterModeloPrenda");

      entity.HasOne(d => d.IdTipoAntenaNavigation).WithMany(p => p.MasterPrecioTipoAntenas)
              .HasForeignKey(d => d.IdTipoAntena)
              .HasConstraintName("FK_MasterPrecioTipoAntena_MasterTipoAntena");
    });

    modelBuilder.Entity<MasterPrendas>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterPrendas_PK")
              .IsClustered(false);

      entity.Property(e => e.Accesorio).HasDefaultValueSql("((0))");
      entity.Property(e => e.Borrado)
              .HasDefaultValueSql("((0))")
              .HasColumnName("borrado");
      entity.Property(e => e.CodigoTag)
              .HasMaxLength(50)
              .HasColumnName("codigoTAG");
      entity.Property(e => e.Color).HasMaxLength(50);
      entity.Property(e => e.DateLastMov).HasColumnType("datetime");
      entity.Property(e => e.Descrip)
              .HasMaxLength(100)
              .HasColumnName("descrip");
      entity.Property(e => e.Falta)
              .HasColumnType("datetime")
              .HasColumnName("FAlta");
      entity.Property(e => e.Fbaja)
              .HasColumnType("datetime")
              .HasColumnName("FBaja");
      entity.Property(e => e.FbajaReal)
              .HasColumnType("datetime")
              .HasColumnName("FBajaReal");
      entity.Property(e => e.IdCliente)
              .HasComment("SELECT [id], [nombre] FROM MasterClientes;")
              .HasColumnName("id_Cliente");
      entity.Property(e => e.IdEtiqueta).HasColumnName("id_etiqueta");
      entity.Property(e => e.IdModeloPrenda)
              .HasComment("SELECT [id], [descripcion] FROM MasterModeloPrenda;")
              .HasColumnName("id_ModeloPrenda");
      entity.Property(e => e.IdSubCliente)
              .HasComment("SELECT [id], [nombre] & [APELLIDO1] FROM MasterSubClientes;")
              .HasColumnName("id_subCliente");
      entity.Property(e => e.Nciclos)
              .HasDefaultValueSql("((0))")
              .HasColumnName("NCiclos");
      entity.Property(e => e.Nlavados)
              .HasDefaultValueSql("((0))")
              .HasColumnName("NLavados");
      entity.Property(e => e.NoActualizarModeloTalla)
              .IsRequired()
              .HasDefaultValueSql("((1))");
      entity.Property(e => e.Norden).HasColumnName("NOrden");
      entity.Property(e => e.Ntotal).HasColumnName("NTotal");
      entity.Property(e => e.Nueva).HasDefaultValueSql("((0))");
      entity.Property(e => e.Stock).HasColumnName("stock");
      entity.Property(e => e.Talla).HasMaxLength(50);
      entity.Property(e => e.Taquilla).HasMaxLength(50);

      entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.MasterPrenda)
              .HasForeignKey(d => d.IdCliente)
              .HasConstraintName("MasterPrendas_FK00");

      entity.HasOne(d => d.IdModeloPrendaNavigation).WithMany(p => p.MasterPrenda)
              .HasForeignKey(d => d.IdModeloPrenda)
              .HasConstraintName("MasterPrendas_FK01");

      entity.HasOne(d => d.IdSubClienteNavigation).WithMany(p => p.MasterPrenda)
              .HasForeignKey(d => d.IdSubCliente)
              .HasConstraintName("MasterPrendas_FK02");

      entity.HasOne(d => d.IdTipoRopaNavigation).WithMany(p => p.MasterPrenda)
              .HasForeignKey(d => d.IdTipoRopa)
              .HasConstraintName("FK_MasterPrendas_MasterTipoRopa");
    });

    modelBuilder.Entity<MasterPrendas>()
    .ToTable(tb =>
    {
      tb.HasTrigger("MasterPrendas_insert_trigger");
      tb.HasTrigger("MasterPrendas_update_trigger");
      tb.HasTrigger("MasterPrendas_delete_trigger");
    });

    modelBuilder.Entity<MasterProveedores>(entity =>
{
  entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterProveedores_PK")
              .IsClustered(false);

  entity.HasIndex(e => e.Id, "id");

  entity.HasIndex(e => e.IdContactoPrincipal, "id_ContactoPrincipal");

  entity.Property(e => e.Id).HasColumnName("id");
  entity.Property(e => e.Cif)
              .HasMaxLength(30)
              .HasColumnName("CIF");
  entity.Property(e => e.FechaAlta).HasColumnType("datetime");
  entity.Property(e => e.FechaBaja).HasColumnType("datetime");
  entity.Property(e => e.IdContactoPrincipal).HasColumnName("id_ContactoPrincipal");
  entity.Property(e => e.Iva).HasColumnName("IVA");
  entity.Property(e => e.Nombre).HasMaxLength(100);
  entity.Property(e => e.RazonSocial)
              .HasMaxLength(150)
              .HasColumnName("Razon_Social");
});

    modelBuilder.Entity<MasterProvincia>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterProvincias_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.Codigo, "codigo").IsUnique();

      entity.HasIndex(e => e.Id, "id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Codigo)
              .HasMaxLength(2)
              .HasColumnName("codigo");
      entity.Property(e => e.Mostrar)
              .HasDefaultValueSql("((0))")
              .HasColumnName("mostrar");
      entity.Property(e => e.Provincia)
              .HasMaxLength(50)
              .HasColumnName("provincia");
    });

    modelBuilder.Entity<MasterPuesto>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterPuestos_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.NombreMaquina, "NombreMaquina").IsUnique();

      entity.HasIndex(e => e.IdZona, "id_zona");

      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.Activo).HasDefaultValueSql("((0))");
      entity.Property(e => e.Clasificacion).HasColumnName("clasificacion");
      entity.Property(e => e.HotKey).HasMaxLength(10);
      entity.Property(e => e.IdZona).HasColumnName("id_zona");
      entity.Property(e => e.NombreMaquina).HasMaxLength(50);
    });

    modelBuilder.Entity<MasterRagoTag>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterRagoTags_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.Id, "Id");

      entity.Property(e => e.Desde).HasMaxLength(50);
      entity.Property(e => e.Hasta).HasMaxLength(50);
    });

    modelBuilder.Entity<MasterRangoCredito>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MasterRangoCredito");

      entity.Property(e => e.Descripcion)
              .HasMaxLength(20)
              .IsUnicode(false);
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<MasterServicio>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__MasterSe__3213E83F33786D78");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Descripcion)
              .HasMaxLength(255)
              .HasColumnName("DESCRIPCION");
      entity.Property(e => e.Gfh)
              .HasMaxLength(255)
              .HasColumnName("GFH");
    });

    modelBuilder.Entity<MasterSexo>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterSexo_PK")
              .IsClustered(false);

      entity.ToTable("MasterSexo");

      entity.HasIndex(e => e.Id, "id");

      entity.Property(e => e.Id)
              .ValueGeneratedNever()
              .HasColumnName("id");
      entity.Property(e => e.Sexo).HasMaxLength(50);
    });

    modelBuilder.Entity<MasterSubClientes>(entity =>
    { 
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterSubClientes_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.IdCliente, "MasterClientesMasterSubClientes");

      entity.HasIndex(e => e.IdDepartamentoCliente, "MasterDepartamentosClienteMasterSubClientes");

      entity.HasIndex(e => e.Nombre, "Name");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdDepartamentoCliente, "id_DepartamentoCliente");

      entity.HasIndex(e => e.IdEtiqueta, "id_etiqueta_defecto");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Apellido1).HasMaxLength(150);
      entity.Property(e => e.Apellido2).HasMaxLength(150);
      entity.Property(e => e.Borrado).HasDefaultValueSql("((0))");
      entity.Property(e => e.Campo1).HasMaxLength(50);
      entity.Property(e => e.Campo2).HasMaxLength(50);
      entity.Property(e => e.Categoria).HasMaxLength(200);
      entity.Property(e => e.CodigoQr)
              .HasMaxLength(50)
              .HasColumnName("CodigoQR");
      entity.Property(e => e.CodigoTag)
              .HasMaxLength(50)
              .HasColumnName("codigoTAG");
      entity.Property(e => e.Colectivo).HasMaxLength(20);
      entity.Property(e => e.Dni)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("DNI");
      entity.Property(e => e.Falta)
              .HasColumnType("datetime")
              .HasColumnName("FAlta");
      entity.Property(e => e.Fbaja)
              .HasColumnType("datetime")
              .HasColumnName("FBaja");
      entity.Property(e => e.Fnacimiento)
              .HasColumnType("datetime")
              .HasColumnName("FNacimiento");
      entity.Property(e => e.Foto).HasColumnType("image");
      entity.Property(e => e.IdCliente)
              .HasComment("SELECT [id], [nombre] FROM MasterClientes;")
              .HasColumnName("id_cliente");
      entity.Property(e => e.IdDepartamentoCliente)
              .HasComment("SELECT [id], [descripcion] FROM MasterDepartamentosCliente;")
              .HasColumnName("id_DepartamentoCliente");
      entity.Property(e => e.IdEmpleado)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.IdEtiqueta).HasColumnName("id_etiqueta");
      entity.Property(e => e.LocSopl).HasColumnName("LocSOPL");
      entity.Property(e => e.Nombre).HasMaxLength(100);
      entity.Property(e => e.Obser).HasMaxLength(250);
      entity.Property(e => e.Puesto).HasMaxLength(200);
      entity.Property(e => e.RopaPersonalizada)
              .IsRequired()
              .HasDefaultValueSql("((1))");
      entity.Property(e => e.Taquilla).HasMaxLength(150);
      entity.Property(e => e.Telefono)
              .HasMaxLength(10)
              .IsFixedLength()
              .HasColumnName("telefono");
      entity.Property(e => e.Turno).HasMaxLength(50);
      entity.Property(e => e.UpsizeTs)
              .IsRowVersion()
              .IsConcurrencyToken(false)
              .HasColumnName("upsize_ts");
      entity.Property(e => e.Vestuario).HasMaxLength(200);

      entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.MasterSubClientes)
              .HasForeignKey(d => d.IdCliente)
              .HasConstraintName("MasterSubClientes_FK00");

      entity.HasOne(d => d.IdDepartamentoClienteNavigation).WithMany(p => p.MasterSubClientes)
              .HasForeignKey(d => d.IdDepartamentoCliente)
              .HasConstraintName("MasterSubClientes_FK01");

      entity.ToTable(tb =>
              {
                tb.HasTrigger("MasterSubClientes_delete_trigger");
                tb.HasTrigger("MasterSubClientes_insert_trigger");
                tb.HasTrigger("MasterSubClientes_update_trigger");
              });
    });

    modelBuilder.Entity<MasterTaquilla>(entity =>
{
  entity.HasKey(e => e.Id);
  entity.ToTable("MasterTaquilla");

  entity.Property(e => e.Campo1).HasMaxLength(100);
  entity.Property(e => e.Campo2).HasMaxLength(100);
  entity.Property(e => e.DateLastMov).HasColumnType("datetime");
  entity.Property(e => e.Descripcion).HasMaxLength(50);
  entity.Property(e => e.FechaEntrega).HasColumnType("datetime");
  entity.Property(e => e.Id).ValueGeneratedOnAdd();
  entity.Property(e => e.IdVestuario).HasColumnName("Id_Vestuario");
  entity.Property(e => e.Observaciones).HasMaxLength(100);
});

    modelBuilder.Entity<MasterTipoAntena>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterTipoAntena_PK")
              .IsClustered(false);

      entity.ToTable("MasterTipoAntena");

      entity.Property(e => e.Id).ValueGeneratedNever();
      entity.Property(e => e.Descripcion)
              .HasMaxLength(50)
              .HasColumnName("descripcion");
      entity.Property(e => e.Facturable).HasDefaultValueSql("((0))");
      entity.Property(e => e.IdGrupo).HasDefaultValueSql("((-1))");
      entity.Property(e => e.Mostrar).HasDefaultValueSql("((1))");
      entity.Property(e => e.MostrarEnPda).HasColumnName("MostrarEnPDA");
      entity.ToTable(e =>
      {
        e.HasTrigger("MasterTipoAntena_insert_trigger");
        e.HasTrigger("MasterTipoAntena_update_trigger");
        e.HasTrigger("MasterTipoAntena_delete_trigger");
      });
    });

    modelBuilder.Entity<MasterTipoIncidencium>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterTipoIncidencia_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.IdCategoria, "MasterCategoriaIncidenciaMasterTipoIncidencia");

      entity.HasIndex(e => e.IdCategoria, "id_categoria");

      entity.Property(e => e.Id)
              .ValueGeneratedNever()
              .HasColumnName("id");
      entity.Property(e => e.Descripcion).HasMaxLength(100);
      entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
      entity.Property(e => e.System)
              .HasDefaultValueSql("((0))")
              .HasColumnName("system");

      entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.MasterTipoIncidencia)
              .HasForeignKey(d => d.IdCategoria)
              .HasConstraintName("MasterTipoIncidencia_FK00");
    });

    modelBuilder.Entity<MasterTipoMaquinaDispensadora>(entity =>
    {
      entity.ToTable("MasterTipoMaquinaDispensadora");

      entity.Property(e => e.Id)
              .ValueGeneratedOnAdd()
              .HasColumnName("id");
      entity.Property(e => e.Descripcion)
              .IsRequired()
              .HasMaxLength(50)
              .HasColumnName("descripcion");
    });

    modelBuilder.Entity<MasterTipoMarcadoPrenda>(entity =>
    {
      entity.HasKey(e => e.IdTpm);

      entity.Property(e => e.IdTpm).HasColumnName("Id_tpm");
      entity.Property(e => e.Descripcion)
              .IsRequired()
              .HasMaxLength(20)
              .IsFixedLength();
    });

    modelBuilder.Entity<MasterTipoMovimientoTaquilla>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MasterTipoMovimientoTaquilla");

      entity.Property(e => e.Descripcion)
              .HasMaxLength(30)
              .IsFixedLength();
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<MasterTipoMovimientosReposicione>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_t_clientesIncidencias");

      entity.Property(e => e.NombreMovimiento)
              .IsRequired()
              .HasMaxLength(100);
    });

    modelBuilder.Entity<MasterTipoPacto>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MasterTipoPacto");

      entity.Property(e => e.Descripcion)
              .IsRequired()
              .HasMaxLength(25);
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<MasterTipoPosicionSopl>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MasterTipoPosicionSOPL");

      entity.Property(e => e.Descripcion)
              .IsRequired()
              .HasMaxLength(20);
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<MasterTipoPrenda>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterTipoPrenda_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.Id, "id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Descripcion)
              .HasMaxLength(50)
              .HasColumnName("descripcion");
    });

    modelBuilder.Entity<LineaDePedidos>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("PK__LineaDeP__3214EC0711AB7E77")
              .IsClustered(false);

      entity.HasIndex(e => e.Id, "Id");

      entity.Property(e => e.Id).HasColumnName("Id");
      entity.Property(e => e.numeroPedido).HasColumnName("numeroPedido");
      entity.Property(e => e.numeroPaciente)
             .HasMaxLength(255)
             .HasColumnName("numeroPaciente");
      entity.Property(e => e.nombrePaciente)
             .HasMaxLength(50)
             .HasColumnName("nombrePaciente");
      entity.Property(e => e.cama)
             .HasMaxLength(50)
             .HasColumnName("cama");
      entity.Property(e => e.unidad)
             .HasMaxLength(50)
             .HasColumnName("unidad");
      entity.Property(e => e.codigoProducto).HasColumnName("codigoProducto");
      entity.Property(e => e.producto)
             .HasMaxLength(100)
             .HasColumnName("producto");
      entity.Property(e => e.cantidad).HasColumnName("cantidad");
      entity.Property(e => e.procesada)
              .HasDefaultValueSql("((0))")
              .HasColumnName("procesada");
      entity.Property(e => e.fechaProceso)
              .HasColumnType("datetime")
              .HasColumnName("fechaProceso");
      entity.Property(e => e.usuario)
             .HasMaxLength(50)
             .HasColumnName("usuario");
      entity.Property(e => e.nPreparacion).HasColumnName("nPreparacion");
      entity.Property(e => e.Descripcion)
             .HasMaxLength(255)
             .HasColumnName("Descripcion");
      entity.Property(e => e.numeroCarro)
             .HasMaxLength(50)
             .HasColumnName("numeroCarro");
    });

    modelBuilder.Entity<iPick>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("PK__iPick__3214EC0730D9287D")
              .IsClustered(false);

      entity.HasIndex(e => e.Id, "Id");

      entity.Property(e => e.Id).HasColumnName("Id");
      entity.Property(e => e.id_usuario).HasColumnName("id_usuario");
      entity.Property(e => e.producto).HasColumnName("producto");
      entity.Property(e => e.cantidad).HasColumnName("cantidad");
      entity.Property(e => e.fecha_validacion)
              .HasColumnType("datetime")
              .HasColumnName("fecha_validacion");
      entity.Property(e => e.nPreparacion).HasColumnName("nPreparacion");
      entity.Property(e => e.Devolucion)
              .HasDefaultValueSql("((0))")
              .HasColumnName("Devolucion");
    });

    modelBuilder.Entity<iPickUsuarioColor>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("PK__iPickUsu__3214EC0772BD9E3F")
              .IsClustered(false);

      entity.HasIndex(e => e.Id, "Id");

      entity.Property(e => e.Id).HasColumnName("Id");
      entity.Property(e => e.id_usuario).HasColumnName("id_usuario");
      entity.Property(e => e.nombre).HasColumnName("nombre");
      entity.Property(e => e.color).HasColumnName("color");
    });

    modelBuilder.Entity<iPickCarros>(entity =>
    {
      entity.HasKey(e => e.id)
        .HasName("PK__iPickCar__3213E83FF60E6881")
        .IsClustered(false);

      entity.HasIndex(e => e.id, "id");

      entity.Property(e => e.id).HasColumnName("id");
      entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
      entity.Property(e => e.EnUso).HasColumnName("EnUso");
    });

    modelBuilder.Entity<MasterTipoRopa>(entity =>
        {
          entity.HasKey(e => e.Id).HasName("PK__MasterTi__3214EC075507333A");

          entity.ToTable("MasterTipoRopa");

          entity.Property(e => e.Descripcion)
                  .IsRequired()
                  .HasMaxLength(25);
        });

    modelBuilder.Entity<MasterTipoTag>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterTipoTag_PK")
              .IsClustered(false);

      entity.ToTable("MasterTipoTag");

      entity.HasIndex(e => e.Id, "id");

      entity.Property(e => e.Id)
              .ValueGeneratedNever()
              .HasColumnName("id");
      entity.Property(e => e.Descripcion).HasMaxLength(50);
      entity.Property(e => e.System).HasDefaultValueSql("((0))");
    });

    modelBuilder.Entity<MasterTipoVestuario>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MasterTipoVestuario");

      entity.Property(e => e.Descripcion)
              .HasMaxLength(10)
              .IsFixedLength();
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<MasterTiposDeBaja>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MasterTiposDeBaja");

      entity.Property(e => e.Descripcion)
              .HasMaxLength(20)
              .IsUnicode(false);
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<MasterTiposDireccione>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterTiposDirecciones_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.Id, "id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Denominacion)
              .HasMaxLength(50)
              .HasColumnName("denominacion");
      entity.Property(e => e.System)
              .HasDefaultValueSql("((0))")
              .HasColumnName("system");
      entity.Property(e => e.Tipo)
              .HasComment("1=Domicilio Social,2=Correspondencia,3=Envio MercancÃ­a,4=Sucursal;5=Almacen (0=def por usuario)")
              .HasColumnName("tipo");
    });

    modelBuilder.Entity<MasterTiposUsuario>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterTiposUsuario_PK")
              .IsClustered(false);

      entity.ToTable("MasterTiposUsuario");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Descripcion).HasMaxLength(50);
      entity.Property(e => e.IdNivelAcceso).HasColumnName("id_NivelAcceso");

      entity.HasOne(d => d.IdNivelAccesoNavigation).WithMany(p => p.MasterTiposUsuarios)
              .HasForeignKey(d => d.IdNivelAcceso)
              .HasConstraintName("MasterTiposUsuario_FK00");
    });

    modelBuilder.Entity<MasterUbicacione>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Altura).HasMaxLength(50);
      entity.Property(e => e.Descripcion).HasMaxLength(50);
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
      entity.Property(e => e.Pasillo).HasMaxLength(50);
      entity.Property(e => e.Posicion)
              .IsRequired()
              .HasMaxLength(50);
    });

    modelBuilder.Entity<MasterUsuario>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterUsuarios_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdTipoUsuario, "id_tipo_usuario");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Activo).HasDefaultValueSql("((0))");
      entity.Property(e => e.Apellido1)
              .HasMaxLength(50)
              .HasColumnName("apellido1");
      entity.Property(e => e.Apellido2)
              .HasMaxLength(50)
              .HasColumnName("apellido2");
      entity.Property(e => e.CodigoTag)
              .HasMaxLength(50)
              .HasColumnName("CodigoTAG");
      entity.Property(e => e.IdTipoUsuario).HasColumnName("id_tipo_usuario");
      entity.Property(e => e.LavanderUserId).HasColumnName("LavanderUserID");
      entity.Property(e => e.Nombre)
              .HasMaxLength(50)
              .HasColumnName("nombre");
      entity.Property(e => e.Pedirporpantalla)
              .HasDefaultValueSql("((0))")
              .HasColumnName("pedirporpantalla");
      entity.Property(e => e.Pwd)
              .HasMaxLength(30)
              .HasColumnName("pwd");
      entity.Property(e => e.Usuario)
              .HasMaxLength(50)
              .HasComment("codigo de lector o de contraseÃ±a por si quiere no llevar chip")
              .HasColumnName("usuario");
      entity.ToTable(e =>
          {
            e.HasTrigger("MasterUsuario_insert_trigger");
            e.HasTrigger("MasterUsuario_update_trigger");
            e.HasTrigger("MasterUsuario_delete_trigger");
          });
    });

    modelBuilder.Entity<MasterVestuario>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MasterVestuario");

      entity.Property(e => e.Descripcion).HasMaxLength(10);
      entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<MasterViasPublica>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterViasPublicas_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.Id, "id");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Abreviatura)
              .HasMaxLength(2)
              .HasColumnName("abreviatura");
      entity.Property(e => e.Descripcion)
              .HasMaxLength(15)
              .HasColumnName("descripcion");
      entity.Property(e => e.System)
              .HasDefaultValueSql("((0))")
              .HasColumnName("system");
    });

    modelBuilder.Entity<MasterZona>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMasterZonas_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.IdZonaPadre, "MasterZonasMasterZonas");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdZonaPadre, "id_ZonaPadre");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Descripcion).HasMaxLength(50);
      entity.Property(e => e.IdZonaPadre).HasColumnName("id_ZonaPadre");
      entity.Property(e => e.Orden).HasColumnName("orden");
      entity.Property(e => e.System)
              .HasDefaultValueSql("((0))")
              .HasColumnName("system");

      entity.HasOne(d => d.IdZonaPadreNavigation).WithMany(p => p.InverseIdZonaPadreNavigation)
              .HasForeignKey(d => d.IdZonaPadre)
              .HasConstraintName("MasterZonas_FK00");
    });

    modelBuilder.Entity<MovAntenasPuesto>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMovAntenasPuesto_PK")
              .IsClustered(false);

      entity.ToTable("MovAntenasPuesto");

      entity.HasIndex(e => e.IdPuesto, "MasterPuestosMovAntenasPuesto");

      entity.HasIndex(e => e.IdTipoAntena, "MasterTipoAntenaMovAntenasPuesto");

      entity.HasIndex(e => e.IdZona, "MasterZonasMovAntenasPuesto");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdPuesto, "id_Puesto");

      entity.HasIndex(e => e.IdTipoAntena, "id_TipoAntena");

      entity.HasIndex(e => e.IdUltimoTipoAntena, "id_TipoAntena1");

      entity.HasIndex(e => e.IdZona, "id_Zona");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Com)
              .HasMaxLength(50)
              .HasColumnName("com");
      entity.Property(e => e.IdPuesto).HasColumnName("id_Puesto");
      entity.Property(e => e.IdTipoAntena).HasColumnName("id_TipoAntena");
      entity.Property(e => e.IdUltimoTipoAntena).HasColumnName("id_UltimoTipoAntena");
      entity.Property(e => e.IdZona).HasColumnName("id_Zona");
      entity.Property(e => e.Matricial).HasDefaultValueSql("((0))");
      entity.Property(e => e.Modelo).HasColumnName("modelo");
      entity.Property(e => e.NombreImpresoraDirecta).HasMaxLength(50);
      entity.Property(e => e.PrintLabel).HasDefaultValueSql("((0))");
      entity.Property(e => e.PrintPren).HasDefaultValueSql("((0))");
      entity.Property(e => e.ReadSound).HasDefaultValueSql("((0))");

      entity.HasOne(d => d.IdPuestoNavigation).WithMany(p => p.MovAntenasPuestos)
              .HasForeignKey(d => d.IdPuesto)
              .HasConstraintName("MovAntenasPuesto_FK00");

      entity.HasOne(d => d.IdTipoAntenaNavigation).WithMany(p => p.MovAntenasPuestos)
              .HasForeignKey(d => d.IdTipoAntena)
              .HasConstraintName("MovAntenasPuesto_FK01");

      entity.HasOne(d => d.IdZonaNavigation).WithMany(p => p.MovAntenasPuestos)
              .HasForeignKey(d => d.IdZona)
              .HasConstraintName("MovAntenasPuesto_FK02");

      entity.HasOne(d => d.ModeloNavigation).WithMany(p => p.MovAntenasPuestos)
              .HasForeignKey(d => d.Modelo)
              .HasConstraintName("MovAntenasPuesto_FK03");
    });

    modelBuilder.Entity<MovCambioAntena>(entity =>
    {
      entity.HasKey(e => e.CodigoTag)
              .HasName("aaaaaMovCambioAntena_PK")
              .IsClustered(false);

      entity.ToTable("MovCambioAntena");

      entity.HasIndex(e => e.IdTipoantena, "MasterTipoAntenaMovCambioAntena");

      entity.HasIndex(e => e.IdTipoTag, "MasterTipoTagMovCambioAntena");

      entity.HasIndex(e => e.IdTipoTag, "id_TipoTag");

      entity.HasIndex(e => e.IdTipoantena, "id_tipoantena");

      entity.Property(e => e.CodigoTag).HasMaxLength(50);
      entity.Property(e => e.IdTipoTag)
              .HasComment("SELECT [id], [descripcion] FROM MasterTipoTag;")
              .HasColumnName("id_TipoTag");
      entity.Property(e => e.IdTipoantena)
              .HasComment("SELECT MasterTipoAntena.descripcion, MasterTipoAntena.Id FROM MasterTipoAntena;")
              .HasColumnName("id_tipoantena");

      entity.HasOne(d => d.IdTipoTagNavigation).WithMany(p => p.MovCambioAntenas)
              .HasForeignKey(d => d.IdTipoTag)
              .HasConstraintName("MovCambioAntena_FK01");

      entity.HasOne(d => d.IdTipoantenaNavigation).WithMany(p => p.MovCambioAntenas)
              .HasForeignKey(d => d.IdTipoantena)
              .HasConstraintName("MovCambioAntena_FK00");
    });

    modelBuilder.Entity<MovCargasDescargasMaquina>(entity =>
    {
      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.FechaHora)
              .HasColumnType("datetime")
              .HasColumnName("fechaHora");
      entity.Property(e => e.IdMaquina).HasColumnName("id_Maquina");
      entity.Property(e => e.IdUsuario).HasColumnName("id_Usuario");
      entity.Property(e => e.TipoMov).HasColumnName("tipoMov");

      entity.HasOne(d => d.IdMaquinaNavigation).WithMany(p => p.MovCargasDescargasMaquinas)
              .HasForeignKey(d => d.IdMaquina)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MovCargasDescargasMaquinas_MasterMaquinaDispensacion");

      entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.MovCargasDescargasMaquinas)
              .HasForeignKey(d => d.IdUsuario)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MovCargasDescargasMaquinas_MasterUsuarios");
    });

    modelBuilder.Entity<MovClienteCamposControl>()
            .ToTable("MovClienteCamposControl")
            .HasKey(m => new { m.CampoControl, m.IdCliente });

    modelBuilder.Entity<MovComandosAntena>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMovComandosAntena_PK")
              .IsClustered(false);

      entity.ToTable("MovComandosAntena");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdAntena, "id_antena");

      entity.HasIndex(e => e.IdComando, "id_comando");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.CodigoTag)
              .HasMaxLength(50)
              .HasColumnName("codigoTAG");
      entity.Property(e => e.IdAntena).HasColumnName("id_antena");
      entity.Property(e => e.IdComando).HasColumnName("id_comando");

      entity.HasOne(d => d.IdComandoNavigation).WithMany(p => p.MovComandosAntenas)
              .HasForeignKey(d => d.IdComando)
              .HasConstraintName("MovComandosAntena_FK00");
    });

    modelBuilder.Entity<MovCreditosDepartamentos>(entity =>
    {
      entity.HasKey(e => e.Id).IsClustered(false);

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
      entity.Property(e => e.RangoCredito).HasDefaultValueSql("((1))");

      entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.MovCreditosDepartamentos)
              .HasForeignKey(d => d.IdDepartamento)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MovCreditosDepartamentos_MasterDepartamentosCliente");
      entity.ToTable(e =>
          {
          e.HasTrigger("MovCreditosDepartamentos_insert_trigger");
          e.HasTrigger("MovCreditosDepartamentos_update_trigger");
          e.HasTrigger("MovCreditosDepartamentos_delete_trigger");
        });
    });

    modelBuilder.Entity<MovCreditosModelosDepartamento>(entity =>
    {
      entity.HasKey(m => new { m.IdDepartamento, m.IdModelo, m.TipoRango }); ;

      entity.HasOne(d => d.IdDepartamentoNavigation).WithMany()
            .HasForeignKey(d => d.IdDepartamento)
            .HasConstraintName("FK_MovCreditosModelosDepartamentos_MasterDepartamentosCliente");

      entity.HasOne(d => d.IdModeloNavigation).WithMany()
              .HasForeignKey(d => d.IdModelo)
              .HasConstraintName("FK_MovCreditosModelosDepartamentos_MasterModeloPrenda");
    });

    modelBuilder.Entity<MovDepartamentosDotacione>(entity =>
    {
      entity.HasNoKey();

      entity.HasOne(d => d.IdDepartamentoNavigation).WithMany()
              .HasForeignKey(d => d.IdDepartamento)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MovDepartamentosDotaciones_MasterDepartamentosCliente");

      entity.HasOne(d => d.IdDotacionNavigation).WithMany()
              .HasForeignKey(d => d.IdDotacion)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MovDepartamentosDotaciones_MasterDotaciones");
    });

    modelBuilder.Entity<MovDireccione>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMovDirecciones_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.IdProvincia, "MasterProvinciasMovDirecciones");

      entity.HasIndex(e => e.IdTipoDireccion, "MasterTiposDireccionesMovDirecciones");

      entity.HasIndex(e => e.IdViapublica, "MasterViasPublicasMovDirecciones");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdTipoDireccion, "id_FormaContacto");

      entity.HasIndex(e => e.Localidad, "id_localidad");

      entity.HasIndex(e => e.IdProvincia, "id_provincia");

      entity.HasIndex(e => e.IdViapublica, "id_viapublica");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Calle)
              .HasMaxLength(100)
              .HasColumnName("calle");
      entity.Property(e => e.Cp)
              .HasMaxLength(12)
              .HasColumnName("cp");
      entity.Property(e => e.Descripcion)
              .HasMaxLength(50)
              .HasColumnName("descripcion");
      entity.Property(e => e.Edificio)
              .HasMaxLength(20)
              .HasColumnName("edificio");
      entity.Property(e => e.Escalera)
              .HasMaxLength(10)
              .HasColumnName("escalera");
      entity.Property(e => e.IdProvincia).HasColumnName("id_provincia");
      entity.Property(e => e.IdTipoDireccion).HasColumnName("id_TipoDireccion");
      entity.Property(e => e.IdViapublica).HasColumnName("id_viapublica");
      entity.Property(e => e.Localidad).HasMaxLength(100);
      entity.Property(e => e.Piso)
              .HasMaxLength(10)
              .HasColumnName("piso");
      entity.Property(e => e.Portal)
              .HasMaxLength(10)
              .HasColumnName("portal");
      entity.Property(e => e.Puerta)
              .HasMaxLength(10)
              .HasColumnName("puerta");
      entity.Property(e => e.RestoDireccion)
              .HasMaxLength(200)
              .HasColumnName("resto_direccion");

      entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.MovDirecciones)
              .HasForeignKey(d => d.IdProvincia)
              .HasConstraintName("MovDirecciones_FK00");

      entity.HasOne(d => d.IdTipoDireccionNavigation).WithMany(p => p.MovDirecciones)
              .HasForeignKey(d => d.IdTipoDireccion)
              .HasConstraintName("MovDirecciones_FK01");

      entity.HasOne(d => d.IdViapublicaNavigation).WithMany(p => p.MovDirecciones)
              .HasForeignKey(d => d.IdViapublica)
              .HasConstraintName("MovDirecciones_FK02");
    });

    modelBuilder.Entity<MovDireccionesCliente>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMovDireccionesClientes_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.IdCliente, "MasterClientesmovDireccionesClientes");

      entity.HasIndex(e => e.IdDireccion, "MovDireccionesmovDireccionesClientes");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdCliente, "id_Cliente");

      entity.HasIndex(e => e.IdDireccion, "id_direccion");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdCliente).HasColumnName("id_Cliente");
      entity.Property(e => e.IdDireccion).HasColumnName("id_direccion");

      entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.MovDireccionesClientes)
              .HasForeignKey(d => d.IdCliente)
              .HasConstraintName("MovDireccionesClientes_FK00");

      entity.HasOne(d => d.IdDireccionNavigation).WithMany(p => p.MovDireccionesClientes)
              .HasForeignKey(d => d.IdDireccion)
              .HasConstraintName("MovDireccionesClientes_FK01");
    });

    modelBuilder.Entity<MovDireccionesPropio>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMovDireccionesPropios_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.IdPropio, "DatosPropiosMovDireccionesPropios");

      entity.HasIndex(e => e.IdDireccion, "MovDireccionesMovDireccionesPropios");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdPropio, "id_Cliente");

      entity.HasIndex(e => e.IdDireccion, "id_direccion");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdDireccion).HasColumnName("id_direccion");
      entity.Property(e => e.IdPropio).HasColumnName("id_Propio");

      entity.HasOne(d => d.IdDireccionNavigation).WithMany(p => p.MovDireccionesPropios)
              .HasForeignKey(d => d.IdDireccion)
              .HasConstraintName("MovDireccionesPropios_FK01");

      entity.HasOne(d => d.IdPropioNavigation).WithMany(p => p.MovDireccionesPropios)
              .HasForeignKey(d => d.IdPropio)
              .HasConstraintName("MovDireccionesPropios_FK00");
    });

    modelBuilder.Entity<MovDireccionesProveedore>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMovDireccionesProveedores_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.IdProveedor, "MasterProveedoresmovDireccionesProveedores");

      entity.HasIndex(e => e.IdDireccion, "MovDireccionesMovDireccionesProveedores");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdProveedor, "id_Cliente");

      entity.HasIndex(e => e.IdDireccion, "id_direccion");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdDireccion).HasColumnName("id_direccion");
      entity.Property(e => e.IdProveedor).HasColumnName("id_Proveedor");

      entity.HasOne(d => d.IdDireccionNavigation).WithMany(p => p.MovDireccionesProveedores)
              .HasForeignKey(d => d.IdDireccion)
              .HasConstraintName("MovDireccionesProveedores_FK01");

      entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.MovDireccionesProveedores)
              .HasForeignKey(d => d.IdProveedor)
              .HasConstraintName("MovDireccionesProveedores_FK00");
    });

    modelBuilder.Entity<MovEtiModCli>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMovEtiModCli_PK")
              .IsClustered(false);

      entity.ToTable("MovEtiModCli");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdCli, "id_Cli");

      entity.HasIndex(e => e.IdEti, "id_Eti");

      entity.HasIndex(e => e.IdMod, "id_Mod");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdCli).HasColumnName("id_Cli");
      entity.Property(e => e.IdEti).HasColumnName("id_Eti");
      entity.Property(e => e.IdMod).HasColumnName("id_Mod");
    });

    modelBuilder.Entity<MovEtiquetasCliente>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMovEtiquetasCliente_PK")
              .IsClustered(false);

      entity.ToTable("MovEtiquetasCliente");

      entity.HasIndex(e => e.IdCliente, "MasterClientesMovEtiquetasCliente");

      entity.HasIndex(e => e.IdEtiqueta, "MasterEtiquetasMovEtiquetasCliente");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdCliente, "id_FormaContacto");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdCliente).HasColumnName("id_Cliente");
      entity.Property(e => e.IdEtiqueta).HasColumnName("id_Etiqueta");

      entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.MovEtiquetasClientes)
              .HasForeignKey(d => d.IdCliente)
              .HasConstraintName("MovEtiquetasCliente_FK00");

      entity.HasOne(d => d.IdEtiquetaNavigation).WithMany(p => p.MovEtiquetasClientes)
              .HasForeignKey(d => d.IdEtiqueta)
              .HasConstraintName("MovEtiquetasCliente_FK01");
    });

    modelBuilder.Entity<MovHisPren>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__MovHisPr__3213E83FFA5A440C");

      entity.ToTable("MovHisPren");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.FechaMov).HasColumnType("datetime");
      entity.Property(e => e.IdSubCliNew).HasColumnName("idSubCliNew");
      entity.Property(e => e.IdSubCliOld).HasColumnName("idSubCliOld");
    });

    modelBuilder.Entity<MovHistoricoEstadoExternoTaquilla>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.DescripcionEstadoExterno)
              .IsRequired()
              .HasMaxLength(50);
      entity.Property(e => e.Fecha).HasColumnType("datetime");
    });

    modelBuilder.Entity<MovHistoricoGestionTaquilla>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Mov_HistoricoGestionTaquillas");

      entity.Property(e => e.Fecha).HasColumnType("date");
      entity.Property(e => e.IdEstado).HasColumnName("idEstado");
      entity.Property(e => e.IdSubCliente).HasColumnName("idSubCliente");
      entity.Property(e => e.IdTaquilla).HasColumnName("idTaquilla");

      entity.HasOne(d => d.IdSubClienteNavigation).WithMany()
              .HasForeignKey(d => d.IdSubCliente)
              .HasConstraintName("FK_Mov_HistoricoGestionTaquillas_MasterSubClientes");
    });

    modelBuilder.Entity<MovHistoricosFechasSubCliente>(entity =>
    {
      entity.Property(e => e.Id).HasColumnName("ID");
      entity.Property(e => e.FechaAlta).HasColumnType("datetime");
      entity.Property(e => e.FechaBaja).HasColumnType("datetime");
      entity.Property(e => e.FechaModificacion)
              .HasDefaultValueSql("(getutcdate())")
              .HasColumnType("datetime");
      entity.Property(e => e.IdsubCliente).HasColumnName("IDSubCliente");
      entity.Property(e => e.TipoBaja)
              .HasMaxLength(20)
              .IsUnicode(false);

      entity.HasOne(d => d.IdsubClienteNavigation).WithMany(p => p.MovHistoricosFechasSubClientes)
              .HasForeignKey(d => d.IdsubCliente)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MovHistoricosFechasSubClientes_MasterSubClientes");
    });

    modelBuilder.Entity<MovMaquinaDisAlmacenes>(entity =>
    {
      entity.HasIndex(e => new { e.IdAlmacen, e.IdMaquinaDispensacion }, "IX_MovMaquinaDisAlmacenes").IsUnique();

      entity.HasIndex(e => e.IdMaquinaDispensacion, "idx_MovMaquinaDisAlmacenes_cantMaxIdModelo");

      entity.Property(e => e.CantMax).HasDefaultValueSql("((-1))");
      entity.Property(e => e.CantMin).HasColumnName("cantMin");
      entity.Property(e => e.CodigoTag).HasMaxLength(50);
      entity.Property(e => e.PuertoPlccerradura)
              .IsRequired()
              .HasMaxLength(20)
              .IsUnicode(false)
              .HasDefaultValueSql("('')")
              .HasColumnName("PuertoPLCCerradura");
      entity.Property(e => e.PuertoPuertaAbierta)
              .IsRequired()
              .HasMaxLength(20)
              .IsUnicode(false)
              .HasDefaultValueSql("('')");
      entity.Property(e => e.Talla).HasMaxLength(50);

      entity.HasOne(d => d.IdMaquinaDispensacionNavigation).WithMany(p => p.MovMaquinaDisAlmacenes)
              .HasForeignKey(d => d.IdMaquinaDispensacion)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MovMaquinaDisAlmacenes_MasterMaquinaDispensacion");
    });

    modelBuilder.Entity<MovMaquinaDisAlmacenes>()
        .ToTable(tb =>
        {
          tb.HasTrigger("MovMaquinaDisAlmacenes_insert_trigger");
          tb.HasTrigger("MovMaquinaDisAlmacenes_update_trigger");
          tb.HasTrigger("MovMaquinaDisAlmacenes_delete_trigger");
        });

    modelBuilder.Entity<MovMaquinaDisReposicione>(entity =>
    {
      entity.Property(e => e.Fecha)
              .HasDefaultValueSql("(getdate())")
              .HasColumnType("datetime");
      entity.Property(e => e.IdAlmacen).HasColumnName("idAlmacen");
      entity.Property(e => e.Talla).HasMaxLength(10);

      entity.HasOne(d => d.IdMaqDisAlmNavigation).WithMany(p => p.MovMaquinaDisReposiciones)
              .HasForeignKey(d => d.IdMaqDisAlm)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MovMaquinaDisReposiciones_MovMaquinaDisAlmacenes");

      entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.MovMaquinaDisReposiciones)
              .HasForeignKey(d => d.IdModelo)
              .HasConstraintName("FK_MovMaquinaDisReposiciones_MasterModeloPrenda");
    });

    modelBuilder.Entity<MovMaquinaDisReposicione>()
        .ToTable(tb =>
        {
          tb.HasTrigger("MovMaquinaDisReposiciones_insert_trigger");
          tb.HasTrigger("MovMaquinaDisReposiciones_update_trigger");
          tb.HasTrigger("MovMaquinaDisReposiciones_delete_trigger");
        });

    modelBuilder.Entity<MovMaquinaModeloReferenciaAlbarane>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Referencia)
              .IsRequired()
              .HasMaxLength(30)
              .IsUnicode(false);

      entity.HasOne(d => d.IdMaquinaNavigation).WithMany()
              .HasForeignKey(d => d.IdMaquina)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MovMaquinaModeloReferenciaAlbaranes_MasterMaquinaDispensacion");

      entity.HasOne(d => d.IdModeloNavigation).WithMany()
              .HasForeignKey(d => d.IdModelo)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MovMaquinaModeloReferenciaAlbaranes_MasterModeloPrenda");
    });

    modelBuilder.Entity<MovMaquinasIncidencia>(entity =>
    {

      entity.Property(e => e.Descripcion)
              .HasMaxLength(200)
              .IsFixedLength();
      entity.Property(e => e.Fecha).HasColumnType("datetime");
      entity.Property(e => e.IdAlmacen).HasColumnName("idAlmacen");
      entity.Property(e => e.IdIncidencia).HasColumnName("idIncidencia");
      entity.Property(e => e.IdMaquina).HasColumnName("idMaquina");
    });

    modelBuilder.Entity<MovMaquinasPuerta>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.FechaHoraDispensacion)
              .HasColumnType("datetime")
              .HasColumnName("Fecha_Hora_Dispensacion");
      entity.Property(e => e.FechaHoraPeticion)
              .HasColumnType("datetime")
              .HasColumnName("Fecha_Hora_Peticion");
      entity.Property(e => e.Id)
              .ValueGeneratedOnAdd()
              .HasColumnName("id");
      entity.Property(e => e.Npuerta).HasColumnName("NPuerta");
      entity.Property(e => e.Nreintentos).HasColumnName("NReintentos");
    });

    modelBuilder.Entity<MovModCli>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMovModCli_PK")
              .IsClustered(false);

      entity.ToTable("MovModCli");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdCli, "id_Cli");

      entity.HasIndex(e => e.IdMod, "id_Mod");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdCli).HasColumnName("id_Cli");
      entity.Property(e => e.IdMod).HasColumnName("id_Mod");
      entity.ToTable(e =>
          {
          e.HasTrigger("MovModCli_insert_trigger");
          e.HasTrigger("MovModCli_update_trigger");
          e.HasTrigger("MovModCli_delete_trigger");
        });
    });    

    modelBuilder.Entity<MovModSubCliente>(entity =>
    {
      entity.ToTable("MovModSubCliente");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdModeloPrenda).HasColumnName("id_ModeloPrenda");
      entity.Property(e => e.IdSubCliente).HasColumnName("id_SubCliente");

      entity.HasOne(d => d.IdModeloPrendaNavigation).WithMany(p => p.MovModSubClientes)
              .HasForeignKey(d => d.IdModeloPrenda)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MovModSubCliente_MasterModeloPrenda");

      entity.HasOne(d => d.IdSubClienteNavigation).WithMany(p => p.MovModSubClientes)
              .HasForeignKey(d => d.IdSubCliente)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MovModSubCliente_MasterSubClientes");

      entity.ToTable(e =>
      {
        e.HasTrigger("MovModSubCliente_delete_trigger");
        e.HasTrigger("MovModSubCliente_insert_trigger");
        e.HasTrigger("MovModSubCliente_update_trigger");        
      });
    });

    modelBuilder.Entity<MovModeloTallaSub>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMovModeloTallaSub_PK")
              .IsClustered(false);

      entity.ToTable("MovModeloTallaSub");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdSubCli, "idSubCli");

      entity.HasIndex(e => e.Idmod, "idmod");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdSubCli).HasColumnName("idSubCli");
      entity.Property(e => e.Idmod).HasColumnName("idmod");
      entity.Property(e => e.Talla)
              .HasMaxLength(50)
              .HasColumnName("talla");

      entity.HasOne(d => d.IdSubCliNavigation).WithMany(p => p.MovModeloTallaSubs)
              .HasForeignKey(d => d.IdSubCli)
              .HasConstraintName("MovModeloTallaSub_FK01");

      entity.HasOne(d => d.IdmodNavigation).WithMany(p => p.MovModeloTallaSubs)
              .HasForeignKey(d => d.Idmod)
              .HasConstraintName("MovModeloTallaSub_FK00");

      entity.ToTable(e =>
      {
        e.HasTrigger("MovModeloTallaSub_delete_trigger");
        e.HasTrigger("MovModeloTallaSub_insert_trigger");
        e.HasTrigger("MovModeloTallaSub_update_trigger");
      });
    });

    modelBuilder.Entity<MovMostrarAntenasPuesto>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MovMostrarAntenasPuesto");

      entity.Property(e => e.IdPuesto).HasColumnName("idPuesto");
      entity.Property(e => e.IdTipoAntena).HasColumnName("idTipoAntena");

      entity.HasOne(d => d.IdPuestoNavigation).WithMany()
              .HasForeignKey(d => d.IdPuesto)
              .HasConstraintName("FK_MovMostrarAntenasPuesto_MasterPuestos");

      entity.HasOne(d => d.IdTipoAntenaNavigation).WithMany()
              .HasForeignKey(d => d.IdTipoAntena)
              .HasConstraintName("FK_MovMostrarAntenasPuesto_MasterTipoAntena");
    });

    modelBuilder.Entity<MovPlcli>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__MovPLCli__3213E83F5E177401");

      entity.ToTable("MovPLCli");

      entity.HasIndex(e => e.IdCli, "Id_cli");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdCli).HasColumnName("Id_cli");
    });

    modelBuilder.Entity<MovPren>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMovPren_PK")
              .IsClustered(false);

      entity.ToTable("MovPren");

      entity.HasIndex(e => new { e.IdPrenda, e.IdTipoAntena }, "IDX_Prenda_TipoAntena");

      entity.HasIndex(e => e.IdPrenda, "Id_Prenda");

      entity.HasIndex(e => e.IdCli, "idCli");

      entity.HasIndex(e => e.IdSubCli, "idSubCli");

      entity.HasIndex(e => e.IdOperario, "id_Operario");

      entity.HasIndex(e => e.IdPuesto, "id_Puesto");

      entity.HasIndex(e => e.IdTipoAntena, "id_TipoAntena");

      entity.HasIndex(e => e.IdMov, "id_exter");

      entity.Property(e => e.Fecha).HasColumnType("datetime");
      entity.Property(e => e.Hora).HasColumnType("datetime");
      entity.Property(e => e.IdCli).HasColumnName("idCli");
      entity.Property(e => e.IdModeloPrenda).HasColumnName("idModeloPrenda");
      entity.Property(e => e.IdMov).HasColumnName("id_mov");
      entity.Property(e => e.IdOperario).HasColumnName("id_Operario");
      entity.Property(e => e.IdPrenda).HasColumnName("Id_Prenda");
      entity.Property(e => e.IdPuesto).HasColumnName("id_Puesto");
      entity.Property(e => e.IdSubCli).HasColumnName("idSubCli");
      entity.Property(e => e.IdTipoAntena).HasColumnName("id_TipoAntena");
      entity.Property(e => e.Obser).HasMaxLength(250);
      entity.Property(e => e.Talla)
              .HasMaxLength(50)
              .HasColumnName("talla");

      entity.HasOne(d => d.IdTipoAntenaNavigation).WithMany(p => p.MovPrens)
              .HasForeignKey(d => d.IdTipoAntena)
              .HasConstraintName("FK_MovPren_MasterTipoAntena");
      entity.ToTable(e =>
          {
          e.HasTrigger("MovPren_insert_trigger");
          e.HasTrigger("MovPren_update_trigger");
          e.HasTrigger("MovPren_delete_trigger");
        });
    });

    modelBuilder.Entity<MovSimbolizadoModeloTallaFord>(entity =>
    {
      entity.HasKey(e => e.Cod_articulo)
              .HasName("PK__MovSimbo__1946348719982561")
              .IsClustered(false);

      entity.ToTable("MovSimbolizadoModeloTallaFord");

      entity.Property(e => e.Cod_articulo).HasColumnName("cod_articulo");
      entity.Property(e => e.Descripcion).HasColumnName("descripcion");
      entity.Property(e => e.Talla).HasColumnName("talla");
      entity.Property(e => e.IdModelo).HasColumnName("idModelo");
    });

    modelBuilder.Entity<MovReposicione>(entity =>
    {
      entity.HasKey(e => e.IdRep).HasName("PK_MovReposiciones_1");

      entity.HasIndex(e => new { e.PuntoRep, e.ModeloRep, e.FechaRep }, "IDX_MovReposiciones_PuntoModeloFecha");

      entity.HasIndex(e => e.PuntoRep, "IdxPunto");

      entity.Property(e => e.IdRep)
              .HasDefaultValueSql("(newsequentialid())")
              .HasColumnName("Id_rep");
      entity.Property(e => e.AlmacenRep).HasColumnName("Almacen_rep");
      entity.Property(e => e.CantidadRep).HasColumnName("Cantidad_rep");
      entity.Property(e => e.ContadasRep).HasColumnName("Contadas_rep");
      entity.Property(e => e.CreationDate)
              .HasDefaultValueSql("(getutcdate())")
              .HasColumnType("datetime");
      entity.Property(e => e.FechaEntregaRep)
              .HasColumnType("datetime")
              .HasColumnName("FechaEntrega_rep");
      entity.Property(e => e.FechaRep)
              .HasColumnType("datetime")
              .HasColumnName("Fecha_rep");
      entity.Property(e => e.LastEditDate)
              .HasDefaultValueSql("(getutcdate())")
              .HasColumnType("datetime");
      entity.Property(e => e.ModeloRep).HasColumnName("Modelo_rep");
      entity.Property(e => e.ObservacionesRep)
              .IsRequired()
              .HasMaxLength(150)
              .HasDefaultValueSql("(N'')")
              .HasColumnName("observaciones_rep");
      entity.Property(e => e.PactoRep).HasColumnName("Pacto_rep");
      entity.Property(e => e.PuestoRep).HasColumnName("puesto_rep");
      entity.Property(e => e.PuntoRep).HasColumnName("Punto_rep");
      entity.Property(e => e.RepuestoRep).HasColumnName("Repuesto_rep");
      entity.Property(e => e.SolicitadasRep).HasColumnName("solicitadas_rep");
      entity.Property(e => e.TipoMovRep).HasColumnName("TipoMov_rep");
      entity.Property(e => e.TipoReposicionRep).HasColumnName("TipoReposicion_rep");
      entity.Property(e => e.UsuarioRep).HasColumnName("usuario_rep");
      entity.ToTable(e =>
          {
          e.HasTrigger("MovReposicione_insert_trigger");
          e.HasTrigger("MovReposicione_update_trigger");
          e.HasTrigger("MovReposicione_delete_trigger");
        });
    });

    modelBuilder.Entity<MovSopl>(entity =>
    {
      entity.ToTable("MovSOPL");

      entity.Property(e => e.Id)
              .HasMaxLength(10)
              .IsFixedLength();
      entity.Property(e => e.ColPlc).HasColumnName("ColPLC");
      entity.Property(e => e.DateLastMov).HasColumnType("datetime");
      entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");
      entity.Property(e => e.IdPuesto).HasColumnName("id_Puesto");
      entity.Property(e => e.IdSubCliente).HasColumnName("ID_SubCliente");
      entity.Property(e => e.RowPlc).HasColumnName("RowPLC");
      entity.Property(e => e.TipoPosicionSopl).HasColumnName("TipoPosicionSOPL");
    });

    modelBuilder.Entity<MovSopl2>(entity =>
    {
      entity.ToTable("MovSOPL2");

      entity.Property(e => e.Id)
              .HasMaxLength(10)
              .IsFixedLength();
      entity.Property(e => e.ColPlc).HasColumnName("ColPLC");
      entity.Property(e => e.DateLastMov).HasColumnType("datetime");
      entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");
      entity.Property(e => e.IdPuesto).HasColumnName("id_Puesto");
      entity.Property(e => e.IdSubCliente).HasColumnName("ID_SubCliente");
      entity.Property(e => e.RowPlc).HasColumnName("RowPLC");
    });

    modelBuilder.Entity<MovStockPrendasUsu>(entity =>
    {
      entity.ToTable("MovStockPrendasUsu");

      entity.HasIndex(e => new { e.IdSubCliente, e.IdModelo, e.Talla }, "IX_MovStockPrendasUsu").IsUnique();

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdModelo).HasColumnName("idModelo");
      entity.Property(e => e.IdSubCliente).HasColumnName("idSubCliente");
      entity.Property(e => e.Talla).HasMaxLength(50);

      entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.MovStockPrendasUsus)
              .HasForeignKey(d => d.IdModelo)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MovStockPrendasUsu_MasterModeloPrenda");

      entity.HasOne(d => d.IdSubClienteNavigation).WithMany(p => p.MovStockPrendasUsus)
              .HasForeignKey(d => d.IdSubCliente)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MovStockPrendasUsu_MasterSubClientes");
      entity.ToTable(e =>
          {
          e.HasTrigger("MovStockPrendasUsu_insert_trigger");
          e.HasTrigger("MovStockPrendasUsu_update_trigger");
          e.HasTrigger("MovStockPrendasUsu_delete_trigger");
        });
    });

    modelBuilder.Entity<MovStockPrendasUsuHistorico>(entity =>
    {
      entity.ToTable("MovStockPrendasUsu_Historico");
      entity.HasKey(m => m.Id);
      entity.Property(e => e.Fecha).HasColumnType("datetime");
      entity.Property(e => e.IdLastMov).HasColumnName("idLastMov");
      entity.Property(e => e.IdMaquinaMov).HasColumnName("idMaquinaMov");
      entity.Property(e => e.IdModelo).HasColumnName("idModelo");
      entity.Property(e => e.IdSubCliente).HasColumnName("idSubCliente");
      entity.Property(e => e.IdUsuActualizaMov).HasColumnName("idUsuActualizaMov");
    });

    modelBuilder.Entity<MovSubClienteIdentificacionManual>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__MovSubCl__3213E83FD2E3F550");

      entity.ToTable("MovSubClienteIdentificacionManual");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Fecha).HasColumnType("datetime");

      entity.HasOne(d => d.IdSubClienteNavigation).WithMany(p => p.MovSubClienteIdentificacionManuals)
              .HasForeignKey(d => d.IdSubCliente)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_SubClienteIdentManual_SubClientes");
    });

    modelBuilder.Entity<MovSubClienteIdioma>(entity =>
    {
      entity.ToTable("MovSubClienteIdioma");

      entity.Property(e => e.IdSubCliente).HasColumnName("idSubCliente");
      entity.Property(e => e.Idioma)
              .IsRequired()
              .HasMaxLength(20)
              .HasColumnName("idioma");

      entity.HasOne(d => d.IdSubClienteNavigation).WithMany()
              .HasForeignKey(d => d.IdSubCliente)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_SubClienteIdioma_SubClientes");
    });

    modelBuilder.Entity<MovSubClientesDeGuardium>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.IdSubCliente).HasColumnName("idSubCliente");

      entity.HasOne(d => d.IdSubClienteNavigation).WithMany()
              .HasForeignKey(d => d.IdSubCliente)
              .HasConstraintName("FK_MovSubClientesDeGuardia_MasterSubClientes");
    });

    modelBuilder.Entity<MovSubClientesGestionTaquilla>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("Mov_SubClientesGestionTaquillas");

      entity.Property(e => e.FechaPrevistaAlta).HasColumnType("date");
      entity.Property(e => e.FechaPrevistaBaja).HasColumnType("date");
      entity.Property(e => e.IdSubCliente).HasColumnName("idSubCliente");

      entity.HasOne(d => d.IdSubClienteNavigation).WithMany()
              .HasForeignKey(d => d.IdSubCliente)
              .HasConstraintName("FK_Mov_SubClientesGestionTaquillas_MasterSubClientes");
    });

    modelBuilder.Entity<MovSubClientesTipoBaja>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MovSubClientesTipoBaja");

      entity.Property(e => e.IdSubCliente).HasColumnName("idSubCliente");
      entity.Property(e => e.IdTipoBaja).HasColumnName("idTipoBaja");

      entity.HasOne(d => d.IdSubClienteNavigation).WithMany()
              .HasForeignKey(d => d.IdSubCliente)
              .HasConstraintName("FK_MovSubClientesTipoBaja_MasterSubClientes");
    });

    modelBuilder.Entity<MovSubClientesTraslado>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.DescripcionTraslado).HasMaxLength(100);
      entity.Property(e => e.FechaTraslado).HasColumnType("datetime");
      entity.Property(e => e.IdSubCliente).HasColumnName("idSubCliente");

      entity.HasOne(d => d.IdSubClienteNavigation).WithMany()
              .HasForeignKey(d => d.IdSubCliente)
              .HasConstraintName("FK_MovSubClientesTraslados_MasterSubClientes");
    });

    modelBuilder.Entity<MovTallasModelo>(entity =>
    {
      entity.HasIndex(e => new { e.IdModeloPrenda, e.Talla }, "IX_MovTallasModelos").IsUnique();

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdModeloPrenda).HasColumnName("idModeloPrenda");
      entity.Property(e => e.Ordenenlalista).HasColumnName("ordenenlalista");
      entity.Property(e => e.Talla)
              .HasMaxLength(50)
              .HasColumnName("talla");

      entity.HasOne(d => d.IdModeloPrendaNavigation).WithMany(p => p.MovTallasModelos)
              .HasForeignKey(d => d.IdModeloPrenda)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_MovTallasModelos_MasterModeloPrenda");
    });

    modelBuilder.Entity<MovTaquilla>(entity =>
    {
      entity.ToTable("MovTaquilla");
      entity.HasKey(mt => mt.Id);

      entity.Property(e => e.Fecha).HasColumnType("datetime");

      entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
      entity.Property(e => e.Nllave).HasColumnName("NLlave");
    });

    modelBuilder.Entity<MovTaquillas>(entity =>
    {
      entity
              .HasNoKey()
              .ToTable("MovTaquillas");

      entity.Property(e => e.CodTagLeido).HasMaxLength(50);
      entity.Property(e => e.DateLastMov).HasColumnType("datetime");
      entity.Property(e => e.Fecha).HasColumnType("datetime");
      entity.Property(e => e.HoraLogin).HasColumnType("datetime");
      entity.Property(e => e.IdMaquina).HasColumnName("idMaquina");
      entity.Property(e => e.IdMaquinaLastMov).HasColumnName("idMaquinaLastMov");
      entity.Property(e => e.IdModeloPrendaLeido).HasColumnName("idModeloPrendaLeido");
      entity.Property(e => e.IdModeloPrendaPedido).HasColumnName("idModeloPrendaPedido");
      entity.Property(e => e.IdPrendaLeido).HasColumnName("idPrendaLeido");
      entity.Property(e => e.IdSubCli).HasColumnName("idSubCli");
      entity.Property(e => e.IdTallaPrendaLeido)
              .HasMaxLength(10)
              .HasColumnName("idTallaPrendaLeido");
      entity.Property(e => e.IdTallaPrendaPedido)
              .HasMaxLength(10)
              .HasColumnName("idTallaPrendaPedido");
    });

    modelBuilder.Entity<MovTelefono>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMovTelefonos_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.IdFormaContacto, "MasterFormasContactoMovTelefonos");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdFormaContacto, "id_FormaContacto");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Contacto).HasMaxLength(50);
      entity.Property(e => e.Descripcion)
              .HasMaxLength(50)
              .HasColumnName("descripcion");
      entity.Property(e => e.IdFormaContacto).HasColumnName("id_FormaContacto");
      entity.Property(e => e.Valor)
              .HasMaxLength(50)
              .HasColumnName("valor");

      entity.HasOne(d => d.IdFormaContactoNavigation).WithMany(p => p.MovTelefonos)
              .HasForeignKey(d => d.IdFormaContacto)
              .HasConstraintName("MovTelefonos_FK00");
    });

    modelBuilder.Entity<MovTelefonosCliente>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMovTelefonosClientes_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.IdCliente, "MasterClientesmovTelefonosClientes");

      entity.HasIndex(e => e.IdTelefono, "MovTelefonosMovTelefonosClientes");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdCliente, "id_Cliente");

      entity.HasIndex(e => e.IdTelefono, "id_Telefono");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdCliente).HasColumnName("id_Cliente");
      entity.Property(e => e.IdTelefono).HasColumnName("id_Telefono");

      entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.MovTelefonosClientes)
              .HasForeignKey(d => d.IdCliente)
              .HasConstraintName("MovTelefonosClientes_FK00");

      entity.HasOne(d => d.IdTelefonoNavigation).WithMany(p => p.MovTelefonosClientes)
              .HasForeignKey(d => d.IdTelefono)
              .HasConstraintName("MovTelefonosClientes_FK01");
    });

    modelBuilder.Entity<MovTelefonosPropio>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMovTelefonosPropios_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.IdPropio, "DatosPropiosMovTelefonosPropios");

      entity.HasIndex(e => e.IdTelefono, "MovTelefonosMovTelefonosPropios");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdPropio, "id_Cliente");

      entity.HasIndex(e => e.IdTelefono, "id_Telefono");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdPropio).HasColumnName("id_Propio");
      entity.Property(e => e.IdTelefono).HasColumnName("id_Telefono");

      entity.HasOne(d => d.IdPropioNavigation).WithMany(p => p.MovTelefonosPropios)
              .HasForeignKey(d => d.IdPropio)
              .HasConstraintName("MovTelefonosPropios_FK00");

      entity.HasOne(d => d.IdTelefonoNavigation).WithMany(p => p.MovTelefonosPropios)
              .HasForeignKey(d => d.IdTelefono)
              .HasConstraintName("MovTelefonosPropios_FK01");
    });

    modelBuilder.Entity<MovTelefonosProveedore>(entity =>
    {
      entity.HasKey(e => e.Id)
              .HasName("aaaaaMovTelefonosProveedores_PK")
              .IsClustered(false);

      entity.HasIndex(e => e.IdProveedor, "MasterProveedoresmovTelefonosProveedores");

      entity.HasIndex(e => e.IdTelefono, "MovTelefonosMovTelefonosProveedores");

      entity.HasIndex(e => e.Id, "id");

      entity.HasIndex(e => e.IdProveedor, "id_Cliente");

      entity.HasIndex(e => e.IdTelefono, "id_Telefono");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
      entity.Property(e => e.IdTelefono).HasColumnName("id_Telefono");

      entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.MovTelefonosProveedores)
              .HasForeignKey(d => d.IdProveedor)
              .HasConstraintName("MovTelefonosProveedores_FK00");

      entity.HasOne(d => d.IdTelefonoNavigation).WithMany(p => p.MovTelefonosProveedores)
              .HasForeignKey(d => d.IdTelefono)
              .HasConstraintName("MovTelefonosProveedores_FK01");
    });

    modelBuilder.Entity<MovTipoAntenaPdum>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__MovTipoA__3213E83F27A800D7");

      entity.ToTable("MovTipoAntenaPDA");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.IdLastMov).HasColumnName("idLastMov");
      entity.Property(e => e.IdTipoMov).HasColumnName("idTipoMov");
      entity.Property(e => e.Orden).HasColumnName("orden");
    });

    modelBuilder.Entity<OldCode>(entity =>
    {
      entity.HasKey(e => new { e.Id, e.OldCode1 }).HasName("PK__OldCodes__A3D8298F8D681C87");

      entity.Property(e => e.OldCode1)
              .HasMaxLength(50)
              .HasColumnName("OldCode");
      entity.Property(e => e.Fecha).HasColumnType("datetime");
    });

    modelBuilder.Entity<PruebasVictor1>(entity =>
    {
      entity.ToTable("PruebasVictor1");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.CodTag)
              .IsRequired()
              .HasMaxLength(20)
              .HasColumnName("codTag");
      entity.Property(e => e.Date)
              .HasColumnType("datetime")
              .HasColumnName("date");
      entity.Property(e => e.IdSerialRead)
              .HasMaxLength(50)
              .HasColumnName("idSerialRead");
    });

    modelBuilder.Entity<PuertaDispensacione>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.Iplector)
              .HasMaxLength(100)
              .HasColumnName("IPLector");
      entity.Property(e => e.Npuerta).HasColumnName("NPuerta");
      entity.Property(e => e.PosXdispensacion).HasColumnName("PosXDispensacion");
      entity.Property(e => e.PosXreposo).HasColumnName("PosXReposo");
      entity.Property(e => e.PosYdispensacion).HasColumnName("PosYDispensacion");
    });

    modelBuilder.Entity<PuertaEstado>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_PuertaEstados");

      entity.Property(e => e.Npuerta).HasColumnName("NPuerta");
    });

    modelBuilder.Entity<RptFrecuenciaMovimientosPrenda>(entity =>
    {
      entity
              .HasNoKey()
              .ToView("rptFrecuenciaMovimientosPrendas");

      entity.Property(e => e.Apellido1).HasMaxLength(150);
      entity.Property(e => e.Apellido2).HasMaxLength(150);
      entity.Property(e => e.Descrip)
              .HasMaxLength(100)
              .HasColumnName("descrip");
      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Nombre)
              .HasMaxLength(100)
              .HasColumnName("nombre");
      entity.Property(e => e.Taquilla)
              .HasMaxLength(150)
              .HasColumnName("taquilla");
      entity.Property(e => e.Vestuario)
              .HasMaxLength(200)
              .HasColumnName("vestuario");
    });

    modelBuilder.Entity<TLabelsAcumuladasLaundry>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK_AAA");

      entity.ToTable("t_LabelsAcumuladasLaundry");

      entity.Property(e => e.Apellido1).HasMaxLength(100);
      entity.Property(e => e.Apellido2)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.Campo1)
              .HasMaxLength(150)
              .IsUnicode(false);
      entity.Property(e => e.CodTagPre)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Code128)
              .IsRequired()
              .HasMaxLength(50)
              .HasDefaultValueSql("('')");
      entity.Property(e => e.Departamento).HasMaxLength(50);
      entity.Property(e => e.Empresa).HasMaxLength(50);
      entity.Property(e => e.IdPre)
              .HasColumnType("numeric(18, 0)")
              .HasColumnName("idPre");
      entity.Property(e => e.Modelo)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Nombre).HasMaxLength(100);
      entity.Property(e => e.NombreCliente).HasMaxLength(100);
      entity.Property(e => e.Norden).HasColumnName("NOrden");
      entity.Property(e => e.Ntotal).HasColumnName("NTotal");
      entity.Property(e => e.Prenda).HasMaxLength(50);
      entity.Property(e => e.Talla).HasMaxLength(10);
      entity.Property(e => e.Taquilla).HasMaxLength(50);
      entity.Property(e => e.Vestuario)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<TTerminosFijo>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__t_Termin__3213E83F37377A40");

      entity.ToTable("t_TerminosFijos");

      entity.Property(e => e.Id)
              .ValueGeneratedNever()
              .HasColumnName("id");
      entity.Property(e => e.CadaAlbaranTfj).HasColumnName("CadaAlbaran_tfj");
      entity.Property(e => e.CadaFacturaTfj).HasColumnName("CadaFactura_tfj");
      entity.Property(e => e.CantPrendasTfj).HasColumnName("CantPrendas_tfj");
      entity.Property(e => e.FechaFinalTfj)
              .HasColumnType("datetime")
              .HasColumnName("FechaFinal_tfj");
      entity.Property(e => e.FechaInicioTfj)
              .HasColumnType("datetime")
              .HasColumnName("FechaInicio_tfj");
      entity.Property(e => e.IdClienteTfj).HasColumnName("idCliente_tfj");
      entity.Property(e => e.IdSubClienteTfj).HasColumnName("Id_SubCliente_tfj");
      entity.Property(e => e.IdprendaTfj).HasColumnName("idprenda_tfj");
      entity.Property(e => e.ObservacionesTfj)
              .HasMaxLength(100)
              .HasColumnName("Observaciones_tfj");
    });

    modelBuilder.Entity<TmpCuad>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__tmpCuad__3213E83F5CB30798");

      entity.ToTable("tmpCuad");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Col).HasMaxLength(5);
      entity.Property(e => e.Fecha).HasColumnType("datetime");
      entity.Property(e => e.FinalProc).HasDefaultValueSql("((0))");
      entity.Property(e => e.IdCli).HasColumnName("idCli");
      entity.Property(e => e.IdPre).HasColumnName("idPre");
      entity.Property(e => e.IdsubCli).HasColumnName("idsubCli");
      entity.Property(e => e.NcolTot).HasColumnName("NColTot");
      entity.Property(e => e.Nmov).HasColumnName("NMov");
      entity.Property(e => e.NrowTot).HasColumnName("NRowTot");
      entity.Property(e => e.Puesto).HasMaxLength(30);
      entity.Property(e => e.Row).HasMaxLength(5);
      entity.Property(e => e.TipoOrd).HasDefaultValueSql("((0))");
    });

    modelBuilder.Entity<VerificacionFuncionamiento>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__Verifica__3213E83FBAA93DD2");

      entity.ToTable("VerificacionFuncionamiento");

      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Fecha)
              .HasColumnType("datetime")
              .HasColumnName("fecha");
      entity.Property(e => e.IdMaquina).HasColumnName("idMaquina");
    });

    modelBuilder.Entity<VistaGfh>(entity =>
    {
      entity
              .HasNoKey()
              .ToView("VistaGFH");

      entity.Property(e => e.Apellido1).HasMaxLength(150);
      entity.Property(e => e.Apellido2).HasMaxLength(150);
      entity.Property(e => e.Descrip)
              .HasMaxLength(100)
              .HasColumnName("descrip");
      entity.Property(e => e.Descripcion)
              .HasMaxLength(255)
              .HasColumnName("DESCRIPCION");
      entity.Property(e => e.Fecha).HasColumnType("datetime");
      entity.Property(e => e.Gfh)
              .HasMaxLength(255)
              .HasColumnName("GFH");
      entity.Property(e => e.Id).HasColumnName("id");
      entity.Property(e => e.Nombre).HasMaxLength(100);
    });

    modelBuilder.Entity<VistaInformePrendaUsuario>(entity =>
    {
      entity
              .HasNoKey()
              .ToView("vista_InformePrendaUsuario");

      entity.Property(e => e.Borrado).HasColumnName("borrado");
      entity.Property(e => e.CodigoTag)
              .HasMaxLength(50)
              .HasColumnName("codigoTAG");
      entity.Property(e => e.Color).HasMaxLength(50);
      entity.Property(e => e.DateLastMov).HasColumnType("datetime");
      entity.Property(e => e.Descrip)
              .HasMaxLength(100)
              .HasColumnName("descrip");
      entity.Property(e => e.Descripcion)
              .HasMaxLength(50)
              .HasColumnName("descripcion");
      entity.Property(e => e.Falta)
              .HasColumnType("datetime")
              .HasColumnName("FAlta");
      entity.Property(e => e.Fbaja)
              .HasColumnType("datetime")
              .HasColumnName("FBaja");
      entity.Property(e => e.FbajaReal)
              .HasColumnType("datetime")
              .HasColumnName("FBajaReal");
      entity.Property(e => e.IdCliente).HasColumnName("id_Cliente");
      entity.Property(e => e.IdEtiqueta).HasColumnName("id_etiqueta");
      entity.Property(e => e.IdModeloPrenda).HasColumnName("id_ModeloPrenda");
      entity.Property(e => e.IdSubCliente).HasColumnName("id_subCliente");
      entity.Property(e => e.Modelo).HasMaxLength(50);
      entity.Property(e => e.Nciclos).HasColumnName("NCiclos");
      entity.Property(e => e.Nlavados).HasColumnName("NLavados");
      entity.Property(e => e.Norden).HasColumnName("NOrden");
      entity.Property(e => e.Ntotal).HasColumnName("NTotal");
      entity.Property(e => e.Stock).HasColumnName("stock");
      entity.Property(e => e.Talla).HasMaxLength(50);
      entity.Property(e => e.Taquilla).HasMaxLength(50);
    });

    modelBuilder.Entity<VistaMovPre>(entity =>
    {
      entity
              .HasNoKey()
              .ToView("VistaMovPre");

      entity.Property(e => e.DesPren).HasMaxLength(100);
      entity.Property(e => e.Fecha).HasColumnType("datetime");
      entity.Property(e => e.Hora).HasColumnType("datetime");
      entity.Property(e => e.IdCli).HasColumnName("idCli");
      entity.Property(e => e.IdPrenda).HasColumnName("Id_Prenda");
      entity.Property(e => e.IdSubCli).HasColumnName("idSubCli");
      entity.Property(e => e.IdTipoAntena).HasColumnName("id_TipoAntena");
      entity.Property(e => e.ModeloPrenda).HasMaxLength(50);
      entity.Property(e => e.Nciclos).HasColumnName("NCiclos");
      entity.Property(e => e.Nlavados).HasColumnName("NLavados");
      entity.Property(e => e.Nombre).HasMaxLength(100);
      entity.Property(e => e.NombreMaquina).HasMaxLength(50);
      entity.Property(e => e.NombreSubCli).HasMaxLength(403);
      entity.Property(e => e.Norden).HasColumnName("NOrden");
      entity.Property(e => e.Ntotal).HasColumnName("NTotal");
      entity.Property(e => e.Obser).HasMaxLength(250);
      entity.Property(e => e.TipoAntena).HasMaxLength(50);
    });

    modelBuilder.Entity<VistaPrendasYultimoMovimiento>(entity =>
    {
      entity
              .HasNoKey()
              .ToView("VistaPrendasYUltimoMovimiento");

      entity.Property(e => e.Borrado).HasColumnName("borrado");
      entity.Property(e => e.CodigoTag)
              .HasMaxLength(50)
              .HasColumnName("codigoTAG");
      entity.Property(e => e.Color).HasMaxLength(50);
      entity.Property(e => e.DateLastMov).HasColumnType("datetime");
      entity.Property(e => e.Descrip)
              .HasMaxLength(100)
              .HasColumnName("descrip");
      entity.Property(e => e.EnUso).HasColumnName("enUso");
      entity.Property(e => e.Falta)
              .HasColumnType("datetime")
              .HasColumnName("FAlta");
      entity.Property(e => e.Fbaja)
              .HasColumnType("datetime")
              .HasColumnName("FBaja");
      entity.Property(e => e.FbajaReal)
              .HasColumnType("datetime")
              .HasColumnName("FBajaReal");
      entity.Property(e => e.IdCliente).HasColumnName("id_Cliente");
      entity.Property(e => e.IdEtiqueta).HasColumnName("id_etiqueta");
      entity.Property(e => e.IdModeloPrenda).HasColumnName("id_ModeloPrenda");
      entity.Property(e => e.IdSubCliente).HasColumnName("id_subCliente");
      entity.Property(e => e.IdSubClienteUsaPrenda).HasColumnName("idSubClienteUsaPrenda");
      entity.Property(e => e.Nciclos).HasColumnName("NCiclos");
      entity.Property(e => e.Nlavados).HasColumnName("NLavados");
      entity.Property(e => e.Norden).HasColumnName("NOrden");
      entity.Property(e => e.Ntotal).HasColumnName("NTotal");
      entity.Property(e => e.RegistroMovimientoTransitorio).HasColumnName("registroMovimientoTransitorio");
      entity.Property(e => e.Stock).HasColumnName("stock");
      entity.Property(e => e.Talla).HasMaxLength(50);
      entity.Property(e => e.Taquilla).HasMaxLength(50);
    });

    modelBuilder.Entity<MovCreditosModelosDepartamento>()
        .HasKey(m => new { m.IdDepartamento, m.IdModelo, m.TipoRango });

    modelBuilder.Entity<MovMaquinasIncidencia>()
        .HasKey(m => new { m.IdMaquina, m.Fecha });

    modelBuilder.Entity<PuertaDispensacione>()
        .HasKey(m => new { m.Npuerta, m.IdMaquina });

    modelBuilder.Entity<PrendaDisponibleEnLenceria>()
        .HasNoKey()
        .ToView("ListaPrendasUbicacionSubcliente");
    modelBuilder.Entity<PrendasDisponiblesSubCliente>()
      .HasNoKey()
      .ToView("ListaPrendasDisponiblesSubCliente");
    modelBuilder.Entity<SubClientHistoryModel>()
        .HasNoKey();

    modelBuilder.Entity<MasterNotificaciones>()
        .HasOne(n => n.MasterAvisosPorEmail)
        .WithMany()
        .HasForeignKey(n => n.IdMasterAviso)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<MasterNotificaciones>()
        .HasOne(n => n.MasterMaquinaDispensacion)
        .WithMany()
        .HasForeignKey(n => n.IdMaquina)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<MasterNotificaciones>()
       .ToTable("MasterNotificaciones", tb =>
       {
         tb.HasTrigger("MasterNotificaciones_insert_trigger");
         tb.HasTrigger("MasterNotificaciones_update_trigger");
         tb.HasTrigger("MasterNotificaciones_delete_trigger");
       });
   

    modelBuilder.Entity<RegistroIdentificacionUsuario>()
        .HasOne(n => n.MasterSubClientes)
        .WithMany()
        .HasForeignKey(n => n.IdMasterSubCliente)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<RegistroIdentificacionUsuario>()
        .HasOne(n => n.MasterMaquinaDispensacion)
        .WithMany()
        .HasForeignKey(n => n.IdMaquinaDispensacion)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<RegistroIdentificacionUsuario>()
      .ToTable("RegistroIdentificacionUsuario", tb =>
      {
        tb.HasTrigger("RegistroIdentificacionUsuario_insert_trigger");
        tb.HasTrigger("RegistroIdentificacionUsuario_update_trigger");
        tb.HasTrigger("RegistroIdentificacionUsuario_delete_trigger");
      });

    modelBuilder.Entity<PrendasDisponiblesEnTaquillasInteligentes>()
        .HasNoKey()
        .ToView("ListaPrendasDisponiblesEnTaquillasInteligentes");

    modelBuilder.Entity<PrendasDisponiblesEnMaquinaDispensacion>()
           .HasNoKey()           
           .ToView("ListaPrendasDisponiblesEnMaquinaDispensacion")
           .Property(p => p.StockReal)
           .HasPrecision(18, 2);

    modelBuilder.Entity<PrendasDisponiblesEnQuesitoConRFID>()
        .HasNoKey()
        .ToView("ListaPrendasDisponiblesEnQuesitosConRFID");

    modelBuilder.Entity<CreditosDisponiblesModel>()
           .HasNoKey()
           .ToView("ComprobarCreditosDisponibles");

    modelBuilder.Entity<MasterSubClienteTarjeta>()
            .HasOne(t => t.MasterSubClientes)
            .WithMany()
            .HasForeignKey(t => t.IdMasterSubCliente)
            .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<MovMaquinaDepartamentos>()
    .Property(t => t.Id)
    .ValueGeneratedOnAdd();

    modelBuilder.Entity<MovMaquinaDepartamentos>()
    .HasIndex(m => new { m.IdMasterMaquinaDispensacion, m.IdMasterDepartamentosCliente })
    .IsUnique();

    modelBuilder.Entity<MovMaquinaDepartamentos>()
        .HasOne(t => t.MasterMaquinaDispensacion)
        .WithMany(m => m.MovMaquinaDepartamentos) // Agrega ICollection<MovMaquinaDepartamentos> en MasterMaquinaDispensacion
        .HasForeignKey(t => t.IdMasterMaquinaDispensacion)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<MovMaquinaDepartamentos>()
        .HasOne(t => t.MasterDepartamentosCliente)
        .WithMany(d => d.MovMaquinaDepartamentos) // Agrega ICollection<MovMaquinaDepartamentos> en MasterDepartamentosCliente
        .HasForeignKey(t => t.IdMasterDepartamentosCliente)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<MasterModeloPrendaComposicion>(entity =>
    {
      entity.HasKey(pc => new { pc.IdPrendaCompuesta, pc.IdPrendaComponente });
      entity.ToTable("MasterModeloPrendaComposicion");

      entity.HasOne(pc => pc.PrendaCompuesta)
            .WithMany(p => p.ComoCompuesta)
            .HasForeignKey(pc => pc.IdPrendaCompuesta)
            .OnDelete(DeleteBehavior.Restrict);

      entity.HasOne(pc => pc.PrendaComponente)
            .WithMany(p => p.ComoComponente)
            .HasForeignKey(pc => pc.IdPrendaComponente)
            .OnDelete(DeleteBehavior.Restrict);

      entity.ToTable(tb =>
      {
        tb.HasCheckConstraint("CK_MasterModeloPrendaComposicion_NoAutoComposicion", "[IdPrendaCompuesta] <> [IdPrendaComponente]");
      });
    });

    modelBuilder.Entity<MovPrendasNumeroCiclosExternos>()
       .HasKey(e => e.IdMasterPrendas);

    modelBuilder.Entity<MovPrendasNumeroCiclosExternos>()
        .HasOne(e => e.MasterPrenda)
        .WithOne(p => p.MovPrendasNumeroCiclosExternos)
        .HasForeignKey<MovPrendasNumeroCiclosExternos>(e => e.IdMasterPrendas)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<MovMaquinaContadorPrendasSucias>()
      .ToTable("MovMaquinaContadorPrendasSucias", tb =>
      {
          tb.HasTrigger("MovMaquinaContadorPrendasSucias_insert_trigger");
          tb.HasTrigger("MovMaquinaContadorPrendasSucias_update_trigger");
          tb.HasTrigger("MovMaquinaContadorPrendasSucias_delete_trigger");
      });

    // Security tables - map to singular table names
    modelBuilder.Entity<AccessLog>()
        .ToTable("AccessLog");
    
    modelBuilder.Entity<OperationsLog>()
        .ToTable("OperationsLog");
    
    modelBuilder.Entity<Device>()
        .ToTable("Devices");

    modelBuilder.Entity<AuthRefreshToken>(entity =>
    {
      entity.ToTable("AuthRefreshTokens");
      entity.HasKey(e => e.Token);
      entity.Property(e => e.Token).HasMaxLength(64).IsUnicode(false);
      entity.Property(e => e.Subject).HasMaxLength(450).IsRequired();
      entity.Property(e => e.ExpiresAtUtc).HasColumnType("datetime2");
      entity.Property(e => e.CreatedAtUtc).HasColumnType("datetime2");
      entity.HasIndex(e => new { e.Revoked, e.ExpiresAtUtc });
    });

    // Linen operations table
    modelBuilder.Entity<LinenOperation>()
        .ToTable("LinenOperations")
        .HasOne(lo => lo.Item)
        .WithMany()
        .HasForeignKey(lo => lo.ItemId)
        .OnDelete(DeleteBehavior.Restrict);

        OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
