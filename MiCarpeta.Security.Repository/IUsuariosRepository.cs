using MiCarpeta.Security.Domain.Entities;

namespace MiCarpeta.Security.Repository
{
    public interface IUsuariosRepository : IRepository<Usuarios>
    {
        Usuarios ValidarInicioSesion(string usuario, string clave);
    }
}
