var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*
Step 1: Install the Microsoft.AspNetCore.Cors package
dotnet add package Microsoft.AspNetCore.Cors

Step 2: Add CORS services in the ConfigureServices method
public void ConfigureServices(IServiceCollection services)
*/





builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

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
app.Run();
