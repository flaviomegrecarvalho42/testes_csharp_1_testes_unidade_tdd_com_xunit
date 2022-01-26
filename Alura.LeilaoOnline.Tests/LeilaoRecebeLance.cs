using Alura.LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Theory]
        [InlineData(2, new double[] { 800, 900 })]
        [InlineData(4, new double[] { 1000, 1200, 1400, 1300 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdeEsperada, double[] lances)
        {
            #region Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var marta = new Interessada("Marta", leilao);
            var priscila = new Interessada("Priscila", leilao);

            leilao.IniciarPregao();

            for (int i = 0; i < lances.Length; i++)
            {
                var lance = lances[i];

                if ((i % 2) == 0)
                {
                    leilao.ReceberLance(marta, lance);
                }
                else
                {
                    leilao.ReceberLance(priscila, lance);
                }
            }

            leilao.TerminarPregao();
            #endregion

            #region Act
            leilao.ReceberLance(marta, 1000);
            #endregion

            #region Assert
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtida);
            #endregion
        }

        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            #region Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var marta = new Interessada("Marta", leilao);

            leilao.IniciarPregao();
            leilao.ReceberLance(marta, 800);
            #endregion

            #region Act
            leilao.ReceberLance(marta, 1000);
            #endregion

            #region Assert
            var qtdeEsperada = 1;
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtida);
            #endregion
        }
    }
}
