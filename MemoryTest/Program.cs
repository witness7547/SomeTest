using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const int SIZE = 10000;
            int[,] arr = new int[SIZE, SIZE];

            while (true)
            {
                //faster
                Stopwatch sw = Stopwatch.StartNew();
                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        arr[i, j] = i * 10 + j;//arr[i, j]
                    }
                }
                Console.WriteLine(sw.ElapsedMilliseconds);


                //slower
                sw = Stopwatch.StartNew();
                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        arr[j, i] = i * 10 + j;//arr[j, i]
                    }
                }
                Console.WriteLine(sw.ElapsedMilliseconds);

                Console.WriteLine("===========================");
                Console.ReadLine(); 
            }
        }
    }
}
/*
This performance differential occurs because of the way the memory for the matrix array is organized. 
The array is stored contiguously in memory in row-major order, meaning that you can envision each of 
the rows from the matrix laid out in memory linearly one after the other. When the application accesses 
a value from the array, the relevant cache line will contain the value as well as other values directly
surrounding the one requested, which are likely to be the values immediately before or after in the 
same row. Thus, accessing the next value from the row should be significantly faster than accessing 
a value in a different row. In the slower version, we're always accessing values in different rows 
by jumping from row to row, accessing the same column location in each row. 
*/


//http://supercomputingblog.com/optimization/taking-advantage-of-cache-coherence-in-your-programs/