using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Runner
{
    /// <summary>
    /// Validation rule class to perform UI validation on Limit input
    /// </summary>
    public class InputLimitValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var limitText = value as string;
            if (String.IsNullOrEmpty(limitText) && String.IsNullOrWhiteSpace(limitText)) return ValidationResult.ValidResult;
            int limit = int.MinValue;
            if (int.TryParse(limitText, out limit))
            {
                if (limit < 0)
                {
                    return new ValidationResult(false, "Please enter a positive limit.");
                }
            }
            else
            {
                return new ValidationResult(false, "Please enter a valid limit.");
            }
            return ValidationResult.ValidResult;
        }
    }
    /// <summary>
    /// Validation rule class to perform UI validation on input sequence
    /// </summary>
    public class InputSequenceValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var limitText = value as string;
            if (String.IsNullOrEmpty(limitText) && String.IsNullOrWhiteSpace(limitText)) return ValidationResult.ValidResult;
            if (!String.IsNullOrEmpty(limitText) &&
                Regex.IsMatch(limitText, @"[a-zA-Z_ ]+$"))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Please enter valid sequence");
            }
        }

    }
}
