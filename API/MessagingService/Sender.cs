using System;
using RabbitMQ.Client;
using System.Text;
using Domain;
using System.Text.Json;
using AutogService.BLL.Facade;

namespace MessagingService.Send
{
    public class Sender
    {
        /// <summary>
        /// send a requestToPrint to the queue
        /// </summary>
        /// <param name="request"></param>
        public static void SendDocumentToQueue(RequestToPrint request)
        {
            SendToQueue("toPrint", request);

        }
        /// <summary>
        /// send a responseOK to the queue
        /// </summary>
        /// <param name="response"></param>
        public static void ResponseFromQueue(ResponseOK response)
        {
            SendToQueue("responseFromServicePrinter", response);
        }
        /// <summary>
        /// send a msg to a queue
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="message"></param>
        private static void SendToQueue(string queue, object message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                IBasicProperties properties = channel.CreateBasicProperties();
                if (message.GetType().Equals(typeof(RequestToPrint)))
                {
                    var priority = ((RequestToPrint)message).Priority;
                    properties.Priority = (byte)priority;
                }
                
                intercept(message);                

                channel.BasicPublish(
                    exchange: "",
                    routingKey: queue,
                    basicProperties: properties,
                    body: body
                    );
                Console.WriteLine("[x] Sent {0}", message);

            }
        }

        /// <summary>
        /// intercept the message, and depends the msg type go to the db or not. 
        /// </summary>
        /// <param name="message"></param>
        public static void intercept(object message)
        {
            if (message.GetType().Equals(typeof(ResponseOK)))
            {
                var msg = ((ResponseOK)message);
                RequestToPrint toSave = new()
                {
                    Document = msg.DocumentName
                };
                FacadeService.Save(toSave);
            }
        }
    }
}
