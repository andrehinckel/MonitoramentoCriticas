using Application.DTOs;
using System;
using System.Linq;

namespace Application.Services
{
    public class ResultadoPromaxHercules : IResultadoPromaxHercules
    {
        int[,] matriz;


        public ResultadoPromaxHercules(int minhaVariavel)
        {
            matriz = new int[minhaVariavel, 3];
        }

        //recebe a lista do promax e do hercules e para cada um dos dois, chama o compararpromaxhercules apos filtrar por data
        public void CompararPromaxHercules(ResultadoCriticaHerculesDto resultadoCriticaHercules, ResultadoCriticaPromaxDto resultadoCriticaPromax)
        {
            if (resultadoCriticaHercules.DataHoraInicio == resultadoCriticaPromax.DataHoraInicio
                && resultadoCriticaHercules.DataHoraFim == resultadoCriticaPromax.DataHoraFim
                && resultadoCriticaHercules.ChaveUnica == resultadoCriticaPromax.ChaveUnica)
            {
                for (int i = 0; i < resultadoCriticaHercules.Criticas.Count(); i++)
                {
                    if (resultadoCriticaHercules.Criticas[i].NumeroCritica == resultadoCriticaPromax.Criticas[i].NumeroCritica
                        && resultadoCriticaHercules.Criticas[i].Status == resultadoCriticaPromax.Criticas[i].Status
                        && resultadoCriticaHercules.Criticas[i].Alcada == resultadoCriticaPromax.Criticas[i].Alcada)
                        matriz[i, 2] = matriz[i, 2] + 1;
                    else
                        matriz[i, 1] = matriz[i, 1] + 1;
                    matriz[i, 0] = resultadoCriticaHercules.Criticas[i].NumeroCritica;
                }

                for (int i = 0; i < resultadoCriticaPromax.Criticas.Count(); i++)
                {
                    if (resultadoCriticaHercules.Criticas[i].NumeroCritica == resultadoCriticaPromax.Criticas[i].NumeroCritica
                        && resultadoCriticaHercules.Criticas[i].Status == resultadoCriticaPromax.Criticas[i].Status
                        && resultadoCriticaHercules.Criticas[i].Alcada == resultadoCriticaPromax.Criticas[i].Alcada)
                        matriz[i, 2] = matriz[i, 2] + 1;
                    else
                        matriz[i, 1] = matriz[i, 1] + 1;
                    matriz[i, 0] = resultadoCriticaPromax.Criticas[i].NumeroCritica;
                }
            }
        }

        public int GetOKs(int codigoCritica)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                if (matriz[i, 0] == codigoCritica)
                    return matriz[i, 2];
            }
            throw new ArgumentException("Não foi possível encontrar o código informado");
        }

        public string GetNOKs(int codigoCritica, ResultadoCriticaHerculesDto resultadoCriticaHercules)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                if (matriz[i, 0] == codigoCritica && resultadoCriticaHercules.Criticas[i].NumeroCritica == codigoCritica)
                    return $"O NOK foi do pedido que corresponde à chave {resultadoCriticaHercules.ChaveUnica} no grupo de critica {resultadoCriticaHercules.GrupoCritica}";
            }
            throw new ArgumentException("Não foi possível encontrar o código informado");
        }
    }
}
