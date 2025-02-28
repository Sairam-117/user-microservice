using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Core.Mappers;
using eCommerce.Infrastructure;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);


//KeyVault
var keyVaultUrl = Environment.GetEnvironmentVariable("KeyVaultUrl");

var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

var secret = await client.GetSecretAsync("microcourse-dev-db-conn-string");
var connectionString = secret.Value.Value.Length > 0 ? secret.Value.Value : builder.Configuration.GetConnectionString("DefaultConnection");
builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;


// Add Services
builder.Services.AddInfrastructure();
builder.Services.AddCore();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Add Controller
builder.Services.AddControllers().AddJsonOptions
    (options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// AutoMapper Config
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

//FluentValidation
builder.Services.AddFluentValidationAutoValidation();

//Add API Explorer Services
builder.Services.AddEndpointsApiExplorer();

// Add Swagger Generation services to create swagger specification
builder.Services.AddSwaggerGen();

//Add cors services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("*")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

//Exception Handling
app.UseExceptionHandlingMiddleware();

//Routing
app.UseRouting();

//Add Endpoint that can service swagger.json
app.UseSwagger();

//Add swagger UI for interactive page for api testing
app.UseSwaggerUI();

app.UseCors();

//Auth
app.UseAuthentication();
app.UseAuthorization();

//Controller routes
app.MapControllers();

app.Run();
