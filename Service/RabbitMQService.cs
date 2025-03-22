using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using OrderManagementAPI.Model;

public class RabbitMQService
{
    private readonly string _connectionString;

    public RabbitMQService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task ConsumeOrderCreatedEventAsync(Order order)
    {
        // Configura o ConnectionFactory
        var factory = new ConnectionFactory
        {
            Uri = new Uri(_connectionString),
            //DispatchConsumersAsync = true // Habilita o consumo assíncrono
        };

        // Cria a conexão de forma assíncrona
        using (var connection = await factory.CreateConnectionAsync())
        // Cria o canal de forma assíncrona
        using (var channel = await connection.CreateChannelAsync())
        {
            // Declara a fila (queue)
            await channel.QueueDeclareAsync(queue: order.CustomerName,
                                           durable: false,
                                           exclusive: false,
                                           autoDelete: false,
                                           arguments: null);

            // Configura o consumidor assíncrono
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                // Converte o corpo da mensagem (body) para string
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Desserializa a mensagem para um objeto Order
                var order = JsonConvert.DeserializeObject<Order>(message);

                // Processa o pedido (ex: log, enviar e-mail, etc.)
                Console.WriteLine($"Pedido recebido: {order.CustomerName}");
                Console.WriteLine($"Itens: {JsonConvert.SerializeObject(order.Items)}");
                Console.WriteLine($"Total: {order.TotalPrice}");

                // Confirma o recebimento da mensagem (acknowledgement)
                await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            };

            // Inicia o consumo da fila de forma assíncrona
            await channel.BasicConsumeAsync(queue: "order_created",
                                            autoAck: false, // Confirmação manual (ack)
                                            consumer: consumer);

            Console.WriteLine("Aguardando mensagens... Pressione [Enter] para sair.");
            Console.ReadLine(); // Mantém o consumidor ativo
        }


    }
}