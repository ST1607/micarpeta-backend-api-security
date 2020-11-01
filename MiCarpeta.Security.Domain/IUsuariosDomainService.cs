using MiCarpeta.Security.Domain.Entities;

namespace MiCarpeta.Security.Domain
{
    public interface IUsuariosDomainService
    {
        Response IniciarSesion(string usuario, string clave);
    }
}
