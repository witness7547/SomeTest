using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTest
{
    public class Dealer
    {
        public string dealerName = "dealer1";

        public event EventHandler newCar;

        public void RaiseNewCar(string carName)
        {
            MyArgs arg = new MyArgs(carName);
            if (newCar != null)
            {
                newCar(this, arg);
            }
        }
    }

    public class MyArgs : EventArgs
    {
        public string Name { get; set; }

        public MyArgs(string name)
        {
            this.Name = name;
        }
    }
}
