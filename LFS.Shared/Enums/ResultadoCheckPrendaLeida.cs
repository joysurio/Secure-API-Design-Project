using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFS.Shared.Enums
{
  public enum ResultadoCheckPrendaLeida
  {
    OK,
    USUARIO_SUPERADO_FECHA_BAJA,
    USUARIO_BORRADO,
    PRENDA_SUPERADA_FECHA_BAJA,
    PRENDA_BORRADA,
    CICLOS_LAVADOS_SUPERADOS,
    USUARIO_FIJADO_NO_CORRESPONDE,
    PRENDA_YA_LEIDA_EN_MISMO_PUNTO,
    NULL
  }
}
