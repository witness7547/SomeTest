using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] arr = new int[2, 3];
            //foreach (var item in arr)
            //{
            //    Console.WriteLine(item);
            //}

            int[] bbb = new int[6] { 1, 2, 3, 4, 5, 6 };

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arr[i, j] = bbb[i * 3 + j];
                    Console.WriteLine(arr[i,j]);
                }
            }
        }
    }
}
