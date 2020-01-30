using System;
using System.Collections.Generic;

namespace Domain.Entity
{
    public abstract class ResultadoCriticaBase
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
