using MongoDB.Driver;
using System;

namespace Corporate.MyFilmes.Schedule.Infra.Repositories.Base
{
    public class MongoDBContext<T> where T : class
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }

        public MongoClient Session { get; private set; }

        private IMongoDatabase _database { get; }

        public MongoDBContext()
        {
            try
            {

                ConnectionString = "mongodb://localhost:27017/";
                DatabaseName = "myFimesdb";

                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
        
                settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };

                var client = new MongoClient(settings);
                _database = client.GetDatabase(DatabaseName);
            
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel acessar o servidor", ex);
            }
        }

        public IMongoCollection<T> GetColection
        {
            get
            {
                return _database.GetCollection<T>(typeof(T).Name);
            }
        }

    }
}
    