//backend.Program.cs
using backend.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;

// Cria o builder da aplicação web
var builder = WebApplication.CreateBuilder(args);

// Inicializa os repositórios
var transacaoRepo = new TransacaoRepository();
var usuarioRepo = new UsuarioRepository(transacaoRepo);

// Registra os repositórios como serviços singleton
builder.Services.AddSingleton(usuarioRepo);
builder.Services.AddSingleton(transacaoRepo);

// Adiciona suporte a controllers
builder.Services.AddControllers();

// Configura o Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();

// Configura a política de CORS para permitir qualquer origem, método e cabeçalho
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Configura o Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Gestão de Gastos",
        Version = "v1",
        Description = "API de Gestão de Usuários e Transações Financeiras.",
        Contact = new OpenApiContact
        {
            Name = "Equipe de Desenvolvimento",
            Email = "samueel.oliveira@gmail.com"
        }
    });

// Adiciona suporte a comentários XML
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
c.IncludeXmlComments(xmlPath);
});


// Constrói a aplicação
var app = builder.Build();

// Habilita o middleware do Swagger apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Controle de Gastos v1");
    });
}

// Aplica a política de CORS
app.UseCors("CorsPolicy");

// Habilita a autorização
app.UseAuthorization();

// Mapeia os controllers
app.MapControllers();

// Inicia a aplicação
app.Run();