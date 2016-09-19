using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            string inp = "";
            string result2 = "";
            Expression e1 = new Expression();
            do
            {
                Console.Write(">> ");
                inp = Console.ReadLine();

                string[] result1 = e1.Extract(inp);
                if (result1[0] != "error")
                {
                    result2 = e1.Process(result1, inp);
                }

                Console.WriteLine(result2);

            } while ((result2 != "quit") && (result2 != "exit"));

            Console.WriteLine("Bye");
            Console.ReadLine();
        }
    }
}
