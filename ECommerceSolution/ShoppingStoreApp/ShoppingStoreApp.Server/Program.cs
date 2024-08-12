using ShoppingStoreApp.Server.Entities;
using ShoppingStoreApp.Server.Repositories.Interfaces;
using ShoppingStoreApp.Server.Repositories;
using ShoppingStoreApp.Server.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*
Step 1: Install the Microsoft.AspNetCore.Cors package
dotnet add package Microsoft.AspNetCore.Cors

Step 2: Add CORS services in the ConfigureServices method
public void ConfigureServices(IServiceCollection services)
*/

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddFile("logs/catalog-{Date}.json", isJson: true);
});

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddScoped<IProductRepository, ProductRepositoryMySql>();
builder.Services.AddScoped<IProductService, ProductService>();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");


// .NET Core Allow CORS methods at a glance:
app.UseCors(builder => builder.AllowAnyOrigin()); // Allow requests from any origin
//app.UseCors(builder => builder.WithOrigins("http://domain.com")); // Allow requests only from domain.com
app.UseCors(builder => builder.AllowAnyHeader()); // Allow any header in the request
app.UseCors(builder => builder.AllowAnyMethod()); // Allow any HTTP method in the request


app.MapGet("/api/employees", () =>
{
    List<Employee> employees = new List<Employee>();

    employees.Add(new Employee { Id = 12, FirstName = "Ravi", LastName = "Tambade" });
    employees.Add(new Employee { Id = 13, FirstName = "Raji", LastName = "Patil" });
    employees.Add(new Employee { Id = 14, FirstName = "Seema", LastName = "More" });
    return Results.Ok(employees);
});

app.MapGet("/api/employees/{id}", (int id) =>
{
    bool status = true;
    var emp = new Employee { Id = id, FirstName = "Ravi", LastName = "Tambade" };
    if (status)
    {
        return Results.Ok(emp);
    }
    return Results.NotFound();
});

app.MapPost("/api/employees", (Employee emp) =>
{
    List<Employee> employees = new List<Employee>();
    employees.Add(emp);
    return Results.Created($"/employees/{emp.Id}", emp);
});

app.MapPut("/api/employees/{id}", (int id, Employee emp) =>
{
    var existingEmp = new Employee { Id = id, FirstName = "Ravi", LastName = "Tambade" }; ;
    if (existingEmp is null) return Results.NotFound();
    existingEmp.FirstName = emp.FirstName;
    existingEmp.LastName = emp.LastName;
    return Results.NoContent();
});

app.MapDelete("/api/employees/{id}", (int id) =>
{
    bool status = false;
    if (status)
    {
        return Results.Ok();
    }
    return Results.NotFound();
});


app.Run();
