using Microsoft.EntityFrameworkCore;

using MvcAspAzure.Domain.Data;
using MvcAspAzure.Domain.Entity;
using MvcAspAzure.Domain.Repository;
using MvcAspAzure.Infrastructure.Repository;


var builder = WebApplication.CreateBuilder(args);

//connect to DB
//builder.Services.AddDbContext<ShipmenDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection")));
builder.Services.AddDbContext<ShipmenDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDbConnection")));

//register repository
builder.Services.AddScoped(typeof(IRepositoryAsync<Cargo>), typeof(RepositoryAsync<Cargo>));
builder.Services.AddScoped(typeof(IRepositoryAsync<Driver>), typeof(RepositoryAsync<Driver>));
builder.Services.AddScoped(typeof(IRepositoryAsync<DriverTruck>), typeof(RepositoryAsync<DriverTruck>));
builder.Services.AddScoped(typeof(IRepositoryAsync<PlaceState>), typeof(RepositoryAsync<PlaceState>));
builder.Services.AddScoped(typeof(IRepositoryAsync<MvcAspAzure.Domain.Entity.Route>), typeof(RepositoryAsync<MvcAspAzure.Domain.Entity.Route>));
builder.Services.AddScoped(typeof(IRepositoryAsync<Shipment>), typeof(RepositoryAsync<Shipment>));
builder.Services.AddScoped(typeof(IRepositoryAsync<State>), typeof(RepositoryAsync<State>));
builder.Services.AddScoped(typeof(IRepositoryAsync<Truck>), typeof(RepositoryAsync<Truck>));
builder.Services.AddScoped(typeof(IRepositoryAsync<Warehouse>), typeof(RepositoryAsync<Warehouse>));
builder.Services.AddScoped(typeof(IRepositoryAsync<Contact>), typeof(RepositoryAsync<Contact>));

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

//TODO: Add CORS policy....?
builder.Services.AddCors(options => {
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

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
