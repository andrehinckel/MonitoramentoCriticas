using Application.DTOs;
using Application.Services;
using Domain.Entity;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests.Tests.DomainTest
{
    public class Teste
    {
        private readonly IResultadoPromaxHercules _comparacaoResultadoPromaxHercules;

        public Teste()
        {
            _comparacaoResultadoPromaxHercules = new ResultadoPromaxHercules(10);

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

            _comparacaoResultadoPromaxHercules.GetNOKs(12345, resultadoHercules).Should().Be($"O NOK foi do pedido que corresponde à chave {resultadoHercules.ChaveUnica} com o número da critica {12345} no grupo de critica {resultadoHercules.GrupoCritica}");
            _comparacaoResultadoPromaxHercules.GetNOKs(12346, resultadoHercules).Should().Be($"O NOK foi do pedido que corresponde à chave {resultadoHercules.ChaveUnica} com o número da critica {12346} no grupo de critica {resultadoHercules.GrupoCritica}");
        }

        [Test]
        public void deve_retornar_criticas_noks_oks_quando_promax_hercules()
        {
            //Hercules
            var critica1 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12345
            };

            var critica4 = new Critica()
            {
                Alcada = 2,
                Status = 5,
                NumeroCritica = 12347
            };

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
            criticas.Add(critica1);
            criticas.Add(critica4);

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

            _comparacaoResultadoPromaxHercules.GetOKs(12347).Should().Be(1);

            _comparacaoResultadoPromaxHercules.GetNOKs(12345, resultadoHercules).Should().Be($"O NOK foi do pedido que corresponde à chave {resultadoHercules.ChaveUnica} com o número da critica {12345} no grupo de critica {resultadoHercules.GrupoCritica}");
            _comparacaoResultadoPromaxHercules.GetNOKs(12346, resultadoHercules).Should().Be($"O NOK foi do pedido que corresponde à chave {resultadoHercules.ChaveUnica} com o número da critica {12346} no grupo de critica {resultadoHercules.GrupoCritica}");
            _comparacaoResultadoPromaxHercules.GetNOKs(12348, resultadoHercules).Should().Be($"O NOK foi do pedido que corresponde à chave {resultadoHercules.ChaveUnica} com o número da critica {12348} no grupo de critica {resultadoHercules.GrupoCritica}");
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

            _comparacaoResultadoPromaxHercules.GetNotPerformed(resultadoHercules).Should().Be($"{2} criticas não foram executadas no Hércules");
        }
    }
}
