using System;
using System.Collections.Generic;

namespace LFS.Shared.Entities;

public partial class MasterControl
{
    public short? ComAntena { get; set; }

    public bool? ImpresoraTec { get; set; }

    public int? PuertoTec { get; set; }

    public bool? NoAcumulaEtiqueta { get; set; }

    public string TextoEtiquetaPrenda { get; set; }

    public int? VersionwebCtr { get; set; }

    public int? CantMaxPrendasUso { get; set; }

    public string EmailServidor { get; set; }

    public int? EmailPuerto { get; set; }

    public bool? EmailUseAuntentificacion { get; set; }

    public bool? EmailSsl { get; set; }

    public string EmailUser { get; set; }

    public string EmailPasswd { get; set; }

    public string EmailEmpresa { get; set; }

    public int? LimiteMensualIdentManual { get; set; }

    public int Id { get; set; }
}
