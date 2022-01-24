using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.MyFilmes.Schedule.Domain.Entities.Filmes.Repository
{
    public interface IFilmesRepository
    {
        Task<Filme> GetById(Guid filmeId);
        Task<List<Filme>> GetAllAsync();
        Task<bool> AddAsync(Filme filme);
        Task UpdateAsync(Filme filme);
        Task<bool> DeleteAsync(Guid filmeId);
    }
}
