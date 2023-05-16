using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Newtonsoft.Json;
using CardCollector.Net6.Database.Models;
using System.Text;

namespace Collection.Worker
{
    public class CardConsumer : BackgroundService
    {
        private readonly ILogger<CardConsumer> _logger;
        //private readonly IOrderService _service;

        public readonly IServiceScopeFactory _serviceScopeFactory;

        private string exchangeName;
        private string routingKey;
        private string queueName;
        private IModel channel;

        public CardConsumer(ILogger<CardConsumer> logger, IServiceScopeFactory serviceScopeFactory)
        {
            ConnectionFactory factory = new();
            factory.Uri = new Uri("amqp://rppbqhta:VGBH8m_LzZAYttQizzAVD2xHnswQIgbs@rattlesnake.rmq.cloudamqp.com/rppbqhta");
            factory.ClientProvidedName = "CollectionService";

            IConnection cnn = factory.CreateConnection();
            channel = cnn.CreateModel();
            exchangeName = "CardCollector";
            routingKey = "card-routing-key";
            queueName = "CardQueue";

            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            // When the timer should have no due-time, then do the work once now.

            DoWork();

            using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    DoWork();
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Timed Hosted Service is stopping.");
            }
        }

        private void DoWork()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                //IOrderService orderService = scope.ServiceProvider.GetService<IOrderService>();

                //ConnectionFactory factory = new();
                //factory.Uri = new Uri("amqps://xgjivpda:oV4Cr6xUh1ORfcklh1mcoKoRWgA8WBaJ@rattlesnake.rmq.cloudamqp.com/xgjivpda");
                //factory.ClientProvidedName = "PaymentService";

                //IConnection cnn = factory.CreateConnection();

                //IModel channel = cnn.CreateModel();

                //string exchangeName = "Cinepolis";
                //string routingKey = "order-routing-key";
                //string queueName = "OrderQueue";

                channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
                channel.QueueDeclare(queueName, true, false, false, null);
                channel.QueueBind(queueName, exchangeName, routingKey, null);

                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (sender, args) =>
                {
                    Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                    var body = args.Body.ToArray();

                    string message = Encoding.UTF8.GetString(body);

                    _logger.LogInformation($"Message Received: {message}");
                    Card order = JsonConvert.DeserializeObject<Card>(message);

                    //orderService.CreateOrder(order);
                    //Sturen naar DB
                    channel.BasicAck(args.DeliveryTag, false);
                };

                string consumerTag = channel.BasicConsume(queueName, false, consumer);
            }
        }

    }
}
