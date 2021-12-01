using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task_01.Validation
{
    public class FutureDateValidation:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var dt = DateTime.Parse(value.ToString());
            return dt.Date <= DateTime.Now.Date;
        }
    }
}
