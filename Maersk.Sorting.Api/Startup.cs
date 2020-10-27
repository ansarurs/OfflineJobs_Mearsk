using Hangfire;
using Hangfire.MemoryStorage;
using Maersk.Sorting.Api.Controllers;
using Maersk.Sorting.Api.Data;
using Maersk.Sorting.Api.Wrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;

namespace Maersk.Sorting.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddSingleton<ISortJobProcessor, SortJobProcessor>();
            services.AddDbContext<SortJobDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SortJobDbConn")));
            
            services.AddScoped<IRepositoryWrappercs, RepositoryWrapper>();
            services.AddHangfire(x =>x.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                                  .UseSimpleAssemblyNameTypeSerializer()
                                  .UseDefaultTypeSerializer()
                                  .UseMemoryStorage());
            services.AddHangfireServer();
            services.AddControllers();
            //var mvcBuilder = services.AddMvc();
            //mvcBuilder.AddMvcOptions(o => o.Conventions.Add(new GenericRestControllerNameConvention()));
            //mvcBuilder.ConfigureApplicationPartManager(c =>
            //{
            //    c.FeatureProviders.Add(new GenericRestControllerFeatureProvider());
            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseHangfireDashboard();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
