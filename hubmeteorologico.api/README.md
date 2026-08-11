### 1. [Desenvolvimento]

> Crie um projeto que contenha uma API funcional para que um frontend busque
> os registros interpolados.
> Vários usuários podem consumir essa informação ao mesmo tempo.
> Todos os registros filtrados precisam ser entregues ao frontend.
> Utilize a linguagem .NET e as ferramentas que achar necessário.
> Utilize também o dump de dados fornecido.
> Parâmetros: Fazenda, Lavoura e Data/Hora.

- Adicionado AddResponseCompression para reduzir tamanho das requisições.
- Query sem rasteramento de memória: AsNoTracking()
- Criado índice nos campos utilizados na consulta:
  CREATE INDEX "idx_RegistrosInterpolados_busca"
  ON "RegistrosInterpolados" ("FazendaId", "MapaFazendaLavouraId", "DataHora");

### 2. [Arquitetura]

> Descreva detalhadamente a estratégia que você usaria para desenvolver um
> endpoint de busca e persistência de dados externos (dados das estações de
> terceiros ao banco de dados):

Ex de GET http://localhost:5000/registros-
meteorológicos?Equipamento=X&DataHora=Y

- Criar um client ("EstacaoClient" ou algo do tipo) pra acessar a api e retornar os dados conforme os parâmetros.
- Criar uma controller ("RegistrosMeteorologicosController") com um endpoint que recebe os parâmetros, consulta os dados via client.
- Faz validações e consultas necessárias em outras tabelas e persiste os dados na tabela RegistrosMeteorologicos.
- Criar um worker que acesse periodicamente (configurado) esse endpoint para registrar os dados.

### 3. [Arquitetura]

> Descreva como você modelaria a atualização de dados cadastrais dos
> equipamentos no domínio da aplicação.
> Também descreva quais entidades você usaria e o porquê.
> Regra: Existem Estações Meteorológicas e Pluviômetros. Apenas Estações
> podem ter o nome alterado.

- Criando na controller Equipamento e um endpoint para cada tipo de equipamento, onde uma classe para cada tipo usada como request, a classe teria os atritutos originais da entidade Equipamento.
- Dessa forma atendendo a possibilidade de alterar o nome ou não conforme o tipo do equipamento. Poderia ainda ser utilizada uma service ou mesmo um serviço para reutilizar determinados trechos do código.
- Não ficou claro quais dados além do Nome, poderiam ou não serem atualizadis, talvez ficando incompleta a funcionalidade como um todo.

### 4. [Arquitetura]

> Identifique e descreva os pontos críticos relacionados à performance,
> escalabilidade, e retenção de dados desse sistema que possui batchs recorrentes
> e alta demanda de leitura/escrita.

- O maior limitador tende a ser o banco, que deve ser otimizado.
- Identificar e/ou criar índices nas tabelas.
- Validar integridade dos dados entre os relacionamentos das tabelas.
- Inserir dados via BulkInsert (EF Core ou outros conforme perforamance).
- Utilizar mecanismos para tornar o processamento assíncrono (workers ou filas de mensageria).
- Avaliar quais dados pode utilizar cache.
- Consolidar e arquivar dados antigos em tabelas específicas, mantendo menos volume nas tabelas originais.
- Ainda possível ter alguma melhoria de leitura mudando para Go com gRPC/Protobuf (serialização em binário e HTTP/2) e em chamadas simultâneas onde há alta concorrência.

### 5. [Arquitetura]

> Descreva como você implementaria a autenticação e autorização nas APIs
> utilizando o protocolo OAuth.

Na api foi feito da seguinte forma para não elevar a complexidade e tempo desenvolvimento:

- Utilizando o IdentityServer para gerenciar credenciais e gerar token JWT.
- Validação do token utilizando Microsoft.AspNetCore.Authentication.JwtBearer.
- Aplicação de informações de clients, scopes e policies definidas no .env da aplicação.
- Em um cenário de desenvolvimento real separar uma api para auth e utilizar nas demais apis.

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
