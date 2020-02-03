using Application.Services;
using Infra.Repositories;
using System;
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
            int oks = 0;

            foreach (var item in teste)
            {
                if (item.OKs != 0)
                    oks++;
                else if (item.NaoExeceutados != null)
                    Console.WriteLine($"Não Executados: {item.NaoExeceutados}");
                else
                    Console.WriteLine($"{item.NOKs}");

            }
            Console.WriteLine($"Quantidade Total de OKs: {oks}");
        }
    }
}
