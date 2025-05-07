using MyApp.Infrastructure.DependencyInjection;
using MyApp.Application.DependencyInjection;
using MyApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MyApp.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Use Middleware for error handling, authentication, and authorization
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();



app.Run();

