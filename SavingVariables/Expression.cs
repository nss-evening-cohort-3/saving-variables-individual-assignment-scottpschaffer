using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SavingVariables
{
    public class Expression
    {
        string lastCommand = "";

        public string[] Extract(string expr)
        {
            // Write expression to DB now

            expr = expr.ToLower();

            string[] expr1 = { "error", "error" };

            // If just letter, then print value
            Regex r1 = new Regex(@"\s*(?<variable>^[a-z]$)\s*");

            // If "letter = number", then variable assigned value
            Regex r2 = new Regex(@"\s*(?<variable>[a-z])\s*=\s*(?<value>[-]?\d+)\s*");

            // If text is one word command
            Regex r3 = new Regex(@"\s*(^(?<command>[a-z]+)$)\s*");

            // If Text is two words separated by a space
            Regex r4 = new Regex(@"\s*(^(?<command>[a-z]+)\s?(?<value>[a-z]+)$)\s*");

            Match m1 = r1.Match(expr);
            Match m2 = r2.Match(expr);
            Match m3 = r3.Match(expr);
            Match m4 = r4.Match(expr);

            if (m1.Success)
            {
                GroupCollection g1 = m1.Groups;
                expr1[0] = g1["variable"].Value;
            }
            else if (m2.Success)
            {
                GroupCollection g2 = m2.Groups;
                expr1[0] = g2["variable"].Value;
                expr1[1] = g2["value"].Value;
            }
            else if (m3.Success)
            {
                GroupCollection g3 = m3.Groups;
                expr1[0] = g3["command"].Value;
            }
            else if (m4.Success)
            {
                GroupCollection g4 = m4.Groups;
                expr1[0] = g4["command"].Value;
                expr1[1] = g4["value"].Value;
            }

            return expr1;
        }

        public string Process(string[] input, string origInput)
        {
            string output = "";
            switch(input[0])
            {
                case "lastq":
                    output = lastCommand;
                    break;
                case "quit":
                case "exit":
                    output = input[0];
                    break;
                case "clear":
                case "remove":
                case "delete":
                    // check if input[1] is letter or "all"
                    // if letter then remove letter and value from DB
                    // if "all" then delete all entries from DB
                    break;
                case "show":
                    // If input[1] is "all" then go through DB and print out letter and value
                    break;
                case "help":
                    // iterate through list of Commands and their definitions
                    output = "I'll help you";
                    break;
                default:
                    if (input[0].Length == 1)
                    {
                        // check if input[0] is in DB already
                        if (input[1] != "error")
                        {
                            int num1 = 0;
                            bool isNum = int.TryParse(input[1], out num1);
                            if (isNum)
                            {
                                // Check if input[0] is in DB already
                                // If not, then add to DB. If yes, then return error message
                            }
                        }
                        else
                        {
                            // return value of variable (input[0]) 
                        }
                    }

                    break;
            }
            lastCommand = origInput;
            return output;
        }
    }
}
