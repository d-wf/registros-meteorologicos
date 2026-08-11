using System.Security.Claims;
using Duende.IdentityModel;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using HubMeteorologico.Api.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HubMeteorologico.Api;

public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly AppDbContext _dbContext;

    public ResourceOwnerPasswordValidator(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ValidateAsync(
        ResourceOwnerPasswordValidationContext context,
        CancellationToken ct
    )
    {
        // TODO: Buscar do banco
        if (
            !string.IsNullOrWhiteSpace(context.UserName)
            && !string.IsNullOrWhiteSpace(context.Password)
        )
        {
            var customResponse = new Dictionary<string, object>
            {
                { "UserData", new { Name = context.UserName } },
            };

            context.Result = new GrantValidationResult(
                subject: context.UserName,
                authenticationMethod: "password",
                claims: new List<Claim>
                {
                    new(JwtClaimTypes.Name, context.UserName),
                    new("UserData", JsonConvert.SerializeObject(new { Name = context.UserName })),
                },
                identityProvider: "local",
                customResponse: customResponse
            );
        }
        else
        {
            context.Result = new GrantValidationResult(
                TokenRequestErrors.InvalidGrant,
                "Credenciais inválidas."
            );
        }
    }
}
