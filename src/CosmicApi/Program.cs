using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.FileProviders;
using CosmicApi.Api.Configurations;
using CosmicApi.Application.MappingProfiles;
using CosmicApi.Configurations;
using CosmicApi.Infrastructure.Services;
using CosmicApi.Infrastructure.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();

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

var app = builder.Build();

// set BaseUrl value for picture mapping (todo : find better way)
var baseUrl = app.Configuration.GetSection("BaseUrl");
AppDomain.CurrentDomain.SetData("BaseUrl", baseUrl.Value);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "StaticFiles")),
    RequestPath = ""
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfig();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.InitDatabase();
await app.RunAsync();
