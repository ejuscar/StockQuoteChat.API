using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace StockQuoteChat.Bot.Receiver
{
    public class Receive
    {
        public static async Task Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            HubConnection hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7173/chat")
                .Build();

            try
            {
                await hubConnection.StartAsync();
                Console.WriteLine("Connection started");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "stock_quote_chat",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var parsedBody = JsonSerializer.Deserialize<QueueMessage>(Encoding.UTF8.GetString(body));

                    await hubConnection.InvokeAsync("AddUserToRoom", new UserConnection("MyChat Bot", parsedBody!.Room));
                    await hubConnection.InvokeAsync("SendMessage", parsedBody.Message);
                };
                channel.BasicConsume(queue: "stock_quote_chat",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}

