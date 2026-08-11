using System.Text;
using System.Text.Json.Serialization;
using Duende.IdentityServer.Models;
using HubMeteorologico.Api;
using HubMeteorologico.Api.Clients;
using HubMeteorologico.Api.Config;
using HubMeteorologico.Api.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);
var appConfig = builder.Configuration.GetSection("App").Get<AppConfig>() ?? new AppConfig();

if (appConfig.Database == null)
    throw new Exception("Parâmetro DataBase não configurado");

if (string.IsNullOrWhiteSpace(appConfig.Database.ConnectionString))
    throw new Exception("Parâmetro DataBase > ConnectionString não configurado");

if (appConfig.Auth == null)
    throw new Exception("Parâmetro Auth não configurado");

if (appConfig.Auth.Clients == null || appConfig.Auth.Clients.Count == 0)
    throw new System.Exception("Parâmetro Auth > Clients não configurado");

foreach (var c in appConfig.Auth.Clients)
{
    if (string.IsNullOrWhiteSpace(c.Value.ClientId))
        throw new System.Exception($"Parâmetro ClientId não configurado para {c.Key}");
    if (string.IsNullOrWhiteSpace(c.Value.ClientSecret))
        throw new System.Exception($"Parâmetro ClientSecret não configurado para {c.Key}");
}

builder.Services.AddSingleton<IAuthConfig>(appConfig.Auth);

builder.Services.AddSingleton<IAuthorizationPolicyProvider, DynamicPolicyProvider>();
builder.Services.AddScoped<IEstacaoClient, EstacaoClient>();

builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(appConfig.Database.ConnectionString, o => o.UseNetTopologySuite());

#if DEBUG
    optionsBuilder.EnableSensitiveDataLogging();
#endif
});

var apiScopes = appConfig.Auth.Scopes.Select(p => new ApiScope(
    name: p.Name,
    displayName: p.Display
));

var apiResources = new List<ApiResource>
{
    new ApiResource("hubMeteorologico", "Hub Meteorologico")
    {
        Scopes = apiScopes.Select(s => s.Name).ToList(),
    },
};

builder
    .Services.AddIdentityServer(options =>
    {
        options.EmitStaticAudienceClaim = false;
        options.IssuerUri = appConfig.Auth.Issuer;
    })
    .AddInMemoryApiScopes(apiScopes)
    .AddInMemoryApiResources(apiResources)
    .AddInMemoryClients(
        appConfig.Auth.Clients.Select(p => new Client()
        {
            ClientId = p.Value.ClientId,
            ClientSecrets = { new Secret(p.Value.ClientSecret.Sha512()) },
            AllowedGrantTypes = { GrantType.ResourceOwnerPassword, GrantType.ClientCredentials },
            AllowedScopes = apiScopes.Select(a => a.Name).ToList(),
            AccessTokenLifetime = 3600,
            AbsoluteRefreshTokenLifetime = 0,
            SlidingRefreshTokenLifetime = 3600,
            RefreshTokenUsage = TokenUsage.ReUse,
            RefreshTokenExpiration = TokenExpiration.Sliding,
            UpdateAccessTokenClaimsOnRefresh = true,
            AllowOfflineAccess = true,
        })
    )
    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
    .AddDeveloperSigningCredential();

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = appConfig.Auth.Issuer;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
        };
    });

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.WriteIndented = false;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var swaggerScopes = appConfig.Auth.Scopes.ToDictionary(s => s.Name, s => s.Display ?? s.Name);

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition(
        "oauth2",
        new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                Password = new OpenApiOAuthFlow
                {
                    TokenUrl = new Uri("/connect/token", UriKind.Relative),
                    RefreshUrl = new Uri("/connect/token", UriKind.Relative),
                    Scopes = swaggerScopes,
                },
            },
        }
    );

    c.AddSecurityRequirement(
        (document) =>
            new OpenApiSecurityRequirement()
            {
                [new OpenApiSecuritySchemeReference("oauth2", document)] = appConfig
                    .Auth.Scopes.Select(i => i.Name)
                    .ToList(),
            }
    );

    var xmlPath = Path.Combine(AppContext.BaseDirectory, "HubMeteorologico.Api.xml");
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddAuthorization(options =>
{
    if (appConfig.Auth?.Policies != null)
    {
        foreach (var p in appConfig.Auth.Policies)
        {
            options.AddPolicy(
                p.Policy,
                policy =>
                {
                    policy.RequireAuthenticatedUser();

                    if (p.Scope != null && p.Scope.Any())
                    {
                        policy.RequireClaim("scope", p.Scope.ToArray());
                    }
                }
            );
        }
    }
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
