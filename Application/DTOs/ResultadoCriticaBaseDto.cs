using Domain.Entity;
using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public abstract class ResultadoCriticaBaseDto
    {
        public DateTime DataHoraInicio;
        public DateTime DataHoraFim;
        public string GrupoCritica;
        public List<Critica> Criticas;
        public string ChaveUnica;
    }
}
