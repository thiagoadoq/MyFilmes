using EasyNetQ;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.MyFilmes.Schedule.Infra.RabbitMq
{
    public interface IMessageBus
    {
        IAdvancedBus AdvancedBus { get; }
        bool IsConnected { get; }

        void Dispose();
        Task PublishAsync<T>(T message);
        Task PublishInTimeAsync<T>(T input, TimeSpan delay);
        Task SubscribeAsync<T>(string subscriptionId, Func<T, CancellationToken, Task> onMessage, string queueName);
    }
}