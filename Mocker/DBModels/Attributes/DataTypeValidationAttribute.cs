using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModels.Attributes
{
    class DataTypeValidationAttribute: ValidationAttribute
    {
        string[] _args;
        public DataTypeValidationAttribute(params string[] args)
        {
            _args = args;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string val = (string)value;
            if (_args.Contains(val))
            {
                return ValidationResult.Success;
            }
            List<string> list = new List<string>(_args);
            return new ValidationResult($"Invalid Datatype for Entity Fields, Supported datatypes are: {string.Join(", ", list)}");
        }
    }
}
