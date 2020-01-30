using Domain.Entity;
using System;
using System.Collections.Generic;

namespace Infra.Mongo
{
    public class Collections
    {
        private static readonly Dictionary<Type, string> Mapeamento = new Dictionary<Type, string>
        {
            {typeof(ResultadoCriticaHercules), "resultados_criticas" },
            {typeof(ResultadoCriticaPromax), "resultados_criticas_promax" }
        };
    }
}
