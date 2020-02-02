using Application.DTOs;
using Application.Services;
using Domain.Entity;
using FluentAssertions;
using Infra.Repositories;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests.Builder;

namespace Tests.Tests.DomainTest
{
    public class Teste
    {
        private readonly IResultadoPromaxHercules _comparacaoResultadoPromaxHercules;
        private readonly IResultadoCriticaHerculesRepository _criticaHerculesRepository;
        private readonly IResultadoCriticaPromaxRepository _criticaPromaxRepository;

        public Teste()
        {
            _criticaHerculesRepository = Substitute.For<IResultadoCriticaHerculesRepository>();
            _criticaPromaxRepository = Substitute.For<IResultadoCriticaPromaxRepository>();
            _comparacaoResultadoPromaxHercules = new ResultadoPromaxHercules(_criticaHerculesRepository, _criticaPromaxRepository);
        }

        [Test]
        public void deve_retornar_critics_ok()
        {
            var critica1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12345
            };

            var critica2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12346
            };

            List<Critica> criticas = new List<Critica>();
            criticas.Add(critica1);
            criticas.Add(critica2);

            var resultadoHercules = new ResultadoCriticaHerculesDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 30),
                DataHoraFim = new DateTime(2020 - 01 - 30),
                GrupoCritica = "2",
                Criticas = criticas,
                ChaveUnica = "123456789"
            };

            var resultadoPromax = new ResultadoCriticaPromaxDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 30),
                DataHoraFim = new DateTime(2020 - 01 - 30),
                GrupoCritica = "2",
                Criticas = criticas,
                ChaveUnica = "123456789"
            };

            _comparacaoResultadoPromaxHercules.CompararPromaxHercules(resultadoHercules, resultadoPromax);

            _comparacaoResultadoPromaxHercules.GetOKs(12345).Should().Be(1);
            _comparacaoResultadoPromaxHercules.GetOKs(12346).Should().Be(1);
        }

        [Test]
        public void deve_retornar_critics_nok_quando_promax_e_hercules_sao_diferentes()
        {
            //hercules
            var critica1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12345
            };

            //promax
            var critica2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12346
            };

            List<Critica> criticas = new List<Critica>();
            criticas.Add(critica1);

            List<Critica> criticas2 = new List<Critica>();
            criticas2.Add(critica2);

            var resultadoHercules = new ResultadoCriticaHerculesDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 30),
                DataHoraFim = new DateTime(2020 - 01 - 30),
                GrupoCritica = "2",
                Criticas = criticas,
                ChaveUnica = "123456789"
            };

            var resultadoPromax = new ResultadoCriticaPromaxDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 30),
                DataHoraFim = new DateTime(2020 - 01 - 30),
                GrupoCritica = "2",
                Criticas = criticas2,
                ChaveUnica = "123456789"
            };

            _comparacaoResultadoPromaxHercules.CompararPromaxHercules(resultadoHercules, resultadoPromax);

            _comparacaoResultadoPromaxHercules.GetOKs(12345).Should().Be(0);
            _comparacaoResultadoPromaxHercules.GetOKs(12346).Should().Be(0);

            _comparacaoResultadoPromaxHercules.GetNOKs(12345).Should().Be($"O NOK foi do pedido que corresponde à chave {resultadoHercules.ChaveUnica} com o número da critica {12345} no grupo de critica {resultadoHercules.GrupoCritica}");
            _comparacaoResultadoPromaxHercules.GetNOKs(12346).Should().Be($"O NOK foi do pedido que corresponde à chave {resultadoHercules.ChaveUnica} com o número da critica {12346} no grupo de critica {resultadoHercules.GrupoCritica}");
        }

        [Test]
        public void deve_retornar_listas_criticas_noks_oks_promax_hercules()
        {
            //HerculesA
            var criticaHerculesA1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12340
            };

            var criticaHerculesA2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12341
            };

            //HerculesB
            var criticaHerculesB1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12343
            };

            var criticaHerculesB2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12347
            };

            //PromaxA
            var criticaPromaxA1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12346
            };

            var criticaPromaxA2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12341
            };

            var criticaPromaxA3 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12348
            };

            //PromaxB
            var criticaPromaxB1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12349
            };

            var criticaPromaxB2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12347
            };

            var criticaPromaxB3 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 123411
            };

            List<Critica> criticasHerculesA = new List<Critica>();
            criticasHerculesA.Add(criticaHerculesA1);
            criticasHerculesA.Add(criticaHerculesA2);

            List<Critica> criticasHerculesB = new List<Critica>();
            criticasHerculesB.Add(criticaHerculesB1);
            criticasHerculesB.Add(criticaHerculesB2);

            List<Critica> criticasPromaxA = new List<Critica>();
            criticasPromaxA.Add(criticaPromaxA1);
            criticasPromaxA.Add(criticaPromaxA2);
            criticasPromaxA.Add(criticaPromaxA3);

            List<Critica> criticasPromaxB = new List<Critica>();
            criticasPromaxB.Add(criticaPromaxB1);
            criticasPromaxB.Add(criticaPromaxB2);
            criticasPromaxB.Add(criticaPromaxB3);

            var resultadoHerculesA = new ResultadoCriticaHerculesDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 30),
                DataHoraFim = new DateTime(2020 - 01 - 30),
                GrupoCritica = "2",
                Criticas = criticasHerculesA,
                ChaveUnica = "1234567890"
            };

            var resultadoHerculesB = new ResultadoCriticaHerculesDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 31),
                DataHoraFim = new DateTime(2020 - 02 - 01),
                GrupoCritica = "1",
                Criticas = criticasHerculesB,
                ChaveUnica = "1234567891"
            };

            var resultadoPromaxA = new ResultadoCriticaPromaxDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 30),
                DataHoraFim = new DateTime(2020 - 01 - 30),
                GrupoCritica = "2",
                Criticas = criticasPromaxA,
                ChaveUnica = "1234567890"
            };

            var resultadoPromaxB = new ResultadoCriticaPromaxDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 31),
                DataHoraFim = new DateTime(2020 - 02 - 01),
                GrupoCritica = "1",
                Criticas = criticasPromaxB,
                ChaveUnica = "1234567891"
            };

            _comparacaoResultadoPromaxHercules.CompararPromaxHercules(resultadoHerculesA, resultadoPromaxA);
            _comparacaoResultadoPromaxHercules.GetOKs(12341).Should().Be(1);

            _comparacaoResultadoPromaxHercules.GetNOKs(12340).Should()
                .Be($"O NOK foi do pedido que corresponde à chave {resultadoHerculesA.ChaveUnica} com o número da critica {12340} no grupo de critica {resultadoHerculesA.GrupoCritica}");
            _comparacaoResultadoPromaxHercules.GetNOKs(12346).Should()
                .Be($"O NOK foi do pedido que corresponde à chave {resultadoHerculesA.ChaveUnica} com o número da critica {12346} no grupo de critica {resultadoHerculesA.GrupoCritica}");
            _comparacaoResultadoPromaxHercules.GetNOKs(12348).Should()
                .Be($"O NOK foi do pedido que corresponde à chave {resultadoHerculesA.ChaveUnica} com o número da critica {12348} no grupo de critica {resultadoHerculesA.GrupoCritica}");

            _comparacaoResultadoPromaxHercules.CompararPromaxHercules(resultadoHerculesB, resultadoPromaxB);
            _comparacaoResultadoPromaxHercules.GetOKs(12347).Should().Be(1);
            _comparacaoResultadoPromaxHercules.GetNOKs(12349).Should()
                .Be($"O NOK foi do pedido que corresponde à chave {resultadoHerculesB.ChaveUnica} com o número da critica {12349} no grupo de critica {resultadoHerculesB.GrupoCritica}");
            _comparacaoResultadoPromaxHercules.GetNOKs(123411).Should()
                .Be($"O NOK foi do pedido que corresponde à chave {resultadoHerculesB.ChaveUnica} com o número da critica {123411} no grupo de critica {resultadoHerculesB.GrupoCritica}");
            _comparacaoResultadoPromaxHercules.GetNOKs(12343).Should()
                .Be($"O NOK foi do pedido que corresponde à chave {resultadoHerculesB.ChaveUnica} com o número da critica {12343} no grupo de critica {resultadoHerculesB.GrupoCritica}");
        }

        [Test]
        public void deve_retornar_criticas_noks_oks_quando_promax_hercules()
        {
            //HerculesA
            var criticaHerculesA1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12340
            };

            var criticaHerculesA2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12341
            };

            //PromaxA
            var criticaPromaxA1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12346
            };

            var criticaPromaxA2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12341
            };

            var criticaPromaxA3 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12348
            };

            List<Critica> criticasHerculesA = new List<Critica>();
            criticasHerculesA.Add(criticaHerculesA1);
            criticasHerculesA.Add(criticaHerculesA2);

            List<Critica> criticasPromaxA = new List<Critica>();
            criticasPromaxA.Add(criticaPromaxA1);
            criticasPromaxA.Add(criticaPromaxA2);
            criticasPromaxA.Add(criticaPromaxA3);

            var resultadoHerculesA = new ResultadoCriticaHerculesDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 30),
                DataHoraFim = new DateTime(2020 - 01 - 30),
                GrupoCritica = "2",
                Criticas = criticasHerculesA,
                ChaveUnica = "1234567890"
            };

            var resultadoPromaxA = new ResultadoCriticaPromaxDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 30),
                DataHoraFim = new DateTime(2020 - 01 - 30),
                GrupoCritica = "2",
                Criticas = criticasPromaxA,
                ChaveUnica = "1234567890"
            };

            _comparacaoResultadoPromaxHercules.CompararPromaxHercules(resultadoHerculesA, resultadoPromaxA);
            _comparacaoResultadoPromaxHercules.GetOKs(12341).Should().Be(1);

            _comparacaoResultadoPromaxHercules.GetNOKs(12340).Should()
                .Be($"O NOK foi do pedido que corresponde à chave {resultadoHerculesA.ChaveUnica} com o número da critica {12340} no grupo de critica {resultadoHerculesA.GrupoCritica}");
            _comparacaoResultadoPromaxHercules.GetNOKs(12346).Should()
                .Be($"O NOK foi do pedido que corresponde à chave {resultadoHerculesA.ChaveUnica} com o número da critica {12346} no grupo de critica {resultadoHerculesA.GrupoCritica}");
            _comparacaoResultadoPromaxHercules.GetNOKs(12348).Should()
                .Be($"O NOK foi do pedido que corresponde à chave {resultadoHerculesA.ChaveUnica} com o número da critica {12348} no grupo de critica {resultadoHerculesA.GrupoCritica}");
        }

        [Test]
        public void deve_retornar_critica_nao_executada_()
        {
            //Promax
            var critica2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12346
            };

            var critica3 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12347
            };

            var critica5 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12348
            };

            List<Critica> criticas = new List<Critica>();

            List<Critica> criticas2 = new List<Critica>();
            criticas2.Add(critica2);
            criticas2.Add(critica3);
            criticas2.Add(critica5);

            var resultadoHercules = new ResultadoCriticaHerculesDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 30),
                DataHoraFim = new DateTime(2020 - 01 - 30),
                GrupoCritica = "2",
                Criticas = criticas,
                ChaveUnica = "123456789",
                CriticaNaoExecutada = 0
            };

            var resultadoPromax = new ResultadoCriticaPromaxDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 30),
                DataHoraFim = new DateTime(2020 - 01 - 30),
                GrupoCritica = "2",
                Criticas = criticas2,
                ChaveUnica = "123456789"
            };

            _comparacaoResultadoPromaxHercules.CompararPromaxHercules(resultadoHercules, resultadoPromax);

            _comparacaoResultadoPromaxHercules.GetNotPerformed(resultadoHercules).Should().Be($"{3} criticas não foram executadas no Hércules");
        }

        [Test]
        public async Task deve_retornar_listas_de_criticas_promax_repository()
        {
            //PromaxA
            var criticaPromaxA1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12346
            };

            var criticaPromaxA2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12341
            };

            var criticaPromaxA3 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12348
            };

            //PromaxB
            var criticaPromaxB1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12349
            };

            var criticaPromaxB2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12347
            };

            var criticaPromaxB3 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 123411
            };

            List<Critica> criticasPromaxA = new List<Critica>();
            criticasPromaxA.Add(criticaPromaxA1);
            criticasPromaxA.Add(criticaPromaxA2);
            criticasPromaxA.Add(criticaPromaxA3);

            List<Critica> criticasPromaxB = new List<Critica>();
            criticasPromaxA.Add(criticaPromaxB1);
            criticasPromaxA.Add(criticaPromaxB2);
            criticasPromaxA.Add(criticaPromaxB3);

            var resultadoPromaxA = new ResultadoCriticaPromaxDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 30),
                DataHoraFim = new DateTime(2020 - 01 - 30),
                Criticas = criticasPromaxA,
                ChaveUnica = "1234567890",
                GrupoCritica = "abc"
            };

            var resultadoPromaxB = new ResultadoCriticaPromaxDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 31),
                DataHoraFim = new DateTime(2020 - 02 - 01),
                Criticas = criticasPromaxB,
                ChaveUnica = "1234567891",
                GrupoCritica = "abc"
            };

            List<ResultadoCriticaPromaxDto> resultadoRepositoryPromax = new List<ResultadoCriticaPromaxDto>();
            resultadoRepositoryPromax.Add(resultadoPromaxA);
            resultadoRepositoryPromax.Add(resultadoPromaxB);

            _criticaPromaxRepository.ObterTodasAsCriticasPromax().Returns(resultadoRepositoryPromax.Select(x => new ResultadoCriticaPromax
            {
                ChaveUnica = x.ChaveUnica,
                Criticas = x.Criticas,
                DataHoraFim = x.DataHoraFim,
                DataHoraInicio = x.DataHoraInicio,
                GrupoCritica = x.GrupoCritica
            }).ToList());

            var result = await _comparacaoResultadoPromaxHercules.GetPromax();

            result.Should().HaveCount(2);
        }

        [Test]
        public async Task deve_retornar_listas_de_criticas_hercules_repository()
        {
            //HerculesA
            var criticaHerculesA1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12340
            };

            var criticaHerculesA2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12341
            };

            //HerculesB
            var criticaHerculesB1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12343
            };

            var criticaHerculesB2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12347
            };

            List<Critica> criticasHerculesA = new List<Critica>();
            criticasHerculesA.Add(criticaHerculesA1);
            criticasHerculesA.Add(criticaHerculesA2);

            List<Critica> criticasHerculesB = new List<Critica>();
            criticasHerculesA.Add(criticaHerculesB1);
            criticasHerculesA.Add(criticaHerculesB2);

            var resultadoHerculesA = new ResultadoCriticaHerculesDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 30),
                DataHoraFim = new DateTime(2020 - 01 - 30),
                GrupoCritica = "2",
                Criticas = criticasHerculesA,
                ChaveUnica = "1234567890"
            };

            var resultadoHerculesB = new ResultadoCriticaHerculesDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 31),
                DataHoraFim = new DateTime(2020 - 02 - 01),
                GrupoCritica = "1",
                Criticas = criticasHerculesB,
                ChaveUnica = "1234567891"
            };

            List<ResultadoCriticaHerculesDto> resultadoRepositoryHercules = new List<ResultadoCriticaHerculesDto>();
            resultadoRepositoryHercules.Add(resultadoHerculesA);
            resultadoRepositoryHercules.Add(resultadoHerculesB);

            _criticaHerculesRepository.ObterTodasAsCriticasHercules().Returns(resultadoRepositoryHercules.Select(x => new ResultadoCriticaHercules
            {
                ChaveUnica = x.ChaveUnica,
                Criticas = x.Criticas,
                DataHoraFim = x.DataHoraFim,
                DataHoraInicio = x.DataHoraInicio,
                GrupoCritica = x.GrupoCritica
            }).ToList());

            var result = await _comparacaoResultadoPromaxHercules.GetHercules();

            result.Should().HaveCount(2);
        }

        [Test]
        public async Task deve_retornar_ok_para_cada_item_das_listas_dos_repositorios_hercules_promax()
        {
            //PromaxA
            var criticaPromaxA1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12346
            };

            var criticaPromaxA2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12341
            };

            var criticaPromaxA3 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12348
            };

            //PromaxB
            var criticaPromaxB1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12349
            };

            var criticaPromaxB2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12347
            };

            var criticaPromaxB3 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 123411
            };

            List<Critica> criticasPromaxA = new List<Critica>();
            criticasPromaxA.Add(criticaPromaxA1);
            criticasPromaxA.Add(criticaPromaxA2);
            criticasPromaxA.Add(criticaPromaxA3);

            List<Critica> criticasPromaxB = new List<Critica>();
            criticasPromaxB.Add(criticaPromaxB1);
            criticasPromaxB.Add(criticaPromaxB2);
            criticasPromaxB.Add(criticaPromaxB3);

            var resultadoPromaxA = new ResultadoCriticaPromaxDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 30),
                DataHoraFim = new DateTime(2020 - 01 - 30),
                Criticas = criticasPromaxA,
                ChaveUnica = "1234567890",
                GrupoCritica = "abc"
            };

            var resultadoPromaxB = new ResultadoCriticaPromaxDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 31),
                DataHoraFim = new DateTime(2020 - 02 - 01),
                Criticas = criticasPromaxB,
                ChaveUnica = "1234567891",
                GrupoCritica = "abc"
            };

            List<ResultadoCriticaPromaxDto> resultadoRepositoryPromax = new List<ResultadoCriticaPromaxDto>();
            resultadoRepositoryPromax.Add(resultadoPromaxA);
            resultadoRepositoryPromax.Add(resultadoPromaxB);

            _criticaPromaxRepository.ObterTodasAsCriticasPromax().Returns(resultadoRepositoryPromax.Select(x => new ResultadoCriticaPromax
            {
                ChaveUnica = x.ChaveUnica,
                Criticas = x.Criticas,
                DataHoraFim = x.DataHoraFim,
                DataHoraInicio = x.DataHoraInicio,
                GrupoCritica = x.GrupoCritica
            }).ToList());

            //HerculesA
            var criticaHerculesA1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12340
            };

            var criticaHerculesA2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12341
            };

            //HerculesB
            var criticaHerculesB1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12343
            };

            var criticaHerculesB2 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12347
            };

            List<Critica> criticasHerculesA = new List<Critica>();
            criticasHerculesA.Add(criticaHerculesA1);
            criticasHerculesA.Add(criticaHerculesA2);

            List<Critica> criticasHerculesB = new List<Critica>();
            criticasHerculesB.Add(criticaHerculesB1);
            criticasHerculesB.Add(criticaHerculesB2);

            var resultadoHerculesA = new ResultadoCriticaHerculesDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 30),
                DataHoraFim = new DateTime(2020 - 01 - 30),
                GrupoCritica = "2",
                Criticas = criticasHerculesA,
                ChaveUnica = "1234567890"
            };

            var resultadoHerculesB = new ResultadoCriticaHerculesDto()
            {
                DataHoraInicio = new DateTime(2020 - 01 - 31),
                DataHoraFim = new DateTime(2020 - 02 - 01),
                GrupoCritica = "1",
                Criticas = criticasHerculesB,
                ChaveUnica = "1234567891"
            };

            List<ResultadoCriticaHerculesDto> resultadoRepositoryHercules = new List<ResultadoCriticaHerculesDto>();
            resultadoRepositoryHercules.Add(resultadoHerculesA);
            resultadoRepositoryHercules.Add(resultadoHerculesB);

            _criticaHerculesRepository.ObterTodasAsCriticasHercules().Returns(resultadoRepositoryHercules.Select(x => new ResultadoCriticaHercules
            {
                ChaveUnica = x.ChaveUnica,
                Criticas = x.Criticas,
                DataHoraFim = x.DataHoraFim,
                DataHoraInicio = x.DataHoraInicio,
                GrupoCritica = x.GrupoCritica
            }).ToList());

            var retorno = await _comparacaoResultadoPromaxHercules.Main();

            retorno.Should().HaveCount(4);
        }
    }
}
