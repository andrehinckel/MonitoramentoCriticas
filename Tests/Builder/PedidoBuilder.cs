namespace Tests.Builder
{
    public class PedidoBuilder
    {
        public int _codigo;

        public Pedido Construir()
        {
            return new Pedido()
            {
                Codigo = _codigo
            };
        }

        public PedidoBuilder ComCodigo(int codigo)
        {
            _codigo = codigo;
            return this;
        }
    }

    public class Pedido
    {
        public int Codigo;
    }
}
