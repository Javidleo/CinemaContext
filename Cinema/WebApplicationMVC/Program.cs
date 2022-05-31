using DataAccess.Context;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;
using UseCases.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services DI

builder.Services.AddControllers();

builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddTransient<IMovieSansSalonService, MovieSansSalonService>();
builder.Services.AddTransient<ISalonService, SalonService>();
// add Repository DI 
builder.Services.AddTransient<IChairRepository, ChairRepository>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<ISalonRepository, SalonRepository>();
builder.Services.AddTransient<ICinemaRepository, CinemaRepository>();
builder.Services.AddTransient<ISansRepository, SansRepository>();
builder.Services.AddTransient<IAdminRepository, AdminRepository>();
builder.Services.AddTransient<IMovieSansSalonRepository, MovieSansSalonRepository>();
// DbContext

IConfiguration Configuration = new ConfigurationManager();
builder.Services.AddDbContext<CinemaContext>(options=>
                    options.UseSqlServer(Configuration.GetConnectionString("CinemaContext")));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "/swagger/v1/swagger.json");

app.Run();
