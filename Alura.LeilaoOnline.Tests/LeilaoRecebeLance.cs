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
            //Arrange - Organizar todas as pré-condições e entradas necessárias.
            //Dado o leilão finalizado com X lances
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

            //Act - Ação no objeto ou método em teste. Qual o método está sendo testado.
            //Quando o leilão recebe nova oferta de lance 
            leilao.ReceberLance(marta, 1000);

            //Assert - Verificar os resultados esperados. Afirmar que os resultados esperados ocorreram.
            //Então a quantidade de lances continua sendo X
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtida);
        }

        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //Arrange - Organizar todas as pré-condições e entradas necessárias.
            //Dado o leilão finalizado com X lances
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var marta = new Interessada("Marta", leilao);

            leilao.IniciarPregao();
            leilao.ReceberLance(marta, 800);

            //Act - Ação no objeto ou método em teste. Qual o método está sendo testado.
            //Quando o leilão recebe nova oferta de lance 
            leilao.ReceberLance(marta, 1000);

            //Assert - Verificar os resultados esperados. Afirmar que os resultados esperados ocorreram.
            //Então a quantidade de lances continua sendo X
            var qtdeEsperada = 1;
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtida);
        }
    }
}
