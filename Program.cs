using DotNetEnv;
using Gestao_Patrimonio.Applications.Services;
using Gestao_Patrimonio.Contexts;
using Gestao_Patrimonio.Interfaces;
using Gestao_Patrimonio.Repositories;
using GestaoPatrimonio.Repositores;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//carregando o .env
Env.Load();

////pegando a connetion string 
string connetionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

////conexao com o banco
builder.Services.AddDbContext<GestaoPatrimoniosContext>(options => options.UseSqlServer(connetionString));

// add services to the container.

builder.Services.AddControllers();
// learn more about configuring swagger/openapi at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Areas
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<AreaService>();

//Localizacoes
builder.Services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();
builder.Services.AddScoped<LocalizacaoService>();

//Cidades
builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();
builder.Services.AddScoped<CidadeService>();

//Bairro
builder.Services.AddScoped<IBairroRepository, BairroRepository>();
builder.Services.AddScoped<BairroService>();

//Usuario
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

// configure the http request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
