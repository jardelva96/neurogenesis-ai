using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NeuroGenesisAI.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Habilita Controllers (para seu BrainController funcionar)
builder.Services.AddControllers();

// Registra o serviço de simulação automática do cérebro
builder.Services.AddHostedService<BrainSimulationService>();

// Libera CORS para permitir que o Frontend acesse a API
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Ativa CORS
app.UseCors();

// Mantém HTTPS redirection
app.UseHttpsRedirection();

// Mapeia os Controllers
app.MapControllers();

app.Run();
