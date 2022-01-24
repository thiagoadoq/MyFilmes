using MongoDB.Driver;
using System.Threading.Tasks;

namespace Corporate.MyFilmes.Schedule.Infra.Repositories.Base
{
    public interface IMongoDBContext
    {
        MongoClient MongoClient { get; }
        IClientSessionHandle Session { get; set; }
        IMongoCollection<T> GetCollection<T>() where T : class;
        Task Add<T>(T entity) where T : class;
        void Dispose();
    }
}