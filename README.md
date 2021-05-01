# O que é o qs.messages?
É um Sistema para gerenciamento de envio de mensagens, seja via e-mail ou SMS. 

>**Note:** Nessa versão somente o envio por `E-mail com SMTP`. Ainda estou analisando brokers para SMS, mas a ditatica e logica seguem o mesmo padrão.

O projeto tem como objetivo mostrar como crio um projeto para acesso ao MongoDB com RabbitMQ e tambem funcionar como API de mensageria de qualquer projeto.


## Iniciando com o projeto

O projeto está na versão .NET 5.0.

1. Instale ou rode o docker para o banco de dados do MongoDB.
2. Instale ou rode o docker para o RabbitMQ.

Configure suas conexões no appSettings.json: 

### Mongo

```json
"MongoConnection": {
    "ConnectionString": "mongodb://localhost:27017",
    "Database": "messageDB"
  },
``` 

### RabbitMQ

```json
"MessageSettings": {
    "RabbitConnection": "amqp://guest:guest@localhost",
    "Queue": "message"
  }
``` 

### Email de saida

```json
"MailSettings": {
    "Port": "587",
    "Smtp": "smtp.seudominio.com",
    "User": "seuemail@seudominio.com",
    "Password": "suaSenha"
  },

``` 

# Conectando seus projetos para envio com qs.message. 

Inclua o pacote
```
dotnet add package qsMessage
```

>**Note:** Os e-mails somente serão enviados com os projetos e templates previamente cadastrados no qs.messages.


Inclua a conexão do RabbitMQ e o API Key do projeto criado no qs.message no seu appSettings.json.

```json
"MessageSettings": {
    "RabbitConnection": "amqp://guest:guest@localhost",
    "Queue": "message",
    "ProjectApiKey": "07972cd8-f879-4cfe-b900-d8d753522e05"
  }
``` 
>**Note:** ApiKey é cadastrado automaticamente quando se cadastra um projeto no qs.messages. 
>Lembre - se que a fila e a mesma informada no qs.messages

Adicione o projeto no Startup.cs

1. ConfigureServices

```csharp
 services.AddQsMessage(Configuration);
```

2. Inclua o serviço onde queria usar como injeção de dependencia. 

```csharp
 ISendMailService
```
|Campo|Valor|
|--|--|
|to  | E-mail para quem deseja enviar. |
|templateID| Identificador do template que criou no qs.message  |
|values| Lista de parametros KeyValue para no caso o template precisar substituir valores ex: [Nome] por Danilo  |

>**Note:** Os campos do "value" podem ser qualquer chave cadastrada no "MailTemplate" do seu template informado.
