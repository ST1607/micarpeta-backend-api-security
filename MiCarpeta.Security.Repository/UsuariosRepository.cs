using MiCarpeta.Security.Common;
using MiCarpeta.Security.Domain.Entities;
using MiCarpeta.Security.Repository.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace MiCarpeta.Security.Repository
{
    public class UsuariosRepository : Repository<Usuarios>, IUsuariosRepository
    {
        public UsuariosRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Usuarios ValidarInicioSesion(string usuario, string clave)
        {
            List<FilterQuery> filters = new List<FilterQuery>()
            {
                new FilterQuery { 
                    AtributeName = "Usuario",
                    Operator = (int)Enumerators.QueryScanOperator.Equal,
                    ValueAtribute = usuario
                },
                new FilterQuery {
                    AtributeName = "Clave",
                    Operator = (int)Enumerators.QueryScanOperator.Equal,
                    ValueAtribute = clave
                }
            };

            Usuarios usuarioBD = GetByList(filters).FirstOrDefault();

            return usuarioBD;
        }
    }
}
