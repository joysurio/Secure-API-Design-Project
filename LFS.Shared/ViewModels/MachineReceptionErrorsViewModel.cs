using System;

namespace LFS.Shared.ViewModels
{
  public class MachineReceptionErrorsViewModel
  {
    public int Id { get; set; }
    public int IdMaquina { get; set; }
    public DateTime Fecha { get; set; }
    public int IdUsuario { get; set; }
    public int IdModelo { get; set; }
    public bool HaLeidoUsuarioDiferente { get; set; }
    public bool HaLeidoModeloDiferente { get; set; }
    public string Observaciones { get; set; }
    public string MachineDescription { get; set; }  
    public string SubClientDescription { get; set; }    
    public string ModelDescription { get; set; }
    public int CantidadRecogidas { get; set; }
    public int CantidadNoLeidas { get; set; }
    public int CantidadNoIdentificadas { get; set; }
    public int CantidadError { get; set; }
    public int CantidadHaLeidoUsuarioDiferente { get; set; }
    public int CantidadHaLeidoModeloDiferente { get; set; }
    public double PorCientoNoLeida { get; set; }
    public double PorCientoNoIdentificada { get; set; }
    public double PorCientoError { get; set; }
    public double PorCientoHaLeidoUsuarioDiferente { get; set; }
    public double PorCientoHaLeidoModeloDiferente { get; set; }

    //para diferencias RFID
    public int IdUsuarioAutenticado { get; set; }
    public string SubClientAutenticadoDescription { get; set; }
    public int IdModeloSeleccionado { get; set; }
    public string ModelSeleccionadoDescription { get; set; }
    public string? CodigoTag { get; set; }
    public DateTime? FechaEntrega { get; set; }

  }

}
