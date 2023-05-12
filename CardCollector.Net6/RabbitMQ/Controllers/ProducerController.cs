using EasyNetQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.BackgroundServices;

namespace RabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IBus _bus;

        public ProducerController(IBus bus)
        {
            _bus = bus; 
        }
        [HttpPost]
        public async Task<string> Post()
        {
            UserRequest request = new UserRequest(1);

            var response = await _bus.Rpc.RequestAsync<UserRequest, UserResponse>(request);

            return response.Name;
        }
    }
}
