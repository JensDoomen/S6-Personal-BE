using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQApplication
{
    public class Publisher
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var getconnection = factory.CreateConnection())
            using (var passage = getconnection.CreateModel())
            {
                passage.QueueDeclare("Test", false, false, false, null);
                string message = "creating a message using asp.net core rabbitmq";
                var body = Encoding.UTF8.GetBytes(message);
                passage.BasicPublish("", "Test", null, body);
                Console.WriteLine("sent message:{0}", message);
            }

            Console.ReadLine();
        }
    }
}