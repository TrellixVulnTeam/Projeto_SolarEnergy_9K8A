using SolarEnergy.Api.Data;
using Microsoft.OpenApi.Models;
using SolarEnergy.Infra.DataBase.Repositories;
using SolarEnergy.Domain.Interfaces.Repositories;
using SolarEnergy.Domain.Interfaces.Services;
using SolarEnergy.Domain.Services;
using SolarEnergy.Api.Config;
using System.Text;
using SolarEnergy.Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SolarDbContext>();

builder.Services.AddScoped<IUnidadeRepository, UnidadeRepository>();
builder.Services.AddScoped<IGeracaoRepository, GeracaoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnidadeService, UnidadeService>();
builder.Services.AddScoped<IGeracaoService, GeracaoService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();

var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options => 
{
    options.SwaggerDoc("v1", 
        new OpenApiInfo
        {
            Title = "SolarEnergy.Api",
            Version = "v1",
            Description = "Api desenvolvida para a Aplicação Solar Energy!",
            Contact = new OpenApiContact
            {
                Name = "Edmilson Gomes",
                Url = new Uri("https://github.com/edmilsondmx"),
                Email = "edmilsondmx@gmail.com",
            }
        });
});

builder.Services.AddCors(options => 
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy => 
        {
            policy.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ErrorMiddleware>();

app.Run();
