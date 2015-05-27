using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* http://edx.prometheus.org.ua/courses/KPI/Algorithms101/2015_Spring/ */

using System.Numerics; //task 1
using System.IO;
using System.Threading; 

namespace _project
{
    class Program
    {
        static void Main(string[] args)
        {
            Task9 t = new Task9("..\\..\\tests\\Task9\\input_1_100.txt");
            int path = t.dijkstra(1, 3);
            
            Console.ReadKey();
        }
    }
}
