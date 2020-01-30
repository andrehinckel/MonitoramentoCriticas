using Application.DTOs;
using Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class ComparacaoResultadoPromaxHercules : IComparacaoResultadoPromaxHercules
    {
        public ComparacaoResultadoPromaxHercules()
        {
        }

        public List<int> ok = new List<int>();
        public List<int> nok = new List<int>();

        public void CriticsSum(ResultadoCriticaHerculesDto resultadoCriticaHercules, ResultadoCriticaPromaxDto resultadoCriticaPromax)
        {
            if (resultadoCriticaHercules.DataHoraInicio == resultadoCriticaPromax.DataHoraInicio
                && resultadoCriticaHercules.DataHoraFim == resultadoCriticaPromax.DataHoraFim
                && resultadoCriticaHercules.ChaveUnica == resultadoCriticaPromax.ChaveUnica)
            {
                resultadoCriticaHercules.Criticas.OrderBy(d => d.NumeroCritica);
                resultadoCriticaPromax.Criticas.OrderBy(d => d.NumeroCritica);

                var j = 0;
                var k = 0;

                for (int i = 0; i < resultadoCriticaHercules.Criticas.Count(); i++)
                {
                    if (resultadoCriticaHercules.Criticas[i].NumeroCritica == resultadoCriticaPromax.Criticas[i].NumeroCritica
                        && resultadoCriticaHercules.Criticas[i].Status == resultadoCriticaPromax.Criticas[i].Status
                        && resultadoCriticaHercules.Criticas[i].Alcada == resultadoCriticaPromax.Criticas[i].Alcada)

                        j++;
                    else
                        k++;

                }
                ok.Add(j);
                nok.Add(k);
            }
        }

        public int GetOKs(int codigoCritica)
        {
            return 1;
        }

        public int GetNOKs(int Codigo)
        {
            return 1;
        }
    }
}
