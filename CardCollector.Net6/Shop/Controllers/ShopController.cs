using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Shop.Models;
using System.Text;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;


        public ShopController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Card card = new Card();
            card.Id = Guid.NewGuid().ToString();
            card.Name = " testt";

            ConnectionFactory factory = new();
            factory.Uri = new Uri("amqp://rppbqhta:VGBH8m_LzZAYttQizzAVD2xHnswQIgbs@rattlesnake.rmq.cloudamqp.com/rppbqhta");
            factory.ClientProvidedName = "ShopService";



            IConnection cnn = factory.CreateConnection();



            IModel channel = cnn.CreateModel();



            string exchangeName = "CardCollector";
            string routingKey = "card-routing-key";
            string queueName = "CardQueue";



            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);



            var json = JsonConvert.SerializeObject(card);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchangeName, routingKey, null, body);
            _logger.LogInformation($"Message published to {queueName}");

            //Easy logger
            //EasyLogger.LogInformation(logger: this._logger, message: "Message pushed!");



            channel.Close();
            cnn.Close();



            return Ok(card);
        }


    }
}
