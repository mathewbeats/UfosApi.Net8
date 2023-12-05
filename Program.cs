using ApiUfoCasesNet8.Data;
using ApiUfoCasesNet8.UfoMapper;
using ApiUfoCasesNet8.UfoRepository;
using ApiUfoCasesNet8.UfoRepository.InterfacesUfos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});


//Agregar los repositorios a services de la clase Ufos y de la clase User

builder.Services.AddScoped<IUfosRepository, UfosRepository>(); 
builder.Services.AddScoped<IUsuariosUfoRepository, UsuariosUfoRepository>();

//AÑADIR EL MAPPER 

builder.Services.AddAutoMapper(typeof(UfoMapper));



var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta");

//aqui se configura la autenticacion

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false

    };
});




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Autenticacion en documentación

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "Autenticación JWT usando el esquema Bearer \r\n\r\n " +
        "Ingresa la palabrsa Bearer seguida de un [espacio] y despues su token en el capo de abajo \r\n\r\n" +
        "Ejemplo: \"Bearer tkdsjfjrrgfd\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                In = ParameterLocation.Header
            },
            new List<string>()

        }
    });
});



// Soporte para Cors
//Se pueden habilitar: 1-un dominio, 2-Multiples dominios,
//3-cualquier dominio(tener en cuenta seguridad)
//usamos de ejemplo el dominio: http//localhost:3223, se debe cambiar por el correcto
//se usa (*) para todos los dominios
builder.Services.AddCors(p => p.AddPolicy("PolicyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

//Soporte para CORS

app.UseCors("PolicyCors");

//habilitar la autenticacion

app.UseAuthentication();


//habilitar la authorizacion

app.UseAuthorization();

app.MapControllers();

app.Run();
