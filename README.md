# StockQuoteChat.API
This is a chat project using SignalR and .NET 6. It allows sending and receiving messages in real time, as well as receiving bot messages, indicating user input and output. The bot also informs the current stock quote when the user types the command /stock=stock_code.
To access the UI, download the project https://github.com/ejuscar/stock-quote-chat.ui.

## Project parts
The StockQuoteChat.API project has the chat hub and the API consumer controllers. StockQuoteChat.Application has the templates to be used in the API. The StockQuoteChat.Infrastructure has the data persistence class in the database and the migrations. Finally, StockQuoteChat.Test has the unit tests of the project.

## Bot usage
To use the bot, you need to run a separate project, found at this link: https://github.com/ejuscar/StockQuoteChat.Bot

## Technologies required
-To run this project, you will ned to have a postgresql db and RabbitMQ for the bot.
- To execute the migrations and seeds you have to open Nuget Manager Console and write Update-Database. This command will create the tables in the database and insert some required registers.