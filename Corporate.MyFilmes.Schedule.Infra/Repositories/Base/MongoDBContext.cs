using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Corporate.MyFilmes.Schedule.Infra.Repositories.Base
{
    public class MongoDBContext : IMongoDBContext, IDisposable
    {
        private IMongoDatabase Database { get; }

        public MongoDBContext(IConfiguration configuration)
        {
            try
            {
                var cnnStr = configuration["MyFilmesDatabaseSettings:ConnectionString"];
                var databaseName = configuration["MyFilmesDatabaseSettings:DatabaseName"];

                var settings = MongoClientSettings.FromConnectionString(cnnStr);

                settings.SslSettings = new SslSettings()
                {
                    EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                };

                MongoClient = new MongoClient(settings);

                Database = MongoClient.GetDatabase(databaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel acessar o servidor", ex);
            }
        }

        public MongoClient MongoClient { get; private set; }
        public IClientSessionHandle Session { get; set; }

        public IMongoCollection<T> GetCollection<T>() where T : class
        {
            return Database.GetCollection<T>(typeof(T).Name);
        }

        public async Task Add<T>(T entity) where T : class
        {
            await GetCollection<T>().InsertOneAsync(entity);
        }

        public void Dispose()
        {
            while (Session != null && Session.IsInTransaction)
                Thread.Sleep(TimeSpan.FromMilliseconds(100));

            GC.SuppressFinalize(this);
        }
    }
}
