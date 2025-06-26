using API.Middleware;
using Core.Entities;
using Core.Interfaces;
using Infastructure.Data;
using Infastructure.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register StoreContext as a service (For DI)
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

// Register ProductRepository as a Service (For DI)
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Register IGenericRepository as a Service (For DI)
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddCors();
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
    var connString = builder.Configuration.GetConnectionString("Redis") ?? throw new Exception("Can not get Redis connection string");
    var configuration = ConfigurationOptions.Parse(connString, true);
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddSingleton<ICartService, CartService>();

// Configure Identity with AppUser and StoreContext
builder.Services.AddAuthorization();                  // adds authorization services to the service
builder.Services.AddIdentityApiEndpoints<AppUser>().AddEntityFrameworkStores<StoreContext>();   // AddIdentityApiEndpoints<AppUser>() implicitly call AddAuthentication()

var app = builder.Build();

// Middleware (Order does matter)

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();           // Handling exceptions in the middleware

// Allow headers, methods, credentials(cookies) from the following domains
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200", "https://localhost:4200"));

// Enforce authentication and authorization for requests which needs authentication
app.UseAuthentication();
app.UseAuthorization();

// End of Middleware 

app.MapControllers();                               // Make available the Endpoints in the controllers for the HTTP requests

app.MapGroup("api").MapIdentityApi<AppUser>();      // register the Endpoints "STARTING with API" given from the Identity model for the AppUser : /api/login


try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();      // checks the latest migration and the migration addedd to the DB,
                                                // compares them and if the latest migration didnt applied to the current DB,
                                                // then automatically add the latest migration to the DB
    await StoreContextSeed.SeedAsync(context);     // seed data if the tables are empty
}
catch (Exception ex)
{
    System.Console.WriteLine(ex);
    throw;
}


app.Run();
