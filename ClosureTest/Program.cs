/*
In essence, a closure is a block of code which can be executed at a later time, but which maintains the environment
in which it was first created - i.e. it can still use the local variables etc of the method which created it, 
even after that method has finished executing.

The general feature of closures is implemented in C# by anonymous methods and lambda expressions.

Reference:https://stackoverflow.com/questions/428617/what-are-closures-in-net
*/

using System;

namespace ClosureTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Action a = CreateAction();
            a();

            a();
        }

        static Action CreateAction()
        {
            int counter = 0;
            return delegate
            {
                counter++;
                Console.WriteLine(counter);
            };//编译器会将闭包创建为一个新的类型
        }
    }
}

/*
How does it work?
You see, the C# compiler detects when a delegate forms a closure which is passed out of the current scope 
and it promotes the delegate, and the associated local variables into a compiler generated class. 
This way, it simply needs a bit of compiler trickery to pass around an instance of the compiler generated class, 
so each time we invoke the delegate we are actually calling the method on this class. Once we are no longer 
holding a reference to this delegate, the class can be garbage collected and it all works exactly as it is supposed to!

Reference:https://www.simplethread.com/c-closures-explained/
*/
