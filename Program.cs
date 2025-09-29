using Microsoft.EntityFrameworkCore;
using SQLCRUD.Data;
using Microsoft.OpenApi.Models;
using System.Reflection;
using SQLCRUD.Filters;
using SQLCRUD.Interfaces;
using SQLCRUD.Factories;
using SQLCRUD.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// Add API Controllers
builder.Services.AddControllers();

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Factory Pattern Services
builder.Services.AddScoped<IProductFactory, ProductFactory>();
builder.Services.AddScoped<ICategoryCrudFactory, CategoryCrudFactory>();
builder.Services.AddScoped<IProductService, ProductService>();

// Register Abstract Factory Pattern Services
builder.Services.AddScoped<IAbstractProductFactory, AbstractProductFactory>();
builder.Services.AddScoped<IElectronicsFactory, ElectronicsFactory>();
builder.Services.AddScoped<IFurnitureFactory, FurnitureFactory>();
builder.Services.AddScoped<IAbstractProductService, AbstractProductService>();

// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SQL CRUD API",
        Version = "v1",
        Description = "A comprehensive API for managing products with full CRUD operations",
        Contact = new OpenApiContact
        {
            Name = "SQL CRUD Team",
            Email = "support@sqlcrud.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Include XML comments for better documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Add example schemas
    c.SchemaFilter<ExampleSchemaFilter>();
});

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Enable Swagger in development
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SQL CRUD API v1");
        c.RoutePrefix = "api-docs"; // Swagger UI will be available at /api-docs
        c.DocumentTitle = "SQL CRUD API Documentation";
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
        c.DefaultModelsExpandDepth(-1);
        c.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Map API controllers
app.MapControllers();

// Map MVC controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
