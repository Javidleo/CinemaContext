using DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;
using UseCases.Services;

namespace Application
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Application", Version = "v1" });
            });
            services.AddDbContext<CinemaContext>(option => option.UseSqlServer(Configuration.GetConnectionString("LocalDb")), ServiceLifetime.Singleton);

            // services
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IMovieSansSalonService, MovieSansSalonService>();
            services.AddTransient<ISalonService, SalonService>();
            // repository
            services.AddTransient<IChairRepository, ChairRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<ISalonRepository, SalonRepository>();
            services.AddTransient<ICinemaRepository, CinemaRepository>();
            services.AddTransient<ISansRepository, SansRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IMovieSansSalonRepository, MovieSansSalonRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Application v1"));
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=ViewersController}/{action=GetAll}");
            });
        }
    }
}
