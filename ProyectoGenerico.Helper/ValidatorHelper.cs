using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoGenerico.Helper
{
    public class ValidatorHelper
    {
        public List<string> errorMsg = new List<string>();
        public bool isValid { get; set; } = true;

        public void ValidateObject(object obj)
        {
            if (obj.IsNotNull())
            {
                var context = new ValidationContext(obj, null, null);
                var results = new List<ValidationResult>();

                if (!Validator.TryValidateObject(obj, context, results, true))
                {
                    isValid = false;
                    foreach (var result in results)
                    {
                        errorMsg.Add(result.ErrorMessage);
                    }
                }
            }
        }
    }
}