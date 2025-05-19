using Microsoft.EntityFrameworkCore;
using Wpm.Management.API;
using Wpm.Management.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IDataSeeder, DataSeeder>();

builder.Services.AddDbContext<ManagementDbContext>(config =>
{
    config.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ManagementDbContext>();
    await dbContext.Database.MigrateAsync();
    
    var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
    await dataSeeder.SeedAsync();
}

app.MapControllers();

app.Run();