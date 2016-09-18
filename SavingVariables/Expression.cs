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
        public string[] Extract(string expr)
        {
            // Write expression to DB now

            expr = expr.ToLower();

            string[] expr1 = { "error", "error", "error" };

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
                expr1[1] = "=";
                expr1[2] = g2["value"].Value;
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
    }
}
