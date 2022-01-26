using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class LanceSuperiorMaisProximo : IModalidadeAvaliacao
    {
        public LanceSuperiorMaisProximo(double valorDestino)
        {
            ValorDestino = valorDestino;
        }

        public double ValorDestino { get; }

        public Lance Avaliar(Leilao leilao)
        {
            return leilao.Lances.DefaultIfEmpty(new Lance(null, 0))
                                .Where(l => l.Valor > ValorDestino)
                                .OrderBy(l => l.Valor)
                                .FirstOrDefault();
        }
    }
}
