using MiCarpeta.Security.Application;
using MiCarpeta.Security.Common;
using MiCarpeta.Security.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiCarpeta.Security.Presentation.Controllers
{
    [Route("api/seguridad")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IUsuariosApplicationService UsuariosApplicationService;

        public UsuariosController(IConfiguration config, IUsuariosApplicationService usuariosApplicationService)
        {
            Configuration = config;
            UsuariosApplicationService = usuariosApplicationService;
        }

        [HttpPost("iniciarSesion")]
        public IActionResult IniciarSesion([FromBody] Usuarios usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseViewModel
                {
                    Estado = 400,
                    Errores = new List<string>() { "Todos los campos son obligatorios" }
                });
            }

            ResponseViewModel response = UsuariosApplicationService.IniciarSesion(usuario.Usuario, usuario.Clave);

            return Ok(response);
        }
    }
}
