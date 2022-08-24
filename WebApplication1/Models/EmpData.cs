using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    
    public class EmpData
    {
       public int EID { get; set; }
       [BindProperty]
       [BindRequired]
       public string EName { get; set; }

        [BindProperty]
        [BindRequired]
        public double ESal { get; set; }
    }
}
