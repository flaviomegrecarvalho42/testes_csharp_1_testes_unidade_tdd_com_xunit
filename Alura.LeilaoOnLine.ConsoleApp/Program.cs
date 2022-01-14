using Alura.LeilaoOnline.Core;
using System;

namespace Alura.LeilaoOnLine.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            LeilaoComVariosLances();
            LeilaoComApenasUmLance();
        }

        private static void LeilaoComVariosLances()
        {
            //Arrange - Organizar todas as pré-condições e entradas necessárias.
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var marta = new Interessada("Marta", leilao);
            var priscila = new Interessada("Priscila", leilao);

            leilao.ReceberLance(marta, 800);
            leilao.ReceberLance(priscila, 900);
            leilao.ReceberLance(marta, 1000);
            leilao.ReceberLance(marta, 990);

            //Act - Ação no objeto ou método em teste. Qual o método está sendo testado.
            leilao.TerminarPregao();

            //Assert - Verificar os resultados esperados. Afirmar que os resultados esperados ocorreram.
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;
            VerificarValoresLances(valorEsperado, valorObtido);
        }

        private static void LeilaoComApenasUmLance()
        {
            //Arrange - Organizar todas as pré-condições e entradas necessárias.
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var marta = new Interessada("Marta", leilao);

            leilao.ReceberLance(marta, 800);

            //Act - Ação no objeto ou método em teste. Qual o método está sendo testado.
            leilao.TerminarPregao();

            //Assert - Verificar os resultados esperados. Afirmar que os resultados esperados ocorreram.
            var valorEsperado = 800;
            var valorObtido = leilao.Ganhador.Valor;
            VerificarValoresLances(valorEsperado, valorObtido);
        }

        private static void VerificarValoresLances(int valorEsperado, double valorObtido)
        {
            var corConsole = Console.ForegroundColor;

            if (valorEsperado == valorObtido)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Teste OK!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Teste Falhou! Valor esperado: {valorEsperado}. Valor obtido: {valorObtido}");
            }

            Console.ForegroundColor = corConsole;
        }
    }
}
