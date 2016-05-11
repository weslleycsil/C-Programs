using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplicadorBooth
{
    class Program
    {
        private static int[] And(int[] bin1, int[] bin2)
        {
            int[] result = new int[bin1.Length]; //instancio o vetor resultado do AND logico com o mesmo tamanho de bin1

            for (int i = bin1.Length - 1; i >= 0; i--)
            {
                //percorro do fim pro inicio os vetores fazendo o teste logico bit por bit
                result[i] = bin1[i] & bin2[i];
                //armazeno no vetor result o resultado do AND entre Bin1 e Bin2
            }
            return result;
        }

        private static int[] Xor(int[] bin1, int[] bin2)
        {
            int[] result = new int[bin1.Length]; //instancio o vetor resultado do XOR logico com o mesmo tamanho de bin1

            for (int i = bin1.Length - 1; i >= 0; i--)
            {
                //percorro do fim pro inicio os vetores fazendo o teste logico bit por bit
                result[i] = bin1[i] ^ bin2[i];
                //armazeno no vetor result o resultado do XOR entre Bin1 e Bin2
            }
            return result;
        }

        private static int testeAnd(int[] and)
        {
            //Funcao que recebe um vetor e soma todos os seus valores e retorna esse valor
            //usada para verificar se um AND ainda esta retornando algum Vai 1
            int a = 0;
            for (int i = 0; i < and.Length; i++)
            {
                a = a + and[i]; //percorro todos os indices e somo eles em a 
            }
            return a;
        }

        private static int[] desloca(int[] bin)
        {
            //funcao para deslocar um vetor de binarios da direita para a esquerda em uma casa apenas
            //para facilitar alguns calculos (ficar varias vezes tendo que acertar o tamanho do vetor)
            //verifica -se duas situacoes, uma quando o primeiro valor do vetor e 1 e outra quando e 0
            //a logia e que quando temos 1 na primeira casa, incondicionalmente temos que adicionar mais uma casa no vetor
            //para efetuar o perfeito deslocamento, e por fim em outra funcao temos que igualar o tamanho dos vetores para uma soma
            //se tivermos 0 na primeira casa, nao influenciara tanto, portanto nao e preciso alterar o tamanho do vetor

            if (bin[0] == 1)
            {
                int[] result = new int[bin.Length + 1]; //instancio resultado com o tamanho do bin mais 1
                //agora copio o vetor bin para dentro de result, sem efetuar o deslocamento
                result[0] = 0;//coloco como valor 0 na primeira casa do vetor
                for (int i = 0; i < bin.Length - 1; i++)
                {
                    //loop para percorrer todo o vetor bin e o result para copiar
                    result[i + 1] = bin[i];
                }
                //loop para deslocar o vetor uma casa da direita para a esquerda
                for (int i = 0; i < result.Length - 1; i++)
                {
                    //copio a casa a frente para a anterior descartando-se a primeira casa
                    result[i] = result[i + 1];
                }
                //incondicionalmente a ultima casa do vetor sempre sera 0
                result[result.Length - 1] = 0;
                return result;
            }
            else
            {
                //caso o primeiro valor do vetor nao seja 1 e sim 0
                int[] result = new int[bin.Length]; //instancio do mesmo tamanho que Bin
                //efetuar o deslocamento
                for (int i = 0; i < result.Length - 1; i++)
                {
                    //ignoro a primeira casa
                    result[i] = bin[i + 1];
                }
                //incondicionalmente a ultima casa do vetor sempre sera 0
                result[result.Length - 1] = 0;
                return result;
            }
        }

        private static int[] soma(int [] xor, int [] and)
        {
            int[] xorTemp; //crio vetor temporario que ira guardar o valor inicial do vetor XOR, ou seja o Q (multiplicador)

            while(testeAnd(and) != 0)
            {
                //esse loop sera executado enquanto existir alores diferentes de 0 no vetor and. Isso e verficado pea funcao testeAnd
                if(xor.Length > and.Length)
                {
                    //quando o vetor xor foi maior que o and ele vai enviar o and pra funcao igualadorCasasDec para deixar do mesmo tamanho
                    and = igualadorCasasDec(and, xor.Length);
                } else if (xor.Length < and.Length)
                {
                    //quando o vetor and foi maior que o xor ele vai enviar o xor pra funcao igualadorCasasDec para deixar do mesmo tamanho
                    xor = igualadorCasasDec(xor, and.Length);
                }

                xorTemp = xor; //salvo o valor original do xor antes das modificacoes antes de cada loop

                //inicio a operacao de soma
                xor = Xor(xor, and);// passo o multiplicando e o multiplicador para a funcao xor
                and = And(xorTemp, and);// pego o valor original do xor e faco  and para verificar os vai 1
                and = desloca(and); //desloco o and da direita para esquerda

            }
            return xor;
        }

        private static int[] igualadorCasasDec(int [] bin, int tamanho)
        {
            return null;
        }

        private static int[] complementa2()
        {
            return null;
        }
        static void Main(string[] args)
        {
        }
    }
}
