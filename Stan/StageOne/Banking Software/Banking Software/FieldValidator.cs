using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_Software
{
    public class FieldValidator
    {
        public bool field_validator(string x)
        {
            // Validate string input
            return !string.IsNullOrEmpty(x);
        }

        public bool field_validator(int x)
        {
            // Validate integer input
            return x > 0 && x < 100;
        }
    }

}
