using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Polly;
using RabbitMQ.Client.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.MyFilmes.Schedule.Infra.RabbitMq
{
    public class MessageBus : IMessageBus, IDisposable
    {
        private IBus _bus;
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public MessageBus(IConfiguration configuration)
        {
            _configuration = configuration;

            _connectionString = configuration["RabbitMqSettings:ConnectionString"];

            TryConnect();
        }

        public bool IsConnected => _bus?.Advanced.IsConnected ?? false;
        public IAdvancedBus AdvancedBus => _bus?.Advanced;

        public async Task PublishAsync<T>(T message)
        {
            TryConnect();
            await _bus.PubSub.PublishAsync(message);
        }

        public async Task PublishInTimeAsync<T>(T input, TimeSpan delay)
        {
            TryConnect();
            await _bus.Scheduler.FuturePublishAsync(input, delay);
        }

        public async Task SubscribeAsync<T>(string subscriptionId, Func<T, CancellationToken, Task> onMessage, string queueName)
        {
            TryConnect();

            await _bus.PubSub.SubscribeAsync(subscriptionId, onMessage, config => config.WithQueueName(queueName));
        }

        private void TryConnect()
        {
            if (IsConnected) return;

            var connectRetryCountStr = _configuration["RabbitMqSettings:ConnectRetry:Count"];
            var connectRetryDurationStr = _configuration["RabbitMqSettings:ConnectRetry:DurationInSeconds"];
            var connectRetryDuration = double.Parse(connectRetryDurationStr);
            var connectRetryCount = int.Parse(connectRetryCountStr);

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(connectRetryCount, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(connectRetryDuration, retryAttempt)));

            policy.Execute(() =>
            {
                _bus = RabbitHutch.CreateBus(_connectionString);
                AdvancedBus.Disconnected += OnDisconnect;
            });
        }

        private void OnDisconnect(object s, EventArgs e)
        {
            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .RetryForever();

            policy.Execute(TryConnect);
        }

        public void Dispose()
        {
            _bus.Dispose();
        }
    }
}
