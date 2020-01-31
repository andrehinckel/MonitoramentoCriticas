using Domain.Entity;
using System;
using System.Collections.Generic;

namespace Infra.Mongo
{
    public class Colecoes
    {
        private static readonly Dictionary<Type, string> Mapeamento = new Dictionary<Type, string>
        {
            {typeof(ResultadoCriticaHercules), "resultados_criticas" },
            {typeof(ResultadoCriticaPromax), "resultados_criticas_promax" }
        };

        public static string ObterNomeColecao<TDocument>()
           where TDocument : IEntidade
        {
            var documentType = typeof(TDocument);

            if (Mapeamento.ContainsKey(documentType))
                return Mapeamento[documentType];

            throw new InvalidOperationException($"Coleção não possui nome especificado {typeof(TDocument).Name}");
        }
    }
}
