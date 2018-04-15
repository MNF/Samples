using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace SwashbuckleTests
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Swashbuckle Example Api", Version = "v1" });
                var basePath = AppContext.BaseDirectory;
 				var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".XML";
                var filePath = Path.Combine(basePath, commentsFileName);
                // TODO: fix the code so that TravellerProfileApi.xml can be found in the correct path /app/ inside docker container
                if (File.Exists(filePath))
                {
                    c.IncludeXmlComments(filePath);
                }
                c.MapType<DateTime>(() => new Schema { Type = "string", Format = "date" });
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            //#if DEBUG
            if (env.IsDevelopment() || env.IsStaging())
            {
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Traveller Profile Api");
                });
            }
            app.UseMvc();
        }
    }
}
