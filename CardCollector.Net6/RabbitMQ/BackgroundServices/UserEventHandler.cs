using EasyNetQ;

namespace RabbitMQ.BackgroundServices
{
    public class UserRequest
    {
        public long ID { get; set; }
        public UserRequest(long id)
        {
            ID = id;
        }
    }

    public class UserResponse
    {
        public string Name { get; set; }
        public UserResponse() { }
    }
    public class UserEventHandler : BackgroundService
    {
        private readonly IBus _bus;

        public UserEventHandler(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _bus.Rpc.RespondAsync<UserRequest, UserResponse>(ProcesUserRequest);

        }

        private UserResponse ProcesUserRequest(UserRequest userRequest)
        {
            return new UserResponse() { Name = "Ipsum" };
        }
    }
}
