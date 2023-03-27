using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using CosmicApi.Api.Configurations;
using CosmicApi.Application.MappingProfiles;
using CosmicApi.Configurations;
using CosmicApi.Infrastructure.Services;
using CosmicApi.Api.Common;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<ValidationErrorResultFilter>();
    }).AddValidation();

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
// add swagger
builder.Services.AddSwaggerConfig();
// add health checks
builder.Services.AddHealthChecks();
// add MediatR
builder.Services.AddMediatRConfig();
// configure persistence
builder.Services.ConfigurePersistence(builder.Configuration);
// add automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
// configure authentication
builder.Services.ConfigureAuthentication(builder.Configuration);
// configure authorization
builder.Services.ConfigureAuthorization(builder.Configuration);

builder.Services.AddTransient<IPictureService, PictureService>();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// logging 
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


var app = builder.Build();

// set BaseUrl value for picture mapping
var baseUrl = app.Configuration.GetSection("BaseUrl");
AppDomain.CurrentDomain.SetData("BaseUrl", baseUrl.Value);

// Configure static files 
app.UseStaticFiles(builder.Environment);

app.UseSwaggerConfig();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseHealthChecks();
app.MapControllers();

await app.InitDatabase(app.Logger);
await app.RunAsync();
