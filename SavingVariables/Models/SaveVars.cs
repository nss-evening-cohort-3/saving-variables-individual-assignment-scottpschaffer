using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables.Models
{
    public class SaveVars
    {
        [Key]
        public int VarId { get; set; }

        [Required]
        public string VarName { get; set; }

        [Required]
        public int Value { get; set; }

    }
}
