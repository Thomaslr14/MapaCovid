using Api.Repositories;
using Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Api.Models;
using Infrastructure.Database;
using Infrastructure.Database.Persistence;
using Api.Services.Maps;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Api
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
            MongoDBPersistence.Configure(Configuration);
            ConfigureDependency.Inject(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(@"C:\Development\Git\MapaCovid\src", "Maps")),
                RequestPath = "/maps"
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }

    public static class ConfigureDependency
    {
        public static void Inject(IServiceCollection services)
        {
            services.AddSingleton<IMongoConnect,MongoDatabase>();
            services.AddScoped<IRepository,Repository>();
            services.AddScoped<IServiceRepository,ServiceRepository>();
            services.AddScoped<IMapService, MapService>();
            services.AddScoped<PessoaModel>();
            services.AddScoped<EnderecoModel>();
            services.AddScoped<IndexModel>();
            services.AddMvc();
            services.AddControllersWithViews();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
        }
    }
}
