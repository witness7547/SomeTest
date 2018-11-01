/*
Computer Science
Yield in programming context simply means "brings". It is usually used when some kind of execution context surrenders control
flow to a different execution context. In some cases, upon surrendering control, the first execution context may 
send (i.e., yield) a data value to the second execution context. See Generator_(computer_science).
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> results = Power(2, 8);

            foreach (var item in Power(2, 8))
            {
                Console.WriteLine(item);
            }
        }

        public static IEnumerable<int> Power(int number, int exponent)
        {
            int result = 1;

            for (int i = 0; i < exponent; i++)
            {
                result *= number;
                yield return result;
            }
        }
    }
}
