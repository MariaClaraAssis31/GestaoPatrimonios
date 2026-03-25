using DotNetEnv;
using Gestao_Patrimonio.Applications.Services;
using Gestao_Patrimonio.Contexts;
using Gestao_Patrimonio.Interfaces;
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
