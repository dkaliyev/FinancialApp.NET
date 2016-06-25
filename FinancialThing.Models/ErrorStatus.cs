using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Models
{
    public class ErrorStatus:Status
    {
        public ErrorStatus()
        {
            Data = "There was some error";
            StatusCode = "0";
        }
    }
}
