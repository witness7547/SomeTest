using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IfElseAndSwitch
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "";
            switch (s)
            {
                case "1":
                    //Console.WriteLine("1");
                    //break;

                case "2":
                    Console.WriteLine("2");
                    break;

                default:
                    Console.WriteLine("this is default");
                    break;
            }

            if (s == "1")
            {
                Console.WriteLine("1");
            }
            else if (s == "2")
            {
                Console.WriteLine("2");
            }
            else if (s == "3")
            {
                Console.WriteLine("3");
            }
            else
            {
                Console.WriteLine("this is default!");
            }
        }
    }
}
