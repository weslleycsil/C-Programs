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
            int[] result = new int[tamanho];//recebo o tamanho que o vetor tem que ter e aloco um novo vetor com esse tamanho
            int a = bin.Length - 1;//recebo o tamanho do vetor atual

            for (int i = tamanho - 1; i >= 0; i--, a--)
            {
                if (i > tamanho - bin.Length) // permaneco aqui enquanto meu i nao for maior que o tamanho do vetor bin
                {
                    result[i] = bin[a];
                }
                else //contador i maior que o vetor bin insiro 0 se o numero for positivo ou 1 se for negativo representado em C2
                {
                    if (bin[0] == 1) { result[i] = 1; } else if (bin[0] == 0) { result[i] = 0; }
                }
            }
            return result;
        }

        private static int[] complementa2(int [] n)
        {
            int[] complemento = new int[n.Length]; //vetor a ser populado com o valor 1 para ser somado ao vetor n
            //loop para popular o vetor complemento
            for (int i = 0; i < complemento.Length; i++)
            {
                if(i == complemento.Length - 1) { complemento[i] = 1; } else { complemento[i] = 0; } // populo o vetor com 0 ate a penultima posicao do vetor e a na ultima coloco 1 #0001
            }
            //crio um vetor auxiliar que ira receber o vetor n invertido para a soma
            int[] aux = new int[n.Length];
            //loop para inverter o vetor n e colocar em aux
            for(int i = 0; i < n.Length; i++)
            {
                if(n[i] == 0) { aux[i] = 1; } else { aux[i] = 0; } //verifico se o valor da posicao e 0, se sim ele inverte para 1, se nao, inverte para 0
            }
            aux = soma(aux, complemento);//faco a soma
            return aux;
        }

        static void Main(string[] args)
        {

            int[] a;
            int[] aTemp;
            int[] q;
            int q1 = 0;
            int[] m;
            int[] mC2;


        }
    }
}
