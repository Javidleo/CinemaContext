using DataAccess;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;
using UseCases.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

//IConfiguration Configuration = new ConfigurationManager();
//builder.Services.AddDbContext<CinemaContext>(options =>
//                    options.UseSqlServer(Configuration.GetConnectionString("CinemaContext")));
builder.Services.AddDbContext<CinemaContext>(option => option.UseSqlServer("Server=.;Database=Cinema;Trusted_Connection=True;"));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
