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
            Expression e1 = new Expression();
            do
            {
                Console.Write(">> ");
                inp = Console.ReadLine();

                string[] result1 = e1.Extract(inp);


            } while ((inp.ToLower() != "quit") && (inp.ToLower() != "exit"));

        }
    }
}
