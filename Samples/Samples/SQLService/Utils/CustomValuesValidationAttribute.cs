using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLService.Utils
{
    class CustomValuesValidationAttribute : ValidationAttribute
    {
        string[] _args;

        public CustomValuesValidationAttribute(params string[] args)
        {
            _args = args;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {   string val = (string)value;
            if (_args.Contains(val, StringComparer.OrdinalIgnoreCase)){
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid Value, Gender can only be male or female");
        }


    }
}
