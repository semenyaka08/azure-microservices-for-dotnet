using Wpm.Management.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IDataProvider, DataProvider>();

var app = builder.Build();

app.MapControllers();

app.Run();