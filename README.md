# Projeto .NET API com MongoDB e RabbitMQ

Este é um projeto desenvolvido durante um treinamento, utilizando **.NET API**, **MongoDB** como banco de dados NoSQL e **RabbitMQ** para comunicação assíncrona.

## Tecnologias Utilizadas
- .NET 6/7
- MongoDB
- RabbitMQ

## Configuração do Ambiente
1. Instale o **.NET SDK** compatível com o projeto.
2. Configure o **MongoDB** e o **RabbitMQ** (localmente ou via Docker).
3. Clone este repositório:
   ```sh
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio
   ```
4. Configure as variáveis de ambiente ou edite o `appsettings.json`.
5. Execute a aplicação:
   ```sh
   dotnet run
   ```

## Estrutura do Projeto
```
📂 Projeto
├── 📁 Controllers     # Endpoints da API
├── 📁 Services        # Regras de negócio
├── 📁 Models         # Definição dos modelos de dados
├── 📁 Data   # Comunicação com o MongoDB
├── 📁 Event      # Configuração do RabbitMQ
└── Program.cs        # Ponto de entrada da aplicação
```

