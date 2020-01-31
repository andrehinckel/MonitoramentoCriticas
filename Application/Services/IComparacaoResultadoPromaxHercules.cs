using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IResultadoPromaxHercules
    {
        void CompararPromaxHercules(ResultadoCriticaHerculesDto CriticaHercules, ResultadoCriticaPromaxDto CriticaPromax);
        int GetOKs(int codigoCritica);
        string GetNOKs(int codigoCritica);
        string GetNotPerformed(ResultadoCriticaHerculesDto resultadoCriticaHercules);
        Task<IList<ResultadoCriticaHerculesDto>> GetHercules();
        Task<IList<ResultadoCriticaPromaxDto>> GetPromax();
        Task<List<ResultadoCriticaPromaxHerculesDto>> Main();
    }
}
