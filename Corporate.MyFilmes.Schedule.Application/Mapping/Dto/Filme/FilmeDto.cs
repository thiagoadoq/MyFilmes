﻿using System;

namespace Corporate.MyFilmes.Schedule.Application.Mapping.Dto.Filme
{
    public class FilmeDto
    {
        public Guid Id { get; set; }
        public decimal Preco { get; set; }
        public string Titulo { get; set; }
        public string Nome { get; set; }
        public string UrlImagem { get; set; }
        public bool EmCartaz { get; set; }
        public DateTime DtEstreia { get; set; }
    }
}
