using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowTest
{
    public delegate void Foo(int i);//全局委托

    class Program
    {
        static void Main(string[] args)
        {
            string[] str = { "111111111", "21111111", "3444444444" };
            Main(str);
        }
    }
}
