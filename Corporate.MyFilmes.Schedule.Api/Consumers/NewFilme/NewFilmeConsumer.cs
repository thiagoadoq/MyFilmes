using Corporate.MyFilmes.Schedule.Application.Contracts.Applications;
using Corporate.MyFilmes.Schedule.Application.Mapping.Dto.Filme;
using Corporate.MyFilmes.Schedule.Infra.RabbitMq;

namespace Corporate.MyFilmes.Schedule.Api.Consumers.NewFilme
{
    public class NewFilmeConsumer : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public NewFilmeConsumer(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _bus.SubscribeAsync<NewFilmeMessage>(nameof(NewFilmeConsumer), CadastrarFilme, nameof(NewFilmeMessage));
        }

        private async Task CadastrarFilme(NewFilmeMessage message, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var filmesApplication = scope.ServiceProvider.GetRequiredService<IFilmesApplication>();

            var result = await filmesApplication.AddAsync(new FilmeDto
            {
                Titulo = message.Titulo,
                Nome = message.Nome,
                Preco = message.Preco,
                UrlImagem = message.UrlImagem,
                EmCartaz = message.EmCartaz,
                DtEstreia = message.DtEstreia,
            });
        }
    }
}
