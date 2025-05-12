using FluentValidation.AspNetCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using MvcAspAzure.Application.Warehouse.Commands.CreateShipmentValidator;
using MvcAspAzure.Application.Warehouse.Commands.CreateWarehouseValidator;
using MvcAspAzure.Application.Warehouse.Queries.GetAllWarehouses;
using MvcAspAzure.Application.Warehouse.Queries.GetWarehouseById;
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
builder.Services.AddScoped(typeof(IRepositoryAsync<State>), typeof(RepositoryAsync<State>));
builder.Services.AddScoped(typeof(IRepositoryAsync<Truck>), typeof(RepositoryAsync<Truck>));
builder.Services.AddScoped(typeof(IRepositoryAsync<Contact>), typeof(RepositoryAsync<Contact>));

builder.Services.AddScoped(typeof(IRepositoryAsync<Warehouse>), typeof(RepositoryAsync<Warehouse>));
builder.Services.AddScoped(typeof(IRepositoryAsync<Shipment>), typeof(RepositoryAsync<Shipment>));

builder.Services.AddScoped<GetAllWarehousesHandler>();
builder.Services.AddScoped<GetWarehouseByIdHandler>();


builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//add description
builder.Services.AddSwaggerGen(c => {
c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
    Title = "MvcAspAzure API",
    Version = "v1",
    Description = "ASP.NET Core Web API."
    });

    c.AddServer(new OpenApiServer { Url = "http://localhost:5000" });

    var filePath = Path.Combine(AppContext.BaseDirectory, "swagger", "swagger.yaml");
    c.IncludeXmlComments(filePath);

});

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddControllers()
        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateShipmentCommandValidator>())
        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateWarehouseCommandValidator>());

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


    //Middleware
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.yaml", "Warehouse & Shipment API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
