using DevStreamAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//configurar a conex�o com o banco de dados
// Configurar a conex�o com o banco de dados
builder.Services.AddDbContext<DevSteamAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//configurar o CORS
builder.Services.AddCors(Options =>
{
    Options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Adionar o Swagger com JWT Bearer
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
    {
        new OpenApiSecurityScheme
        {
        Reference = new OpenApiReference
            {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,

        },
        new List<string>()
        }
    });
});

// Servi�o de EndPoints do Identity Framework
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
})
    .AddEntityFrameworkStores<DevSteamAPIContext>()
    .AddDefaultTokenProviders(); // Adicionando o provedor de tokens padr�o

//add servi�o de autentica��o e autoriza��o
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();



var app = builder.Build();
//sweager em ambiente de produ��o
app.UseSwagger();
app.UseSwaggerUI();

// mapear os endpoints padr�o do identy framework
app.MapGroup("/Users").MapIdentityApi<IdentityUser>();
app.MapGroup("/Roles").MapIdentityApi<IdentityRole>();


app.UseHttpsRedirection();
//permitir a autentica��o e autoriza��o de qualquer origem
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
