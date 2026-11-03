using LFS.Shared.Models.Old;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LFS.Shared.Models
{
  public class RespuestaServicioFord
  {
    #region Constructor

    public RespuestaServicioFord()
    {
    }

    #endregion

    #region Properties

    private bool _success;
    private string _message;
    private CSubCliente _subCliente;
    private List<string> _prendas;

    public bool Success
    {
      get { return _success; }
      set { _success = value; }
    }

    public string Message
    {
      get { return _message; }
      set { _message = value; }
    }

    public CSubCliente SubCliente
    {
      get { return _subCliente; }
      set { _subCliente = value; }
    }

    public List<string> Prendas
    {
      get { return _prendas; }
      set { _prendas = value; }
    }

    #endregion

  }
}
