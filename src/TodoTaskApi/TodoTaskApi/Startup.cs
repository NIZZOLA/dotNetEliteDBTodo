using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoTaskApi
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

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                config =>
                {
                    config.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                });
            });

            services.AddSwaggerGen(options =>
            {
                var titleBase = "BACKEND APLICAÇÃO TODO TASK";
                var description = $"Utilizada para teste de aplicativo feito no IONIC";
                var contact = new OpenApiContact()
                {
                    Email = "marcio.nizzola@etec.sp.gov.br",
                    Name = "Etec Itu",
                    Url = new System.Uri("http://www.etecitu.com.br")
                };

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = titleBase,
                    Description = description,
                    Contact = contact
                });
            });
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

            app.UseCors("AllowAllOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Primeira Api ");
            });
        }
    }
}
