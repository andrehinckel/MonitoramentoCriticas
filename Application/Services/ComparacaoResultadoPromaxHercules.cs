using Application.DTOs;
using System.Linq;

namespace Application.Services
{
    public class ComparacaoResultadoPromaxHercules : IComparacaoResultadoPromaxHercules
    {
        public ComparacaoResultadoPromaxHercules()
        {
        }

        int[,] matriz = new int[10, 3];

        public void CriticsSum(ResultadoCriticaHerculesDto resultadoCriticaHercules, ResultadoCriticaPromaxDto resultadoCriticaPromax)
        {
            if (resultadoCriticaHercules.DataHoraInicio == resultadoCriticaPromax.DataHoraInicio
                && resultadoCriticaHercules.DataHoraFim == resultadoCriticaPromax.DataHoraFim
                && resultadoCriticaHercules.ChaveUnica == resultadoCriticaPromax.ChaveUnica)
            {
                for (int i = 0; i < resultadoCriticaHercules.Criticas.Count(); i++)
                {
                    if (resultadoCriticaHercules.Criticas[i].NumeroCritica == resultadoCriticaPromax.Criticas[i].NumeroCritica
                        && resultadoCriticaHercules.Criticas[i].Status == resultadoCriticaPromax.Criticas[i].Status
                        && resultadoCriticaHercules.Criticas[i].Alcada == resultadoCriticaPromax.Criticas[i].Alcada)
                        matriz[i, 2] = matriz[i, 2] + 1;
                    else
                        matriz[i, 1] = matriz[i, 1] + 1;
                    matriz[i, 0] = resultadoCriticaHercules.Criticas[i].NumeroCritica;
                }
            }
        }

        public int GetOKs(int codigoCritica)
        {
            var index = 0;
            for (int i = 0; i < 10; i++)
            {
                if (matriz[i, 0] == codigoCritica)
                    index = i;
            }
            return matriz[index, 2];
        }

        public int GetNOKs(int codigoCritica)
        {
            var index = 0;
            for (int i = 0; i < 10; i++)
            {
                if (matriz[i, 0] == codigoCritica)
                    index = i;
            }
            return matriz[index, 1];
        }
    }
}
