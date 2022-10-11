using System.Text;
using Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using ReceivingApp.Facade;
using MessagingService.Send;

namespace MessagingService.Receive
{
    public class Receiver
    {
        private static IModel channel;
        private static IConnection connection;

        public Receiver()
        {
            InitRabbit();
        }
        /// <summary>
        /// Init rabbit simulating a printer queue
        /// </summary>
        private static void InitRabbit()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            
                channel.QueueDeclare(
                    queue: "toPrint", // misma cola que en el Send
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                Console.WriteLine("[*] Waiting for messages");                  
        }
        /// <summary>
        /// start to process the message in the ReceivingApp
        /// </summary>
        private static void process()
        {
            var consume = new EventingBasicConsumer(channel);
            consume.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("[x] Received {0}", message);

                RequestToPrint request = JsonSerializer.Deserialize<RequestToPrint>(message);

                ResponseOK? responseOk = FacadeReceiving.Print(request);

                if (responseOk != null) Sender.ResponseFromQueue(responseOk);
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                Console.WriteLine("[x] Done");
            };
            channel.BasicConsume(
                queue: "toPrint",
                autoAck: false,
                consumer: consume
                );
        }

        /// <summary>
        /// simulate init the printer
        /// </summary>
        /// <returns></returns>
        public static Task Start()
        {
     //     InitRabbit();
            process();
            return Task.CompletedTask;
        }

        /// <summary>
        /// simulate stop the printer
        /// </summary>
        /// <returns></returns>
        public static Task Stop()
        {
            channel.Close();
            return Task.CompletedTask;

        }
    }
}
