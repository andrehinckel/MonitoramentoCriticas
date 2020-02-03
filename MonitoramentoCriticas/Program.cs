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
        private static IResultadoPromaxHercules _resultadoPromaxHercules;
        private readonly IResultadoCriticaHerculesRepository _resultadoCriticaHercules;
        private readonly IResultadoCriticaPromaxRepository _resultadoCriticaPromax;

        public Program()
        {
            _resultadoPromaxHercules = new ResultadoPromaxHercules(_resultadoCriticaHercules, _resultadoCriticaPromax);
        }

        static async Task Main(string[] args)
        {
            var teste = await _resultadoPromaxHercules.Main();

            foreach (var item in teste)
            {
                Console.WriteLine($"{item}");
            }
        }
    }
}
