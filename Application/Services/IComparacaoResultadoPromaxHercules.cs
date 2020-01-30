using Application.DTOs;

namespace Application.Services
{
    public interface IComparacaoResultadoPromaxHercules
    {
        void CriticsSum(ResultadoCriticaHerculesDto resultadoCriticaHercules, ResultadoCriticaPromaxDto resultadoCriticaPromax);
        int GetOKs(int codigoCritica);
        int GetNOKs(int Codigo);
    }
}
