using CourseService.DataAccess.DBContext;
using CourseService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using CourseService.Domain.Repository;
using CourseService.Domain.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
//register services
builder.Services.AddScoped<ICourseService,CourseServices>();

builder.Services.AddScoped<ICourseRepository,CourseRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Tutorial API",
        Version = "v1"
    });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tutorial API v1");
        c.RoutePrefix = string.Empty; // Swagger opens at root "/"
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
