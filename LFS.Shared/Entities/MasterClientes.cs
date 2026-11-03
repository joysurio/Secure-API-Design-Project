using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterClientes
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string RazonSocial { get; set; }

    public int? IdContactoPrincipal { get; set; }

    public string Cif { get; set; }

    public short? Iva { get; set; }

    public DateTime? FechaAlta { get; set; }

    public DateTime? FechaBaja { get; set; }

    public string DenominacionSubCliente { get; set; }

    public int? IdEtiquetaDefecto { get; set; }

    public bool? Borrado { get; set; }

    public string Actividad { get; set; }

    public string TextoEtiquetaPrenda { get; set; }

    public int? IdClienteLavander { get; set; }

    public int? TipoMovimientoAlbaran { get; set; }

    public int? IdActividad { get; set; }

    public int TipoMarcadoPrenda { get; set; }

    public int CantidadTaquillas { get; set; }

    public int? NumLlavesPorTaquilla { get; set; }

    public bool OrdenacionSoplsoloConPosicionesFijas { get; set; }

    public bool NoEliminarCampo2AlAsociarTarjeta { get; set; }

    public short? DiaDeSemanaParaServicio { get; set; }

    public bool? MaquinasRfid { get; set; }

    public int? TipoMovimientoAumentaCiclosPrenda { get; set; }

    public bool CrearPedidoEnEntrada { get; set; }

    public virtual ICollection<MasterDepartamentosCliente> MasterDepartamentosClientes { get; set; } = new List<MasterDepartamentosCliente>();

    public virtual ICollection<MasterPrecioTipoAntena> MasterPrecioTipoAntenas { get; set; } = new List<MasterPrecioTipoAntena>();

    public virtual ICollection<MasterPrendas> MasterPrenda { get; set; } = new List<MasterPrendas>();

    public virtual ICollection<MasterSubClientes> MasterSubClientes { get; set; } = new List<MasterSubClientes>();

    public virtual ICollection<MovDireccionesCliente> MovDireccionesClientes { get; set; } = new List<MovDireccionesCliente>();

    public virtual ICollection<MovEtiquetasCliente> MovEtiquetasClientes { get; set; } = new List<MovEtiquetasCliente>();

    public virtual ICollection<MovTelefonosCliente> MovTelefonosClientes { get; set; } = new List<MovTelefonosCliente>();

    public virtual MasterTipoMarcadoPrenda TipoMarcadoPrendaNavigation { get; set; }
}
