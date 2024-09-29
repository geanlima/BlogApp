BlogApp 

    BlogApp é uma aplicação web desenvolvida com ASP.NET Core para gerenciamento de postagens de blog com suporte para usuários autenticados. A aplicação utiliza Entity Framework Core para persistência de dados, JWT para autenticação e WebSockets para notificações em tempo real. O projeto implementa os princípios de arquitetura limpa e faz uso de AutoMapper para o mapeamento entre entidades e DTOs. 

Funcionalidades 

    Criação de posts: Usuários autenticados podem criar posts com título, conteúdo e autor. 

    Autenticação com JWT: Autenticação baseada em JWT para garantir a segurança das rotas. 

    Notificações em tempo real: Envio de notificações para todos os usuários conectados via WebSocket quando um novo post é criado. 

    Mapeamento de entidades: Utiliza AutoMapper para mapear entidades de domínio para DTOs. 

    Gerenciamento de usuários: Cadastro e gerenciamento de usuários, incluindo o nome de usuário, e-mail e hash de senha. 



Tecnologias Utilizadas 

    ASP.NET Core: Framework web para o backend. 

    Entity Framework Core: ORM para acesso ao banco de dados. 

    AutoMapper: Biblioteca para mapeamento de objetos (entidades e DTOs). 

    JWT: Autenticação segura usando JSON Web Tokens. 

    WebSockets: Comunicação em tempo real entre cliente e servidor. 

    SQL Server: Banco de dados utilizado para persistência dos dados. 

 

Arquitetura 

    A aplicação segue os princípios da arquitetura limpa, dividindo responsabilidades em camadas: 

    Domain: Contém as entidades e regras de negócio. 

    Application: Contém os serviços e DTOs. 

    Infrastructure: Contém os repositórios, WebSockets, e serviços de infraestrutura como JWT. 

    Presentation: Contém os controladores (API) que expõem os endpoints. 

    Instalação e Configuração 


Pré-requisitos 

    .NET 6 SDK 
    SQL Server 
 

Passos para rodar a aplicação 

    Clone o repositório: 

    https://github.com/geanlima/BlogApp.git 

    cd blogapp  

Configure a string de conexão no arquivo appsettings.Development.json:

    {
    "ConnectionStrings": {
        "DefaultConnection": "Server=GLDEVELOPER\\SQLEXPRESS01;Database=BlogAppDb;User Id=sa;Password=Ge@n7514;TrustServerCertificate=true"
    },
    "Jwt": {
        "SecretKey": "wsRK9vn$%569pLadVT9!@#%$#%3312VvTY",
        "Issuer": "GSL",
        "Audience": "http://www.gsl.com.br"
    },
    "Logging": {
        "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
        }
    }
    }

Execute as migrações para criar o banco de dados:  

    dotnet ef database update  

Execute a aplicação: 

    dotnet run 
 
A aplicação estará disponível em: 

http://localhost:5000  para HTTP. 

https://localhost:5001  para HTTPS. 


Endpoints

Autenticação

POST /api/auth/login: Autentica o usuário e retorna um token JWT.

{
  "username": "exampleuser",
  "password": "examplepassword"
}

Posts

    GET /api/posts: Retorna todos os posts.

    POST /api/posts: Cria um novo post (autenticado).

    PUT /api/posts/{id}: Edita um post existente (autenticado).

    DELETE /api/posts/{id}: Exclui um post existente (autenticado).
    
    WebSocket
    ws://localhost:5000/ws: Endereço para se conectar ao WebSocket. Notificações serão enviadas em tempo real para todos os clientes conectados quando um novo post for criado.

WebSocket

    Na raiz da aplicacao existe um arquivo chamado websocket-test.html, esse arquivo deve ser editado e colocar no trecho " const wsUrl = `ws://localhost:5025/ws`;"
    o endereco onde a aplicacao esta rodando, 
    OBS: nao pode retirar o ws.
