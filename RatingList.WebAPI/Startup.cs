using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Media.BLL.Contracts;
using Media.BLL.Implementation;
using Media.DataAccess.Context;
using Media.DataAccess.Contracts;
using Media.DataAccess.Implementations;
using Media.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Media.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            //BLL
            services.Add(new ServiceDescriptor(typeof(IRatingCreateService),typeof(RatingCreateService), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IRatingGetService),typeof(RatingGetService), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IRatingUpdateService),typeof(RatingUpdateService), ServiceLifetime.Scoped));
            
            services.Add(new ServiceDescriptor(typeof(IViewerCreateService),typeof(ViewerCreateService), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IViewerGetService),typeof(ViewerGetService), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IViewerUpdateService),typeof(ViewerUpdateService), ServiceLifetime.Scoped));
            
            services.Add(new ServiceDescriptor(typeof(ICriticCreateService),typeof(CriticCreateService), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(ICriticGetService),typeof(CriticGetService), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(ICriticUpdateService),typeof(CriticUpdateService), ServiceLifetime.Scoped));

            //DataAccess
            services.Add(new ServiceDescriptor(typeof(IRatingDataAccess), typeof(RatingDataAccess), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IViewerDataAccess), typeof(ViewerDataAccess), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(ICriticDataAccess), typeof(CriticDataAccess), ServiceLifetime.Transient));

            //DB Contexts
            services.AddDbContext<RatingContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("Media")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<RatingContext>();
                context.Database.EnsureCreated(); 
            }
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
        }
    }
}