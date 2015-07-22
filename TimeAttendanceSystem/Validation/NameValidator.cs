using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TimeAttendanceSystem.Validation
{
    public class NameValidator : ValidationRule
    {
        public override ValidationResult Validate
          (object value, System.Globalization.CultureInfo cultureInfo)
        {
            Console.WriteLine(value);
            if (value == null)
                return new ValidationResult(false, "value cannot be empty.");
            else
            {
                if (value.ToString().Length > 100)
                    return new ValidationResult
                    (false, "Name cannot be more than 100 characters long.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
