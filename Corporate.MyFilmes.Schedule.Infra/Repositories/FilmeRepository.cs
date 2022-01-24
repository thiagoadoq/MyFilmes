using Corporate.MyFilmes.Schedule.Domain.Entities.Filmes;
using Corporate.MyFilmes.Schedule.Domain.Entities.Filmes.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Corporate.MyFilmes.Schedule.Infra.Repositories.Base;
using Corporate.MyFilmes.Schedule.Infra.Services.Contracts;

namespace Corporate.MyFilmes.Schedule.Infra.Repositories
{
    public class FilmeRepository : BaseRepository<Filme>, IFilmesRepository
    {
        private readonly IMongoDBContext _dbContext;
        private readonly ICacheService _cacheService;

        public FilmeRepository(IMongoDBContext dbContext, ICacheService cacheService) : base(dbContext)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
        }

        public async Task<bool> AddAsync(Filme filme)
        {
            filme.Id = Guid.NewGuid();

            await _dbContext.Add(filme);
            
            var cacheKey = $"filme_{filme.Id}";

            await _cacheService.SetAsync(cacheKey, filme, DateTimeOffset.Now.AddHours(1));

            return true;
        }

        public async Task<bool> DeleteAsync(Guid filmeId)
        {
            var result = await _dbContext.GetCollection<Filme>().DeleteOneAsync(x => x.Id.Equals(filmeId));

            if(result.DeletedCount > 0)
            return true;
            else
                return false;
        }

        public async Task<List<Filme>> GetAllAsync()
        {
            const string cacheKey = "filmes";

            var cachedFilmes = await _cacheService.GetAsync<List<Filme>>(cacheKey);

            if (cachedFilmes is not null) return cachedFilmes;

            var filmes = (await _dbContext.GetCollection<Filme>().FindAsync(x => x.Titulo != null)).ToList();

            if (filmes is not { Count: > 0 }) return filmes;

            return await _cacheService.SetAsync(cacheKey, filmes, DateTimeOffset.Now.AddMinutes(1));
        }

        public async Task<Filme> GetById(Guid filmeId)
        {
            var cacheKey = $"filme_{filmeId}";

            var cachedFilme = await _cacheService.GetAsync<Filme>(cacheKey);

            if (cachedFilme is not null) return cachedFilme;

            var filme = await _dbContext
                .GetCollection<Filme>()
                .Find(c => c.Id.Equals(filmeId))
                .FirstOrDefaultAsync();

            if (filme is null) return null;

            return await _cacheService.SetAsync(cacheKey, filme, DateTimeOffset.Now.AddHours(1));
        }

        public async Task UpdateAsync(Filme novoFilme)
        {
            var filme = await _dbContext.GetCollection<Filme>().Find(c => c.Id.Equals(novoFilme.Id)).FirstOrDefaultAsync();

            filme.Nome = novoFilme.Nome;
            filme.Titulo = novoFilme.Titulo;
            filme.Preco = novoFilme.Preco;
            filme.EmCartaz = novoFilme.EmCartaz;
            filme.UrlImagem = novoFilme.UrlImagem;
            filme.DtEstreia = novoFilme.DtEstreia;

            var updateResult = await _dbContext.GetCollection<Filme>().ReplaceOneAsync(c => c.Id.Equals(filme.Id), filme);

            if (updateResult.ModifiedCount > 0)
            {

            };
        }
    }
}
