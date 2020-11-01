using AutoMapper;
using MiCarpeta.Security.Application;
using MiCarpeta.Security.Application.AutoMapper;
using MiCarpeta.Security.Domain;
using MiCarpeta.Security.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MiCarpeta.Security
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            IServiceCollection services = new ServiceCollection();

            ConfigureServices(services);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IUsuariosApplicationService, UsuariosApplicationService>();
            services.AddScoped<IUsuariosDomainService, UsuariosDomainService>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();

            AutoMapperConfig.Initialize();
            services.AddSingleton(Mapper.Configuration);
            services.AddSingleton<IMapper>(sp =>
              new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));

            // CONFIGURACI�N DEL SERVICIO DE AUTENTICACI�N JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["MiCarpeta:JWT:Issuer"],
                        ValidAudience = Configuration["MiCarpeta:JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["MiCarpeta:JWT:ClaveSecreta"])
                        )
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // A�ADIMOS EL MIDDLEWARE DE AUTENTICACI�N
            // DE USUARIOS AL PIPELINE DE ASP.NET CORE
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
