using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBInterface;

namespace MysqlDb
{
    public class DBHelper : IDBHelper
    {
        public string GetDBHelper()
        {
            Console.WriteLine("mysql");
            return "";
        }
    }
}
