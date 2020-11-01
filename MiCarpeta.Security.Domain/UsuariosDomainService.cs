using MiCarpeta.Security.Domain.Entities;
using MiCarpeta.Security.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace MiCarpeta.Security.Domain
{
    public class UsuariosDomainService: IUsuariosDomainService
    {
        private readonly IConfiguration Configuration;
        private readonly IUsuariosRepository UsuariosRepository;

        public UsuariosDomainService(IConfiguration configuration, IUsuariosRepository usuariosRepository)
        {
            Configuration = configuration;
            UsuariosRepository = usuariosRepository;
        }

        public Response IniciarSesion(string usuario, string clave)
        {
            Usuarios usuarioBD = UsuariosRepository.ValidarInicioSesion(usuario, clave);

            if(usuarioBD == null)
            {
                return new Response()
                {
                    Estado = 201,
                    Errores = new List<string>() {
                            "El usuario o la contraseña no son correctos."
                        }
                };
            }
            else
            {
                string token = GenerarTokenJWT(usuarioBD);

                usuarioBD.Token = token;
                usuarioBD.VencimientoToken = DateTime.UtcNow.AddHours(2);

                UsuariosRepository.Update(usuarioBD);

                return new Response()
                {
                    Estado = 200,
                    Data = token
                };
            }
        }

        // GENERAMOS EL TOKEN CON LA INFORMACIÓN DEL USUARIO
        private string GenerarTokenJWT(Usuarios usuarioInfo)
        {
            SymmetricSecurityKey _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["MiCarpeta:JWT:ClaveSecreta"]));
            
            SigningCredentials _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            
            List<Claim> _Claims = new List<Claim>() {
                new Claim("IdUsuario", usuarioInfo.IdUsuario.ToString()),
                new Claim("IdRol", usuarioInfo.IdRol.ToString())
            };

            if (usuarioInfo.IdRol.Equals(1))
                _Claims.Add(new Claim(ClaimTypes.Role, "Ciudadano"));
            else
                _Claims.Add(new Claim(ClaimTypes.Role, "Operador"));

            var token = new JwtSecurityToken(
                    issuer: Configuration["MiCarpeta:JWT:Issuer"],
                    audience: Configuration["MiCarpeta:JWT:Audience"],
                    expires: DateTime.UtcNow.AddHours(2),
                    signingCredentials: _signingCredentials,
                    claims: _Claims
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
