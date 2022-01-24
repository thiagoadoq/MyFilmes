using Corporate.MyFilmes.Schedule.Application.Contracts.Applications;
using Corporate.MyFilmes.Schedule.Application.Mapping.Dto.Filme;
using Corporate.MyFilmes.Schedule.Domain.Entities.Filmes;
using Moq;
using System;
using Xunit;

namespace Corporate.MyFilmes.Schedule.Test
{
    public class FilmesTests
    {
        private Mock<FilmeDto> _mock;

        [Fact]
        public void Add_Filmes_Validate_object()
        {
            Moq.Mock<IFilmesApplication> mock = new Moq.Mock<IFilmesApplication>();

            _mock = new Mock<FilmeDto>();
            FilmeDto _filmes = new FilmeDto()
            {
                Titulo = "Filmes geral",
                Preco =10
            };

            var filmeApplication = (IFilmesApplication)mock.Object;

            Assert.NotNull(filmeApplication.AddAsync(_filmes));
        }

        [Fact]
        public async void Get_Filmes_By_Id_Validate_Object()
        {
            Moq.Mock<IFilmesApplication> mock = new Moq.Mock<IFilmesApplication>();

            _mock =  new Mock<FilmeDto>();
            FilmeDto _filmes = new FilmeDto()
            {
                Id = new Guid("14125e08-e4af-42cf-9470-1d1f63752611"),
                Titulo = "Filmes geral",
                Preco = 10
            };

            mock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(_filmes);

            var filmesApplication = (IFilmesApplication)mock.Object;

            var filme = filmesApplication.GetById(new Guid("14125e08-e4af-42cf-9470-1d1f63752611"));

            Assert.Equal(0, filme.Preco);
            Assert.Null(filme.Titulo);
        }

        [Fact]
        public async void GetAll_Filmes_By_Id_Validate_Object()
        {
            Moq.Mock<IFilmesApplication> mock = new Moq.Mock<IFilmesApplication>();

            _mock = new Mock<FilmeDto>();
            FilmeDto _filmes = new FilmeDto()
            {
                Titulo = "Filmes geral",
                Preco = 10
            };

            mock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(_filmes);

            var filmesApplication = (IFilmesApplication)mock.Object;

            var filme = filmesApplication.GetAllAsync();

            Assert.Null(filme);
        }
    }
}