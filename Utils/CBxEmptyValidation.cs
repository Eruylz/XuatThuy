using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace XuatThuy.Utils
{
    class CBxEmptyValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //int age = 0;

            //try
            //{
            //    if (((string)value).Length > 0)
            //        age = Int32.Parse((String)value);
            //}
            //catch (Exception e)
            //{
            //    return new ValidationResult(false, "Illegal characters or " + e.Message);
            //}

            //if ((age < 1) || (age > 11))
            //{
            //    return new ValidationResult(false,
            //      "Please enter an age in the range: " + 1 + " - " + 11 + ".");
            //}
            //else
            //{
            //    return new ValidationResult(true, null);
            //}
            if (value is ComboBoxItem)

                return new ValidationResult(false, "Selection is invalid.");

            return new ValidationResult(true, null);
        }
    }
}
