using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Builder;

namespace Tests.Tests.DomainTest
{
    public class Teste
    {
        [Test]
        public void deve_retornar_ok()
        {
            var resultadoHercules = new ResultadoCriticaHerculesBuilder()
                .ComDataHoraInicio(new DateTime(2020 - 01 - 30))
                .ComDataHoraFim(new DateTime(2020 - 01 - 30))
                .ComCodigoPedido(1)
                .ComCritica(new CriticaBuilder()
                .ComAlcada(1)
                .ComCritica(1)
                .ComStatus(1)
                .Construir()).Construir();

            var resultadoPromax =  new ResultadoCriticaPromaxBuilder()
                .ComDataHoraInicio(new DateTime(2020 - 01 - 30))
                .ComDataHoraFim(new DateTime(2020 - 01 - 30))
                .ComCodigoPedido(1)
                .ComCritica(new CriticaBuilder()
                .ComAlcada(1)
                .ComCritica(1)
                .ComStatus(1)
                .Construir()).Construir();

            resultadoHercules.data_hora_inicio.Should().Equals(resultadoPromax.data_hora_inicio);
            resultadoHercules.Codigo_pedido.Should().Equals(resultadoPromax.Codigo_pedido);
            resultadoHercules.data_hora_fim.Should().Equals(resultadoPromax.data_hora_fim);
            resultadoHercules.Codigo_pedido.Should().Equals(resultadoPromax.Codigo_pedido);
        }
    }
}
