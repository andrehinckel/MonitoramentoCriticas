using Domain.Entity;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class ResultadoCriticaPromaxRepository : MongoRepository<ResultadoCriticaPromax>, IResultadoCriticaPromaxRepository
    {
        public ResultadoCriticaPromaxRepository(IMongoDatabase mongoDatabase)
          : base(mongoDatabase)
        {
        }

        public async Task<IList<ResultadoCriticaPromax>> ObterTodasAsCriticasPromax()
        {
            return await Collection.Find(x => true).ToListAsync();
        }
    }
}
