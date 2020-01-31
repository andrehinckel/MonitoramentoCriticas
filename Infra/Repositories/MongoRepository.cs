using Domain.Entity;
using Infra.Mongo;
using MongoDB.Driver;

namespace Infra.Repositories
{
    public abstract class MongoRepository<TDocument>
   where TDocument : IEntidade
    {
        protected IMongoDatabase MongoDatabase { get; }

        public string CollectionName => Colecoes.ObterNomeColecao<TDocument>();

        protected MongoRepository(IMongoDatabase mongoDatabase)
        {
            MongoDatabase = mongoDatabase;
        }

        protected IMongoCollection<TDocument> Collection => MongoDatabase.GetCollection<TDocument>(CollectionName);
    }
}
