
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Multi-Tenant
builder.Services.AddMultiTenant<TenantInfo>()
    .WithInMemoryStore()
    .WithBasePathStrategy();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MultiTenant API", Version = "v1" });
});

var app = builder.Build();

app.UseMultiTenant();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MultiTenant API V1");
});

app.MapControllers();
app.Run();