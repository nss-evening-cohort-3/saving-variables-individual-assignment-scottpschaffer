using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using SavingVariables.DAL;
using SavingVariables.Models;

namespace SavingVariables
{
    public class Expression
    {
        string lastCommand = "";
        Dictionary<string, string> helpData = new Dictionary<string, string>()
        {
            {"lastq", "Lists previous command" },
            {"quit", "End program" },
            {"exit", "End program" },
            {"clear q", "Removes variable 'q' from DB" },
            {"remove q", "Removes variable 'q' from DB" },
            {"delete q", "Removes variable 'q' from DB" },
            {"clear all", "Removes all variables from DB"},
            {"remove all", "Removes all variables from DB"},
            {"delete all", "Removes all variables from DB"},
            {"show all", "Displays all variables from DB and their values"},
            {"help", "Brings up this list of commands" }
        };

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

            VarRepository repo = new VarRepository();
            List<SaveVars> getVars = repo.GetVars();
            
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
                    if (input[1].Length == 1)
                    {
                        SaveVars found = repo.FindVarByVarName(input[1]);
                        if (found != null)
                        {
                            SaveVars deleted = repo.RemoveVar(found.VarName);
                            output = "\'" + deleted.VarName + "\' is now free!";
                        }
                        else
                        {
                            output = "Error! Variable not found in DB!";
                        }
                    }
                    else if (input[1] == "all")
                    {
                        foreach (SaveVars va in getVars)
                        {
                            SaveVars removed = repo.RemoveVar(va.VarName);
                        }
                        output = " = Deleted all items from database!";
                    }
                    else
                    {
                        output = " = Error! Incorrect entry!";
                    }
                    break;
                case "show":
                    // If input[1] is "all" then go through DB and print out letter and value
                    if (getVars.Count > 0)
                    {
                        Console.WriteLine("________________");
                        Console.WriteLine("| Name | Value |");
                        Console.WriteLine("----------------");

                        foreach (SaveVars va in getVars)
                        {
                            Console.WriteLine("|  " + va.VarName + "   |    " + va.Value + "  | ");
                        }
                        Console.WriteLine("________________");
                    }
                    else
                    {
                        output = "= Database empty! Nothing to show.";
                    }

                    break;
                case "help":
                    // iterate through list of Commands and their definitions
                    foreach(var helpStuff in helpData)
                    {
                        Console.WriteLine(helpStuff.Key + " - " + helpStuff.Value);
                    }
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
                                if (repo.FindVarByVarName(input[0]) == null)
                                {
                                    repo.AddVars(getVars.Count, input[0], num1);
                                    return " = Saved \'" + input[0] + "\' as \'" + input[1] + "\'";
                                }
                                else
                                {
                                    output = " = Error! \'" + input[0] + "\' is already defined!";
                                }
                                

                            }
                            else
                            {
                                output = " = Error! Not a Number!";
                            }
                        }
                        else
                        {
                            // return value of variable (input[0]) 
                            foreach(SaveVars va in getVars)
                            {
                                if (va.VarName == input[0])
                                {
                                    return " = " + va.Value.ToString();
                                }
                            }
                            output = " = Error! Not in DB!";
                        }
                    }

                    break;
            }
            lastCommand = origInput;
            return output;
        }
    }
}
