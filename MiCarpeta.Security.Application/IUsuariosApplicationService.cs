using MiCarpeta.Security.Common;

namespace MiCarpeta.Security.Application
{
    public interface IUsuariosApplicationService
    {
        ResponseViewModel IniciarSesion(string usuario, string clave);
    }
}
