using AutoMapper;
using Corporate.MyFilmes.Schedule.Application.Contracts.Applications;
using Corporate.MyFilmes.Schedule.Application.Mapping.Dto.Filme;
using Corporate.MyFilmes.Schedule.Domain.Entities.Filmes;
using Corporate.MyFilmes.Schedule.Domain.Entities.Filmes.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.MyFilmes.Schedule.Application.Contracts.Implements
{
    public class FilmesApplication : IFilmesApplication
    {

        private readonly IMapper _mapper;
        private readonly IFilmesRepository _filmesRepository;

        public FilmesApplication(IMapper mapper, IFilmesRepository filmesRepository)
        {
            _filmesRepository = filmesRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(FilmeDto filmeDto)
        {
            try
            {
                var filme = _mapper.Map<Filme>(filmeDto);

                return await _filmesRepository.AddAsync(filme);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<FilmeDto> UpdateAsync(FilmeDto filme)
        {
            throw new NotImplementedException();
        }

        public FilmeDto GetById(Guid id)
        {
            var filme =  _filmesRepository.GetById(id);

            return _mapper.Map<FilmeDto>(filme);
        }

        public  List<FilmeDto> GetAllAsync()
        {
            try
            {
                var result =  _filmesRepository.GetAllAsync();
                var filmeDto = _mapper.Map<List<FilmeDto>>(result);
                return filmeDto;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid filmeId)
        {
            return await  _filmesRepository.DeleteAsync(filmeId);
        }
    }
}
