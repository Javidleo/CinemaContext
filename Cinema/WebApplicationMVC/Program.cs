using DataAccess;
using DataAccess.Repository;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;
using UseCases.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
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

builder.Services.AddSqlServer<CinemaContext>("Server=DESKTOP-MONHQ70;Database=bookdb;Trusted_Connection=True;");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
