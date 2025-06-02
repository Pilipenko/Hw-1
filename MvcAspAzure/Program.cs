using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MvcAspAzure.Application.Shipment.Queries.GetAllShipments;
using MvcAspAzure.Application.Shipment.Queries.GetShipmentById;
using MvcAspAzure.Application.Warehouse.Queries.GetAllWarehouses;
using MvcAspAzure.Application.Warehouse.Queries.GetWarehouseById;
using MvcAspAzure.Domain.Repository;
using FluentValidation.AspNetCore;
using MvcAspAzure.Application.Shipment.Commands.CreateShipment;
using MvcAspAzure.Application.Warehouse.Commands.CreateWarehouse;
using MvcAspAzure.Domain.Data;
using MvcAspAzure.Application.Warehouse.Commands.UpdateWarehouse;
using MvcAspAzure.Application.Warehouse.Commands.DeleteWarehouse;
using MvcAspAzure.Application.Services;
using MvcAspAzure.Application.Shipment.Commands.DeleteShipment;
using MvcAspAzure.Application.Shipment.Commands.UpdateShipment;
using MvcAspAzure.Infrastructure.Repository;
using MvcAspAzure.Application.Services.Interfaces;
using MvcAspAzure.Application.Services.Operations;

var builder = WebApplication.CreateBuilder(args);

// ---------- DB Context ----------
builder.Services.AddDbContext<ShipmenDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDbConnection")));
//builder.Services.AddDbContextFactory<ShipmenDbContext>();
// ---------- Repositories ----------
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();

// ---------- Services ----------
builder.Services.AddScoped<ShipmentService>();
builder.Services.AddScoped<WarehouseService>();

builder.Services.AddScoped<IWarehouseOperations, WarehouseOperations>();
builder.Services.AddScoped<IShipmentOperations, ShipmentOperations>();

// ---------- Shipment Commands ----------
builder.Services.AddScoped<CreateShipmentCommandHandler>();
builder.Services.AddScoped<CreateShipmentCommandValidator>();

builder.Services.AddScoped<DeleteShipmentCommandHandler>();

builder.Services.AddScoped<UpdateShipmentCommandHandler>();

// ---------- Shipment Queries ----------
builder.Services.AddScoped<GetAllShipmentsHandler>();
builder.Services.AddScoped<GetShipmentByIdHandler>();

// ---------- Warehouse Commands ----------
builder.Services.AddScoped<CreateWarehouseCommandHandler>();
builder.Services.AddScoped<CreateWarehouseCommandValidator>();

builder.Services.AddScoped<DeleteWarehouseCommandHandler>();

builder.Services.AddScoped<UpdateWarehouseCommandHandler>();

// ---------- Warehouse Queries ----------
builder.Services.AddTransient<GetAllWarehousesHandler>();
builder.Services.AddTransient<GetWarehouseByIdHandler>();



// ---------- Swagger ----------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "MvcAspAzure API",
        Version = "v1",
        Description = "ASP.NET Core Web API."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath)) {
        c.IncludeXmlComments(xmlPath);
    }
});

// ---------- Fluent Validation ----------
builder.Services.AddControllers()
    .AddFluentValidation(fv => {
        fv.RegisterValidatorsFromAssemblyContaining<CreateShipmentCommandValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<CreateWarehouseCommandValidator>();
    });

// ---------- CORS ----------
builder.Services.AddCors(options => {
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// ---------- Middleware ----------
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthorization();

// ---------- Swagger UI ----------
app.UseSwagger(c => {
    c.PreSerializeFilters.Add((swaggerDoc, httpReq) => {
        if (httpReq.Query.ContainsKey("tokenizeHost")) {
            swaggerDoc.Servers = new List<OpenApiServer> {
                new OpenApiServer { Url = "${serviceHost}" }
            };
        }
    });
});

app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/swagger.json", "MvcAspAzure API V1");
});

// ---------- Controller Mapping ----------
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



//http://localhost:5097/swagger