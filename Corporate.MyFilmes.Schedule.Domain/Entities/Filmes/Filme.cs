using Corporate.MyFilmes.Schedule.Domain.Base;
using System;

namespace Corporate.MyFilmes.Schedule.Domain.Entities.Filmes
{
    public class Filme : Entity
    {     
        public decimal Preco { get; set; }
        public string Titulo { get; set; }
        public string Nome { get; set; }
        public string UrlImagem { get; set; }
        public bool EmCartaz { get; set; }
        public DateTime DtEstreia { get; set; }
    }
}
