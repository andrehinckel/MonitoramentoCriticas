using Application.DTOs;

namespace Application.Services
{
    public interface IResultadoPromaxHercules
    {
        void CompararPromaxHercules(ResultadoCriticaHerculesDto resultadoCriticaHercules, ResultadoCriticaPromaxDto resultadoCriticaPromax);
        int GetOKs(int codigoCritica);
        string GetNOKs(int codigoCritica, ResultadoCriticaHerculesDto resultadoCriticaHercules);
        public string GetNotPerformed(ResultadoCriticaHerculesDto resultadoCriticaHercules);
    }
}
