using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        #region Nomenclatura dos Testes
        //Tem que ter 3 partes: Given / When / Then
        //Nomemclatura do teste:
        //- Nome da classe do teste: nome da classe + nome do metódo sendo testado
        //- Nome dos métodos do teste: deverá ter em sua nomenclatura o comportamento esperado + o senário que esta sendo testado
        #endregion 

        [Theory]
        [InlineData(800, new double[] { 800 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(1200, new double[] { 800, 900, 990, 1200 })]
        public void RetornarMaiorValorDadoLeileoComPeloMenosUmLance(double valorEsperado, double[] lances)
        {
            //Arrange - Organizar todas as pré-condições e entradas necessárias.
            //Dado o leilão com pelo menos um lance
            IModalidadeAvaliacao modalidade = new MaiorValor();
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

            //Act - Ação no objeto ou método em teste. Qual o método está sendo testado.
            //Quando o leilão termina
            leilao.TerminarPregao();

            //Assert - Verificar os resultados esperados. Afirmar que os resultados esperados ocorreram.
            //Então o valor esperado é o maior valor
            //E o cliente ganhador é o que deu o maior lance
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void RetornarZeroDadoLeilaoSemLances()
        {
            //Arrange - Organizar todas as pré-condições e entradas necessárias.
            //Dado o leilão sem qualquer lance
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciarPregao();

            //Act - Ação no objeto ou método em teste. Qual o método está sendo testado.
            //Quando o leilão termina 
            leilao.TerminarPregao();

            //Assert - Verificar os resultados esperados. Afirmar que os resultados esperados ocorreram.
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arrange - Organizar todas as pré-condições e entradas necessárias.
            //Dado o leilão sem qualquer lance
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);

            //Assert - Verificar os resultados esperados. Afirmar que os resultados esperados ocorreram.
            Assert.Throws<InvalidOperationException>(
                //Act - Ação no objeto ou método em teste. Qual o método está sendo testado.
                //Quando o leilão termina. 
                () => leilao.TerminarPregao() 
            );
        }

        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(double valorDestino, double valorEsperado, double[] lances)
        {
            //Arrange - Organizar todas as pré-condições e entradas necessárias.
            //Dado o leilão cois interessados
            IModalidadeAvaliacao modalidade = new LanceSuperiorMaisProximo(valorDestino);
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

            //Act - Ação no objeto ou método em teste. Qual o método está sendo testado.
            //Quando o leilão termina
            leilao.TerminarPregao();

            //Assert - Verificar os resultados esperados. Afirmar que os resultados esperados ocorreram.
            //Então o valor esperado é o mais próxio ou maior valor
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
