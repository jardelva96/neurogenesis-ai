using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Habilita Controllers (para seu BrainController funcionar)
builder.Services.AddControllers();

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
