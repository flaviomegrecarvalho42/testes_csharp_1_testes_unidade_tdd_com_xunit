using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            //Arrange - Organizar todas as pré-condições e entradas necessárias.
            var valorNegativo = -100;

            //Assert - Verificar os resultados esperados. Afirmar que os resultados esperados ocorreram.
            Assert.Throws<ArgumentException>(
                //Act - Ação no objeto ou método em teste. Qual o método está sendo testado.
                () => new Lance(null, valorNegativo)
            );
        }
    }
}
