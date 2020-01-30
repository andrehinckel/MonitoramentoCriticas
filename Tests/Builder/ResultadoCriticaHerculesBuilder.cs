using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Builder
{
    public class ResultadoCriticaHerculesBuilder
    {
        public DateTime _data_hora_inicio;
        public DateTime _data_hora_fim;
        public string _grupo_de_critica;
        public int _status;
        public int _codigo_pedido;
        public List<Critica> Critica = new List<Critica>();
        public int _codigo_filial;

        public ResultadoCriticaHercules Construir()
        {
            var critica = Critica.Any() ? Critica : new List<Critica>();

            return new ResultadoCriticaHercules
            {
                data_hora_inicio = _data_hora_inicio,
                data_hora_fim = _data_hora_fim,
                grupo_de_critica = _grupo_de_critica,
                status = _status,
                Codigo_pedido = _codigo_pedido,
                Critica = critica,
                codigo_filial = _codigo_filial
            };
        }

        public ResultadoCriticaHerculesBuilder ComDataHoraInicio(DateTime dataHoraInicio)
        {
            _data_hora_inicio = dataHoraInicio;
            return this;
        }

        public ResultadoCriticaHerculesBuilder ComDataHoraFim(DateTime dataHoraFim)
        {
            _data_hora_fim = dataHoraFim;
            return this;
        }

        public ResultadoCriticaHerculesBuilder ComGrupoCritica(string grupoCritica)
        {
            _grupo_de_critica = grupoCritica;
            return this;
        }

        public ResultadoCriticaHerculesBuilder ComStatus(int status)
        {
            _status = status;
            return this;
        }

        public ResultadoCriticaHerculesBuilder ComCodigoPedido(int codigoPedido)
        {
            _codigo_pedido = codigoPedido;
            return this;
        }

        public ResultadoCriticaHerculesBuilder ComCritica(Critica criticas)
        {
            Critica.Add(criticas);
            return this;
        }

        public ResultadoCriticaHerculesBuilder ComCodigoFilial(int codigoFilial)
        {
            _codigo_filial = codigoFilial;
            return this;
        }
    }

    public class ResultadoCriticaHercules
    {
        public DateTime data_hora_inicio;
        public DateTime data_hora_fim;
        public string grupo_de_critica;
        public int status;
        public int Codigo_pedido;
        public List<Critica> Critica;
        public int codigo_filial;
    }

    public class Critica
    {
        public int alcada;
        public int status;
        public int critica;
    }

    public class CriticaBuilder
    {
        public int _alcada;
        public int _status;
        public int _critica;

        public Critica Construir()
        {
            return new Critica()
            {
                critica = _critica,
                alcada = _alcada,
                status = _status
            };
        }

        public CriticaBuilder ComAlcada(int alcada)
        {
            _alcada = alcada;
            return this;
        }

        public CriticaBuilder ComStatus(int status)
        {
            _status = status;
            return this;
        }

        public CriticaBuilder ComCritica(int critica)
        {
            _critica = critica;
            return this;
        }
    }
}


