using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Builder
{
    public class ResultadoCriticaHerculesBuilder
    {
        public DateTime _dataHoraInicio;
        public DateTime _dataHoraFim;
        public string _grupoDeCritica;
        public int _status;
        public int _codigoPedido;
        public List<Critica> Criticas = new List<Critica>();
        public int _codigoFilial;

        public ResultadoCriticaHercules Construir()
        {
            var critica = Criticas.Any() ? Criticas : new List<Critica>();

            return new ResultadoCriticaHercules
            {
                DataHoraInicio = _dataHoraInicio,
                DataHoraFim = _dataHoraFim,
                GrupoCritica = _grupoDeCritica,
                Status = _status,
                CodigoPedido = _codigoPedido,
                Criticas = critica,
                CodigoFilial = _codigoFilial
            };
        }

        public ResultadoCriticaHerculesBuilder ComDataHoraInicio(DateTime dataHoraInicio)
        {
            _dataHoraInicio = dataHoraInicio;
            return this;
        }

        public ResultadoCriticaHerculesBuilder ComDataHoraFim(DateTime dataHoraFim)
        {
            _dataHoraFim = dataHoraFim;
            return this;
        }

        public ResultadoCriticaHerculesBuilder ComGrupoCritica(string grupoCritica)
        {
            _grupoDeCritica = grupoCritica;
            return this;
        }

        public ResultadoCriticaHerculesBuilder ComStatus(int status)
        {
            _status = status;
            return this;
        }

        public ResultadoCriticaHerculesBuilder ComCodigoPedido(int codigoPedido)
        {
            _codigoPedido = codigoPedido;
            return this;
        }

        public ResultadoCriticaHerculesBuilder ComCritica(Critica criticas)
        {
            Criticas.Add(criticas);
            return this;
        }

        public ResultadoCriticaHerculesBuilder ComCodigoFilial(int codigoFilial)
        {
            _codigoFilial = codigoFilial;
            return this;
        }
    }
}


