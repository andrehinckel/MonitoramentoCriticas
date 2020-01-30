using System;

namespace Application.DTOs
{
    public class ResultadoCriticaDto
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int SomaResultadoPromax { get; set; }
        public int SomaResultadoHercules { get; set; }
        public bool Ok { get; set; }
    }
}
