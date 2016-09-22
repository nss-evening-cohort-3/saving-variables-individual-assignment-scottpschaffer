using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SavingVariables.Models;

namespace SavingVariables.DAL
{
    public class VarRepository
    {
        public VarContext Context { get; set; }

        public VarRepository()
        {
            Context = new VarContext();
        }

        public VarRepository( VarContext _context)
        {
            Context = _context;
        }

        public List<SaveVars> GetVars()
        {
            return Context.Vars.ToList();
        }

        public void AddVars(SaveVars vars)
        {
            Context.Vars.Add(vars);
            Context.SaveChanges();
        }

        public void AddVars (int varId, string varName, int value)
        {
            SaveVars vars = new SaveVars { VarId = varId, VarName = varName, Value = value };
            Context.Vars.Add(vars);
            Context.SaveChanges();
        }

        public SaveVars FindVarByVarName(string varName)
        {

            SaveVars foundVars = Context.Vars.FirstOrDefault(a => a.VarName.ToLower() == varName.ToLower());
            return foundVars;
        }

        public SaveVars RemoveVar(string varName)
        {
            SaveVars foundVar = FindVarByVarName(varName);
            if (foundVar != null)
            {
                Context.Vars.Remove(foundVar);
                Context.SaveChanges();
            }
            return foundVar;
        }
    }
}
