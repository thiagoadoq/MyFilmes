using Corporate.MyFilmes.Schedule.Domain.Base;
using System;

namespace Corporate.MyFilmes.Schedule.Domain.Contracts.Repositories
{
    public interface IGenericRepository<T> : IDisposable where T : Entity
    {
       
    }
}
