using AutoMapper;
using MiCarpeta.Security.Common;
using MiCarpeta.Security.Domain;
using MiCarpeta.Security.Domain.Entities;

namespace MiCarpeta.Security.Application
{
    public class UsuariosApplicationService : IUsuariosApplicationService
    {
        private readonly IUsuariosDomainService UsuariosDomainService;
        private readonly IMapper Mapper;

        public UsuariosApplicationService(IUsuariosDomainService usuariosDomainService, IMapper mapper)
        {
            UsuariosDomainService = usuariosDomainService;
            Mapper = mapper;
        }

        public ResponseViewModel IniciarSesion(string usuario, string clave)
        {
            Response response = UsuariosDomainService.IniciarSesion(usuario, clave);

            return Mapper.Map<ResponseViewModel>(response);
        }
    }
}
