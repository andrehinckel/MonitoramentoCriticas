using Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public interface IResultadoCriticaPromaxRepository
    {
        Task<IList<ResultadoCriticaPromax>> ObterTodasAsCriticasPromax();
    }
}
