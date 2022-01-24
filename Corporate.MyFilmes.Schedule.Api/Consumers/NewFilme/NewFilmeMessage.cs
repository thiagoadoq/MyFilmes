using EasyNetQ;

namespace Corporate.MyFilmes.Schedule.Api.Consumers.NewFilme
{
    [Queue(nameof(NewFilmeMessage))]
    public class NewFilmeMessage
    {
        public decimal Preco { get; set; }
        public string? Titulo { get; set; }
        public string? Nome { get; set; }
        public string? UrlImagem { get; set; }
        public bool EmCartaz { get; set; }
        public DateTime DtEstreia { get; set; }
    }
}
