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
        List<OKsDto> ListOKs = new List<OKsDto>();
        List<NOKsDto> ListNOKs = new List<NOKsDto>();
        List<NaoExecutadosDto> ListNaoExecutados = new List<NaoExecutadosDto>();

        private readonly IResultadoCriticaHerculesRepository _resultadoCriticaHercules;
        private readonly IResultadoCriticaPromaxRepository _resultadoCriticaPromax;

        public ResultadoPromaxHercules(IResultadoCriticaHerculesRepository resultadoCritica, IResultadoCriticaPromaxRepository resultadoCriticaPromax)
        {
            _resultadoCriticaHercules = resultadoCritica;
            _resultadoCriticaPromax = resultadoCriticaPromax;
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
                GrupoCritica = d.GrupoCritica
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
                    var item = new NaoExecutadosDto()
                    {
                        NaoExecutados = resultadoCriticaHercules.CriticaNaoExecutada++.ToString()
                    };
                    ListNaoExecutados.Add(item);
                }
            }
            else
            if (resultadoCriticaHercules.DataHoraInicio == resultadoCriticaPromax.DataHoraInicio
                    && resultadoCriticaHercules.DataHoraFim == resultadoCriticaPromax.DataHoraFim
                    && resultadoCriticaHercules.ChaveUnica == resultadoCriticaPromax.ChaveUnica)
            {
                var NotOK = resultadoCriticaPromax.Criticas.Select(x => x.NumeroCritica).Except(resultadoCriticaHercules.Criticas.Select(h => h.NumeroCritica));
                var NotOKHercules = resultadoCriticaHercules.Criticas.Select(x => x.NumeroCritica).Except(resultadoCriticaPromax.Criticas.Select(p => p.NumeroCritica));
                var OKs = resultadoCriticaPromax.Criticas.Select(x => x.NumeroCritica).Intersect(resultadoCriticaHercules.Criticas.Select(h => h.NumeroCritica));

                if (OKs.Any())
                {
                    for (int i = 0; i < OKs.Count(); i++)
                    {
                        var item = new OKsDto()
                        {
                            OKs = 1,
                            CodigoCritica = OKs.ElementAt(i)
                        };
                        ListOKs.Add(item);
                    }
                }

                if (NotOK.Any())
                {
                    for (int i = 0; i < NotOK.Count(); i++)
                    {
                        var item = new NOKsDto()
                        {
                            NOKs = 1,
                            CodigoCritica = NotOK.ElementAt(i),
                            ChaveUnica = resultadoCriticaHercules.ChaveUnica,
                            GrupoCritica = resultadoCriticaHercules.GrupoCritica
                        };
                        ListNOKs.Add(item);
                    }
                }

                if (NotOKHercules.Any())
                {
                    for (int i = 0; i < NotOKHercules.Count(); i++)
                    {
                        var item = new NOKsDto()
                        {
                            NOKs = 1,
                            CodigoCritica = NotOKHercules.ElementAt(i),
                            ChaveUnica = resultadoCriticaHercules.ChaveUnica,
                            GrupoCritica = resultadoCriticaHercules.GrupoCritica
                        };
                        ListNOKs.Add(item);
                    }
                }
            }
        }

        public int GetOKs(int codigoCritica)
        {
            for (int i = 0; i < ListOKs.Count(); i++)
            {
                if (ListOKs[i].CodigoCritica == codigoCritica)
                    return ListOKs[i].OKs;
            }
            return 0;
        }

        public string GetNotPerformed(ResultadoCriticaHerculesDto resultadoCriticaHercules)
        {
            return $"{resultadoCriticaHercules.CriticaNaoExecutada} criticas não foram executadas no Hércules";
        }

        public string GetNOKs(int codigoCritica)
        {
            for (int i = 0; i < ListNOKs.Count(); i++)
            {
                if (ListNOKs[i].CodigoCritica == codigoCritica)
                    return $"O NOK foi do pedido que corresponde à chave {ListNOKs[i].ChaveUnica} com o número da critica {ListNOKs[i].CodigoCritica} no grupo de critica {ListNOKs[i].GrupoCritica}";
            }
            return "Não foi possível encontrar o código informado";
        }

        public async Task<List<ResultadoCriticaPromaxHerculesDto>> Main()
        {
            List<ResultadoCriticaPromaxHerculesDto> resultadoCriticaPromaxHercules = new List<ResultadoCriticaPromaxHerculesDto>();
            var getHercules = await GetHercules();
            var getPromax = await GetPromax();

            for (int i = 0; i < getPromax.Count(); i++)
            {
                CompararPromaxHercules(getHercules[i], getPromax[i]);
            }

            for (int i = 0; i < ListNaoExecutados.Count(); i++)
            {
                resultadoCriticaPromaxHercules.Add(new ResultadoCriticaPromaxHerculesDto()
                {
                    NaoExeceutados = GetNotPerformed(getHercules[i])
                });
            }

            for (int i = 0; i < ListNOKs.Count(); i++)
            {
                resultadoCriticaPromaxHercules.Add(new ResultadoCriticaPromaxHerculesDto()
                {
                    NOKs = GetNOKs(ListNOKs[i].CodigoCritica)
                });
            }

            for (int i = 0; i < ListOKs.Count(); i++)
            {
                resultadoCriticaPromaxHercules.Add(new ResultadoCriticaPromaxHerculesDto()
                {
                    OKs = 1
                });
            }
            return resultadoCriticaPromaxHercules;
        }
    }
}
