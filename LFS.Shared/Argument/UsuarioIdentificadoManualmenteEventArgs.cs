using LFS.Shared.Models.Old;
using System;

namespace LFS.Shared.Argument
{
    public class UsuarioIdentificadoManualmenteEventArgs : EventArgs
    {
        public UsuarioIdentificadoManualmenteEventArgs(Person usuarioIdentificado)
        {
            Usuario = usuarioIdentificado;
        }

        public Person Usuario;
    }
}
