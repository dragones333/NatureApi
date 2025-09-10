using Microsoft.EntityFrameworkCore;
using NatureAPI.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("NatureDb")
                       ?? "Server=localhost,1434;Database=NatureDb;User Id=sa;Password=Juande..10;TrustServerCertificate=True;";

builder.Services.AddDbContext<NatureDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NatureDbContext>();
    db.Database.Migrate();
}

app.Run();