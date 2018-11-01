using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTest
{
    public class Consumer1
    {
        public void OnNewCar(object sender, EventArgs e)
        {
            if (e is MyArgs)
            {
                Console.WriteLine("Consumer1: " + (sender as Dealer).dealerName + " is selling new car " + (e as MyArgs).Name);
                Console.WriteLine("");
            }
        }
    }
}
