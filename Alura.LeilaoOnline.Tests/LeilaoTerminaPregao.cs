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
            #region Arrange
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
            #endregion

            #region Act
            leilao.TerminarPregao();
            #endregion

            #region Assert
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
            #endregion
        }

        [Fact]
        public void RetornarZeroDadoLeilaoSemLances()
        {
            #region Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciarPregao();
            #endregion

            #region Act
            leilao.TerminarPregao();
            #endregion

            #region Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
            #endregion
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            #region Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            #endregion

            #region Assert
            Assert.Throws<InvalidOperationException>(
                //Act
                () => leilao.TerminarPregao() 
            );
            #endregion
        }

        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(double valorDestino, double valorEsperado, double[] lances)
        {
            #region Arrange
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
            #endregion

            #region Act
            leilao.TerminarPregao();
            #endregion

            #region Assert
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
            #endregion
        }
    }
}
