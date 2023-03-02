using CosmicApi.Api.Configurations;
using CosmicApi.Application.MappingProfiles;
using CosmicApi.Configurations;
using CosmicApi.Infrastructure.Common;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();

builder.Services.AddMediatRConfig();
builder.Services.AddPersistenceSetup(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.Configure<TokenConfiguration>(builder.Configuration.GetSection("TokenConfiguration"));
builder.Services.ConfigureAuthentication();
var key = Encoding.UTF8.GetBytes(builder.Configuration["TokenConfiguration:Secret"]);
var tokenValidationParams = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = false,
    RequireExpirationTime = false,
    ClockSkew = TimeSpan.Zero,

};

builder.Services.AddSingleton(tokenValidationParams);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


var app = builder.Build();

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