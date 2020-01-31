using System;
using System.Collections.Generic;

namespace Domain.Entity
{
    public abstract class ResultadoCriticaBase
    {
        public DateTime DataHoraInicio;
        public DateTime DataHoraFim;
        public string GrupoCritica;
        public int Status;
        public int CodigoPedido;
        public string ChaveUnica;
        public List<Critica> Criticas;
        public int CodigoFilial;
    }
}
