//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System;

namespace dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("index++");
            for (int index = 0; index++ < 10; )
            {
                index += 2;
                int a = index+1;
                Console.WriteLine(a);
            }
            Console.WriteLine("");
            Console.WriteLine("++index");
            for (int counter = 0; ++counter < 10;)
            {
                counter += 2;
                int b = counter + 1;
                Console.WriteLine(b);
            }

            Console.ReadKey(false);
        }
    }
}
