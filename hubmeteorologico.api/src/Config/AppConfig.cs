namespace HubMeteorologico.Api.Config;

/// <summary>
/// Classe com todos os parâmetros da aplicação
/// </summary>
/// <remark>
/// Estrutura em variáveis de ambiente:
///
/// App__Database__ConnectionString=#string#
/// </remark>
public class AppConfig
{
    public DatabaseConfig Database { get; set; } = null!;

    /// <summary>
    /// Configuração de authorization
    /// </summary>
    /// <value></value>
    public AuthConfig Auth { get; set; } = null!;
}

public class DatabaseConfig
{
    public string ConnectionString { get; set; } = null!;
}

public class AuthConfig : IAuthConfig
{
    /// <summary>
    /// Issuer é o originador do JWT
    /// </summary>
    /// <remark>
    /// Estrutura em variáveis de ambiente:
    ///
    /// App__Auth__Issuer=#string#
    ///
    /// </remark>
    public string Issuer { get; set; } = null!;

    /// <summary>
    /// Audience é o público do JWT
    /// </summary>
    /// <remark>
    /// Estrutura em variáveis de ambiente:
    ///
    /// App__Auth__Audience=#string#
    ///
    /// </remark>
    public string Audience { get; set; } = null!;

    /// <summary>
    /// Key é a chave para assinar/validar integridade do JWT
    /// </summary>
    /// <remark>
    /// Estrutura em variáveis de ambiente:
    ///
    /// App__Auth__Key=#string#
    ///
    /// </remark>
    public string Key { get; set; } = null!;

    /// <summary>
    /// Lista de Clients
    /// </summary>
    /// <remark>
    /// Estrutura em variáveis de ambiente:
    ///
    /// App__Auth__Clients=#string#
    ///
    /// </remark>
    public Dictionary<string, ClientConfig> Clients { get; set; } = null!;

    /// <summary>
    /// Lista de scopes
    /// </summary>
    public List<ScopesConfig> Scopes { get; set; } = null!;

    /// <summary>
    /// Nome da policy
    /// </summary>
    /// <remark>
    /// Estrutura em variáveis de ambiente:
    ///
    /// App__Auth__Policies__1__Policy=#string#
    ///
    /// </remark>
    /// <summary>
    /// Lista de politicas
    /// </summary>
    public List<PoliciesConfig> Policies { get; set; } = null!;
}

public class ClientConfig
{
    /// <summary>
    /// Client Id utilizado para autorização
    /// </summary>
    /// <remark>
    /// Estrutura em variáveis de ambiente:
    ///
    /// App__Auth__ClientId=#string#
    ///
    /// </remark>
    public string ClientId { get; set; } = null!;

    /// <summary>
    /// Client Secret utilizado para autorização
    /// </summary>
    /// <remark>
    /// Estrutura em variáveis de ambiente:
    ///
    /// App__Auth__ClientSecret=#string#
    ///
    /// </remark>
    public string ClientSecret { get; set; } = null!;
}

public class ScopesConfig
{
    /// <summary>
    /// Nome dos scopes
    /// </summary>
    /// <remark>
    /// Estrutura em variáveis de ambiente:
    ///
    /// App__Auth__Scopes__0__Name=#string#
    ///
    /// </remark>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Descrição dos scopes
    /// </summary>
    /// <remark>
    /// Estrutura em variáveis de ambiente:
    ///
    /// App__Auth__Scopes__0__Display=#string#
    /// </remark>
    public string Display { get; set; } = null!;
}

public class PoliciesConfig
{
    public string Policy { get; set; } = null!;

    /// <summary>
    /// Definição dos scopes que irão ser validados
    /// </summary>
    /// <remark>
    /// Estrutura em variáveis de ambiente:
    ///
    /// App__Auth__Policies__1__Scope__0=#string#
    /// </remark>
    public List<string> Scope { get; set; } = null!;
}

/// <summary>
/// IAuthConfig
/// </summary>
public interface IAuthConfig
{
    string Issuer { get; }

    string Audience { get; }

    string Key { get; }
    Dictionary<string, ClientConfig> Clients { get; }

    List<ScopesConfig> Scopes { get; }

    List<PoliciesConfig> Policies { get; }
}
