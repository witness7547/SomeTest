using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Dealer dealer = new Dealer();
            Consumer1 c1 = new Consumer1();
            Consumer2 c2 = new Consumer2();

            dealer.newCar += c1.OnNewCar;
            dealer.newCar += c2.OnNewCar;

            dealer.RaiseNewCar("BMW");
        }
    }
}
