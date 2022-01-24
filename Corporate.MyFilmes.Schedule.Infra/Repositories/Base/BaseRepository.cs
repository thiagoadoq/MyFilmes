using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.MyFilmes.Schedule.Infra.Repositories.Base
{
    public class BaseRepository<T> : IDisposable where T : class, new()
    {
        private IMongoDBContext _dbContext;

        public BaseRepository(IMongoDBContext dbContext) => _dbContext = dbContext;

        public virtual async void Add(T entity) => 
            await _dbContext.GetCollection<T>().InsertOneAsync(entity);

        public async void Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            await _dbContext.GetCollection<T>().DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var documents = await _dbContext.GetCollection<T>().FindAsync(Builders<T>.Filter.Empty);

            return await documents.ToListAsync();
        }
        public void Dispose() => GC.SuppressFinalize(this);
    }

    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}

