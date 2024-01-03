using BancoDoacaoSangue.API.Extensions;
using BancoDoacaoSangue.API.Filters;
using BancoDoacaoSangue.Application.Commands.CadastrarDoacao;
using BancoDoacaoSangue.Infra.Persistence;
using BancoDoacaoSangue.Infra.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<DoacaoBancoSangueContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);

builder.Services.AutoMapperConfig();
builder.Services.AddInfrastructure();

builder.Services.AddRefitClient<ICepService>().ConfigureHttpClient(c =>
{
    var urlApi = builder.Configuration["CepApi:Url"];
    c.BaseAddress = new Uri(urlApi);
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.JsonSerializationConfig();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidationFilter));
    options.Filters.Add(typeof(CustomExceptionFilter));
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CadastrarDoacaoCommand>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
