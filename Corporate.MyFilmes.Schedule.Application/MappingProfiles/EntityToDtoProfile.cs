using AutoMapper;
using Corporate.MyFilmes.Schedule.Application.Mapping.Dto.Filme;
using Corporate.MyFilmes.Schedule.Domain.Entities.Filmes;

namespace Corporate.MyFilmes.Schedule.Application.MappingProfiles
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<Filme, FilmeDto>().ReverseMap();           
        }
    }
}
