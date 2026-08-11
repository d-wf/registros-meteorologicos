# Hub Meteorológico API

- API em .NET 10.0 Hub Meteorológico

## Estrutura

- .vscode/ → configurações do VS Code (launch.json, tasks.json)
- config/ → arquivos de configuração e .env
- src/ → código da API
- HubMeteorologico.Api.sln → solução .NET

## Pré-requisitos

- .NET 10.0
- PostgreSQL 16
- Postgis

## Configuração

- O arquivo .env dentro da pasta config contém variáveis para execução local (F5).

## Executar

- A API pode ser acessada via Swagger

## Observações

- Credenciais devem ser preenchidas para obter o token, porém usuario e senha não são validados.
- ClientId e ClientSecret válidos estão definidos no .env
