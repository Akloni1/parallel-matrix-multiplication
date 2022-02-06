using System;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int size = 300;
            int[,] arr3 = new int[size, size];
            
            int[,] arr1 = new int[size, size] /*{{2, 2, 1}, {2, 2, 1}, {2, 2, 2}}*/;
            int[,] arr = new int[size, size] /*{{1, 2, 3}, {1, 2, 3}, {1, 2, 3}}*/;
            Random rnd = new Random();
            for (int n = 0; n < arr.GetLength(0); n++)
            {
                for (int m = 0; m < arr.GetLength(1); m++)
                {
                    arr[n, m] = rnd.Next(1, 9);
                    arr1[n, m] = rnd.Next(1, 9);
                }
            }

            int i = 0;
            int j = 0;
            int k = 0;
            int z = 0;
            Thread[] thread = new Thread[10];
            for (int l = 0; l < thread.Length; l++)
            {
                int copy = z;
                z += size/10;
                thread[l]= new Thread(new ParameterizedThreadStart(e=>mythread1(size,arr3,arr1,arr,i,j,k, copy)));
                thread[l].Start();
            }
            for (int l = 0; l < thread.Length; l++)
            {
                thread[l].Join();
            }
            stopwatch.Stop();
            
            int sss = 0;
            foreach (var VARIABLE in arr3)
            {
                Console.WriteLine(VARIABLE);
                sss++;
            }
            
            Console.WriteLine("время расчета матрицы:"+stopwatch.ElapsedMilliseconds+" мс");
            Console.ReadLine();
        }



        static  void mythread1(int size,  int[,] arr3, int[,] arr1, int[,] arr, int i, int j, int k, int  z )
        {
            for (int l = 0; l < size/10; l++)
            {

                while (true)
                {
                    arr3[z, k] += arr1[z, j] * arr[j, i];
                    j++;


                    if (i + j == (arr.GetLength(1) + arr.GetLength(0)) - 1)
                    {
                        z++;
                        k = 0;
                        i = 0;
                        j = 0;
                        break;
                    }


                    if (arr.GetLength(0) == j)
                    {
                        i++;
                        j = 0;
                        k++;
                    }
                }
            }
        }
    }
}