using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using StockQuoteChat.Bot.Sender.Models;
using System.Text;
using System.Text.Json;

namespace StockQuoteChat.Bot.Sender.Services
{
    public class ChatBotService
    {
        private readonly string _apiUrl = "https://stooq.com/q/l/?s={stockCode}&f=sd2t2ohlcv&h&e=csv";

        public ChatBotService()
        {
        }

        public async Task<QueueMessage> ProcessMessage(SendStockQuoteRequestDto request)
        {
            // Get stock_code csv
            var httpClient = new HttpClient();
            var apiResponse = await httpClient.GetStringAsync(_apiUrl.Replace("{stockCode}", request.StockCode));

            // Get quote value
            var quoteValue = ReadCsvContent(apiResponse);
            var queueMessage = new QueueMessage(request, quoteValue);

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "stock_quote_chat",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(queueMessage));

                channel.BasicPublish(exchange: "",
                                     routingKey: "stock_quote_chat",
                                     basicProperties: null,
                                     body: body);
            }

            return queueMessage;
        }

        private string ReadCsvContent(string fileContent)
        {
            var lines = fileContent.Replace("\r", "").Split("\n");

            // Line 0 is the header, and line 1 contains the values
            var values = lines[1].Split(",");

            // The values are in the following template: Symbol	Date	Time	Open	High	Low	Close	Volume
            // The value we want is in the 4rd index (Open)
            return values[3];
        }
    }
}
