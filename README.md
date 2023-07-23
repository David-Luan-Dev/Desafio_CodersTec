## Aplicação de Exemplo: Movimentações financeiras das lojas.
Esta é uma aplicação de exemplo que combina o Angular e o .NET CORE 6.0 para criar uma calculadora de investimento em **(Movimentações financeiras das lojas)**. A aplicação começa com upload do arquivo que foi disponibilizado pelo desafio, chamado **CNAB.txt**. Apos o upload do arquivo, ela vai te dar um resumo das informações de todas as transações e tipo usada por cada loja, sera salvo no banco de dados e exibido na tabela, para vc ver com mais clareza. 

![](https://yourimageshare.com/ib/fqGBUQtvN0.webp)

### Tecnologias Utilizadas
**Angular:** um framework de desenvolvimento de aplicativos web em TypeScript.

**.NET CORE (6.0):** um framework de desenvolvimento para criar aplicativos Windows e web em C#.
### Visão Geral da Estrutura do Projeto
A aplicação é composta por duas partes principais: o front-end em Angular e o back-end em .NET CORE.

### Front-end (Angular)
A parte frontend da aplicação foi desenvolvida usando o Angular, um framework JavaScript baseado em TypeScript para construção de aplicações web modernas. Para efetuar as requisições HTTP, foi utilizado o Axios. Já para testes unitários, foi utilizado o Jasmine.

*src/app:* Esta pasta contém os componentes, serviços e modelos relacionados à lógica do frontend.

### Back-end (.NET CORE)
A parte backend da aplicação foi desenvolvida usando o .NET CORE, um framework de desenvolvimento robusto para criar aplicativos Windows e web usando a linguagem C#.

**Movimentacoes.Financeira.API:** contém as Controllers necessárias para cada requisição.

**MovimentacoesFinanceira.Application:** Camada responsável pelas regras de negócios. Nela tem as pastas, `ViewModels, InputModels e Services`, temos o modelo de entrada da aplicação e modelo de saida da aplicação, e a services, fica responsavel de se comunicar com a camada repository. A classe **FileService**, ela esta encarregada de fazer o parse do arquivo, atendendo o principio de responsabilidade unica, ela tem apenas uma responsabilidade. **TransacaoFinanceiraService.cs** como o nome já diz, ela é encarregada de fazer as transações da aplicação e aplicando as regras necessárias e se comunicando com a camada repository. Tem um método chamado **(SalvarTransacoes)**, com ele eu salvo todas as transações dessas lojas, e criei um metodo para salvar as lojas, que tramita pela aplicação e com isso dou um id para essas lojas, e consigo fazer um relacionamento com a tabela de transações.

**MovimentacoesFinanceira.Core:** Contém pastas e classes de modelo que representam os objetos de domínio da aplicação.
`Entities` --> Todas as entidades da apliacação, usando o entity framework, vai se criar as tabelas de acordos com essas entidades.

`Enums` --> tem um enum chamado **TipoTransacoesEnum**, os tipos de transações que a aplicação pode receber, e assim facilitar a manutenção do código.

`Notification` **Notification Pattern** --> Basicamente esse pattern nos ajuda a levar mensagens de domínio para a camada de apresentação, como por exemplo erros de validação ou qualquer outra mensagem que seja necessário mostrar ao usuário, já que normalmente a camada de apresentação não possui nenhum acesso direto à camada de domínio. Mas vale ressaltar que não fui muito afundo com esse pattern. Mais é muito importante para as aplicações.

E como não poderia faltar nesta camada, tem as Interfaces, costumo dizer os fieis contratos da nossa apliacação, pasta chamada `Repositories`, ela nos garanti o contrato da nossa implementação concreta para os nossos repositórios.

**MovimentacoesFinanceira.Infrastructure:** Camada responsavel de se comunicar com o banco de dados, banco de dados usado foi o `sql server`, usando o ORM `Entity Framework`. 

### Como Executar a Aplicação
1. Certifique-se de ter o Node.js e o Angular CLI instalados em seu ambiente de desenvolvimento.
2. Clone o repositório do projeto em sua máquina local através do comando `git clone https://github.com/diogomarv/B3`
3. Navegue para a pasta do projeto front-end (cd MovimentacoesFinanceira.Front-End) e execute o comando `npm install` ou `npm i` para instalar as dependências do projeto.
4. Execute o comando `npm start` para iniciar o servidor de desenvolvimento do Angular.
8. No back-end você precisa trocar a connection string, usando o banco de dados sql server. Local de alteração no arquivo, `appsettings.json`.
5. Antes de rodar o back-end, precisa usar os command para executar a migration. certifique-se que o projeto de inicialização `MovimentacoesFinanceira.Infrastructure`. comandos para rodar a migration, depois da connection string apontando para sua maquina local. **Add-Migration**: cria uma nova classe de migração de acordo com o nome especificado com os métodos Up()e Down().
**Update-Database**: Executa o último arquivo de migração criado pelo Add-Migration comando e aplica as alterações no esquema do banco de dados.
5. Navegue para a pasta do projeto back-end e execute o projeto usando sua IDE ou executando `dotnet run` no terminal.
6. Agora você pode acessar a aplicação em seu navegador em http://localhost:4200 e começar a usar inserindo o arquivo CNAB.txt
7. O back-end roda na porta 7201 (`https://localhost:7201`)

### Como executar os Testes Unitários?
**Back-end:** No Visual Studio, clique com o botão direito do mouse no projeto de testes e depois clique em "Run Tests". Certifique-se de efetuar um "Clean" antes de rodar o projeto. OBS: foi utilizado o padrão GWT (Given-When-Then) e AAA nos testes unitários.

**Front-end:** No Visual Studio Code ou editor de sua preferência, navegue até a pasta do projeto e digite `npm test`
### Observações
##### 1. Não foi utilizado IoC no projeto por conta do tamanho do mesmo. Também não foi utilizado nenhuma biblioteca para validação, como o FluentValidation.
##### Em projetos maiores o recomendável é seguir todas as práticas e padrões de projeto, incluindo libs para facilitar o desenvolvimento, porém em projetos menores, menos, pode ser mais.
