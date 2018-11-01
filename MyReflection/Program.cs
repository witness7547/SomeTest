using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
//using MysqlDb;

namespace MyReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.Load("MysqlDb");
            Console.WriteLine(assembly.FullName);
            //Assembly a = typeof(DBHelper).Assembly;
            //Console.WriteLine(a.FullName);

            Console.WriteLine();
            Console.WriteLine();
            foreach (var item in assembly.GetTypes())
            {
                Console.WriteLine(item.FullName);
            }
            Console.WriteLine(Environment.NewLine);


            Type myType = assembly.GetType("MysqlDb.DBHelper");
            foreach (MethodInfo item in myType.GetMethods())
            {
                Console.WriteLine(item.Name);
            }

            object obj = Activator.CreateInstance(myType);
            MethodInfo getDBHelper = myType.GetMethod("GetDBHelper");
            getDBHelper.Invoke(obj, null);


            
            Console.ReadLine();
        }
    }
}