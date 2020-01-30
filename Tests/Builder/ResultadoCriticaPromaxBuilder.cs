using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Builder
{
    class ResultadoCriticaPromaxBuilder
    {
        public DateTime _dataHoraInicio;
        public DateTime _dataHoraFim;
        public string _grupoDeCritica;
        public int _status;
        public int _codigoPedido;
        public List<Critica> Criticas = new List<Critica>();
        public int _codigoFilial;

        public ResultadoCriticaPromax Construir()
        {
            var critica = Criticas.Any() ? Criticas : new List<Critica>();

            return new ResultadoCriticaPromax
            {
                data_hora_inicio = _dataHoraInicio,
                data_hora_fim = _dataHoraFim,
                grupo_de_critica = _grupoDeCritica,
                status = _status,
                Codigo_pedido = _codigoPedido,
                Critica = critica,
                codigo_filial = _codigoFilial
            };
        }

        public ResultadoCriticaPromaxBuilder ComDataHoraInicio(DateTime dataHoraInicio)
        {
            _dataHoraInicio = dataHoraInicio;
            return this;
        }

        public ResultadoCriticaPromaxBuilder ComDataHoraFim(DateTime dataHoraFim)
        {
            _dataHoraFim = dataHoraFim;
            return this;
        }

        public ResultadoCriticaPromaxBuilder ComGrupoCritica(string grupoCritica)
        {
            _grupoDeCritica = grupoCritica;
            return this;
        }

        public ResultadoCriticaPromaxBuilder ComStatus(int status)
        {
            _status = status;
            return this;
        }

        public ResultadoCriticaPromaxBuilder ComCodigoPedido(int codigoPedido)
        {
            _codigoPedido = codigoPedido;
            return this;
        }

        public ResultadoCriticaPromaxBuilder ComCritica(Critica criticas)
        {
            Criticas.Add(criticas);
            return this;
        }

        public ResultadoCriticaPromaxBuilder ComCodigoFilial(int codigoFilial)
        {
            _codigoFilial = codigoFilial;
            return this;
        }
    }
}

