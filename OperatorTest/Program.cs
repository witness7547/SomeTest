using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorTest
{
    class Program
    {
        static void Main(string[] args)
        {

            MyClass m1 = new MyClass(0);
            MyClass m2 = new MyClass(1);

            //MyClass m3 = new MyClass(m1);//don't have copy constructor

            Console.WriteLine(m1 == m2);

            
        }
    }

    class MyClass
    {
        public int _id = 0;

        public MyClass(int id)
        {
            _id = id;
        }

        public static bool operator==(MyClass lhs, MyClass rhs)
        {
            return lhs._id == rhs._id;
        }

        public static bool operator !=(MyClass lhs, MyClass rhs)
        {
            return lhs._id != rhs._id;
        }
    }

    //class DerivesString : String//sealed class
    //{

    //}

    
}
