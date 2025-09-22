using Backend.Data;
using Backend.Models;
using Backend.Services;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Configurations
// -------------------------
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>();

// -------------------------
// Database
// -------------------------
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -------------------------
// Services
// -------------------------
builder.Services.AddSingleton<TokenService>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TenantService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddSingleton<IPasswordHasher<Backend.Models.User>, PasswordHasher<Backend.Models.User>>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TenantService>();


// ✅ Register IPasswordHasher correctly
// builder.Services.AddScoped<IPasswordHasher<Backend.Models.User>, PasswordHasher<Backend.Models.User>>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// -------------------------
// Multi-Tenancy
// -------------------------
builder.Services.AddMultiTenant<Backend.Models.TenantInfo>()
    .WithInMemoryStore()
    .WithBasePathStrategy();

// -------------------------
// JWT Authentication
// -------------------------
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                // Optional: Attach TenantId from JWT to HttpContext
                var tenantIdClaim = context.Principal?.FindFirst("TenantId");
                if (tenantIdClaim != null && int.TryParse(tenantIdClaim.Value, out var tenantId))
                {
                    context.HttpContext.Items["TenantId"] = tenantId;
                }
                return Task.CompletedTask;
            }
        };
    });

// -------------------------
// Controllers
// -------------------------
builder.Services.AddControllers();

// -------------------------
// Swagger (with JWT support)
// -------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MultiTenant API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// -------------------------
// CORS (React frontend)
// -------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// -------------------------
// Migrations & Seeding
// -------------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    // Optional: Seed initial users if needed
    // DbSeeder.Seed(db);
}

// -------------------------
// Middleware Order
// -------------------------
app.UseCors("AllowFrontend");
app.UseMultiTenant();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MultiTenant API V1");
});

app.MapControllers();
app.Run();
