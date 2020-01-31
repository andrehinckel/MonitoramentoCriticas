using Application.DTOs;
using Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ResultadoPromaxHercules : IResultadoPromaxHercules
    {
        int[,] matriz;

        private readonly IResultadoCriticaHerculesRepository _resultadoCriticaHercules;
        private readonly IResultadoCriticaPromaxRepository _resultadoCriticaPromax;

        public ResultadoPromaxHercules(int minhaVariavel, IResultadoCriticaHerculesRepository resultadoCritica, IResultadoCriticaPromaxRepository resultadoCriticaPromax)
        {
            _resultadoCriticaHercules = resultadoCritica;
            _resultadoCriticaPromax = resultadoCriticaPromax;
            matriz = new int[minhaVariavel, 3];
        }

        public async Task<IList<ResultadoCriticaHerculesDto>> GetHercules()
        {
            var resultadoHercules = await _resultadoCriticaHercules.ObterTodasAsCriticasHercules();

            return resultadoHercules.Select(d => new ResultadoCriticaHerculesDto
            {
                DataHoraInicio = d.DataHoraInicio,
                DataHoraFim = d.DataHoraFim,
                ChaveUnica = d.ChaveUnica,
                Criticas = d.Criticas,
                GrupoCritica  = d.GrupoCritica
            }).ToList();
        }

        public async Task<IList<ResultadoCriticaPromaxDto>> GetPromax()
        {
            var resultadoHercules = await _resultadoCriticaPromax.ObterTodasAsCriticasPromax();

            return resultadoHercules.Select(d => new ResultadoCriticaPromaxDto
            {
                DataHoraInicio = d.DataHoraFim,
                DataHoraFim = d.DataHoraFim,
                ChaveUnica = d.ChaveUnica,
                Criticas = d.Criticas
            }).ToList();
        }

        public void CompararPromaxHercules(ResultadoCriticaHerculesDto resultadoCriticaHercules, ResultadoCriticaPromaxDto resultadoCriticaPromax)
        {
            if (!resultadoCriticaHercules.Criticas.Any())
            {
                for (int i = 0; i < resultadoCriticaPromax.Criticas.Count(); i++)
                {
                    resultadoCriticaHercules.CriticaNaoExecutada++;
                }
            }
            else if (resultadoCriticaHercules.DataHoraInicio == resultadoCriticaPromax.DataHoraInicio
                && resultadoCriticaHercules.DataHoraFim == resultadoCriticaPromax.DataHoraFim
                && resultadoCriticaHercules.ChaveUnica == resultadoCriticaPromax.ChaveUnica)
            {
                var NotOK = resultadoCriticaPromax.Criticas.Select(x => x.NumeroCritica).Except(resultadoCriticaHercules.Criticas.Select(h => h.NumeroCritica));
                var NotOKHercules = resultadoCriticaHercules.Criticas.Select(x => x.NumeroCritica).Except(resultadoCriticaPromax.Criticas.Select(p => p.NumeroCritica));
                var OKs = resultadoCriticaPromax.Criticas.Select(x => x.NumeroCritica).Intersect(resultadoCriticaHercules.Criticas.Select(h => h.NumeroCritica));

                var j = 0;

                if (OKs.Any())
                {
                    for (int i = 0; i < OKs.Count(); i++)
                    {
                        matriz[i, 2] = matriz[i, 2] + 1;
                        matriz[i, 0] = OKs.ElementAt(i);
                    }
                }

                if (NotOK.Any())
                {
                    j = OKs.Count();
                    for (int i = 0; i < NotOK.Count(); i++)
                    {
                        matriz[j, 1] = matriz[j, 1] + 1;
                        matriz[j, 0] = NotOK.ElementAt(i);
                        j++;
                    }
                }

                if (NotOKHercules.Any())
                {
                    j = NotOK.Count() + OKs.Count();
                    for (int i = 0; i < NotOKHercules.Count(); i++)
                    {
                        matriz[j, 1] = matriz[j, 1] + 1;
                        matriz[j, 0] = NotOKHercules.ElementAt(i);
                        j++;
                    }
                }
            }
        }

        public int GetOKs(int codigoCritica)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                if (matriz[i, 0] == codigoCritica)
                    return matriz[i, 2];
            }
            throw new ArgumentException("Não houve critica para essa codigo de critica");
        }

        public string GetNotPerformed(ResultadoCriticaHerculesDto resultadoCriticaHercules)
        {
            return $"{resultadoCriticaHercules.CriticaNaoExecutada} criticas não foram executadas no Hércules";
        }

        public string GetNOKs(int codigoCritica, ResultadoCriticaHerculesDto resultadoCriticaHercules)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                if (matriz[i, 0] == codigoCritica)
                    return $"O NOK foi do pedido que corresponde à chave {resultadoCriticaHercules.ChaveUnica} com o número da critica {matriz[i, 0]} no grupo de critica {resultadoCriticaHercules.GrupoCritica}";
            }
            throw new ArgumentException("Não foi possível encontrar o código informado");
        }

        Task<List<ResultadoCriticaBaseDto>> IResultadoPromaxHercules.Main()
        {
            throw new NotImplementedException();
        }
    }
}
