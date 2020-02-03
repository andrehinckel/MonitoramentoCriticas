using Application.DTOs;
using Application.Services;
using Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonitoramentoCriticas
{
    class Program
    {
        private static IResultadoCriticaHerculesRepository _resultadoCriticaHercules;
        private static IResultadoCriticaPromaxRepository _resultadoCriticaPromax;

        static async Task Main(string[] args)
        {
            var service = new ResultadoPromaxHercules(_resultadoCriticaHercules, _resultadoCriticaPromax);

            var teste = await service.Main();

            foreach (var item in teste)
            {
                Console.WriteLine($"{item}");
            }
        }
    }
}
