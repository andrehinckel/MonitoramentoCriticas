using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Builder
{
    class ResultadoCriticaPromaxBuilder
    {
        public DateTime _data_hora_inicio;
        public DateTime _data_hora_fim;
        public string _grupo_de_critica;
        public int _status;
        public int _codigo_pedido;
        public List<Critica> Critica = new List<Critica>();
        public int _codigo_filial;

        public ResultadoCriticaPromax Construir()
        {
            var critica = Critica.Any() ? Critica : new List<Critica>();

            return new ResultadoCriticaPromax
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

        public ResultadoCriticaPromaxBuilder ComDataHoraInicio(DateTime dataHoraInicio)
        {
            _data_hora_inicio = dataHoraInicio;
            return this;
        }

        public ResultadoCriticaPromaxBuilder ComDataHoraFim(DateTime dataHoraFim)
        {
            _data_hora_fim = dataHoraFim;
            return this;
        }

        public ResultadoCriticaPromaxBuilder ComGrupoCritica(string grupoCritica)
        {
            _grupo_de_critica = grupoCritica;
            return this;
        }

        public ResultadoCriticaPromaxBuilder ComStatus(int status)
        {
            _status = status;
            return this;
        }

        public ResultadoCriticaPromaxBuilder ComCodigoPedido(int codigoPedido)
        {
            _codigo_pedido = codigoPedido;
            return this;
        }

        public ResultadoCriticaPromaxBuilder ComCritica(Critica criticas)
        {
            Critica.Add(criticas);
            return this;
        }

        public ResultadoCriticaPromaxBuilder ComCodigoFilial(int codigoFilial)
        {
            _codigo_filial = codigoFilial;
            return this;
        }
    }

    public class ResultadoCriticaPromax
    {
        public DateTime data_hora_inicio;
        public DateTime data_hora_fim;
        public string grupo_de_critica;
        public int status;
        public int Codigo_pedido;
        public List<Critica> Critica;
        public int codigo_filial;
    }
}

