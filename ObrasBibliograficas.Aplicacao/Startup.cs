using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ObrasBibliograficas.Servicos.Services;
using System;
using System.IO;
using System.Reflection;

namespace ObrasBibliograficas.Aplicacao
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Obras Bibliográficas - Guide", Version = "v1" });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

            });

            InicializarInjecaoDependencias(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Obras Bibliográficas - Guide - V1");
            });
        }

        #region ' InicializarInjecaoDependencias '
        /// <summary>
        /// Método responsável por realizar a injeção de dependência
        /// </summary>
        /// <param name="services">Parâmetro que representa a coleção de servicos</param>
        public void InicializarInjecaoDependencias(IServiceCollection services)
        {
            services.AddTransient<ObrasBibliograficasService>();
        }
        #endregion
    }
}