using Domain.Entity;

namespace Tests.Builder
{
    public class CriticaBuilder
    {
        public int _alcada;
        public int _status;
        public int _critica;

        public Critica Construir()
        {
            return new Critica()
            {
                NumeroCritica = _critica,
                Alcada = _alcada,
                Status = _status
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
