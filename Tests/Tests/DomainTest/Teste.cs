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
                .ComDataHoraFim(new DateTime(2020 - 01 - 30)).Construir();
        }
    }
}
