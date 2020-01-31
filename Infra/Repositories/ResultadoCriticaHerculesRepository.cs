using Domain.Entity;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class ResultadoCriticaHerculesRepository : MongoRepository<ResultadoCriticaHercules>, IResultadoCriticaHerculesRepository
    {
        public ResultadoCriticaHerculesRepository(IMongoDatabase mongoDatabase)
            : base(mongoDatabase)
        {
        }
        public async Task<IList<ResultadoCriticaHercules>> ObterTodasAsCriticasHercules()
        {
            return await Collection.Find(_ => true).ToListAsync();
        }
    }
}
