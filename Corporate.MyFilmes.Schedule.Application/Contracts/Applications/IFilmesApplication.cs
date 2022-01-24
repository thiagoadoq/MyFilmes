using Corporate.MyFilmes.Schedule.Application.Mapping.Dto.Filme;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.MyFilmes.Schedule.Application.Contracts.Applications
{
    public interface IFilmesApplication
    {
        FilmeDto GetById(Guid id);
        List<FilmeDto> GetAllAsync();
        Task<bool> AddAsync(FilmeDto filme);
        Task<FilmeDto> UpdateAsync(FilmeDto filme);
        Task<bool> DeleteAsync(Guid filmeId);
    }
}
