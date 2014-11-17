using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofSharing.Web.Infrastructure.ValidationAttributes
{
    public class DateTimeCombinedValidationAttribute : ValidationAttribute
    {
        public DateTimeCombinedValidationAttribute(params string[] propertyNames)
        {
            this.PropertyNames = propertyNames;
        }

        public string[] PropertyNames { get; private set; }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var properties = this.PropertyNames.Select(validationContext.ObjectType.GetProperty);
            var values = properties.Select(p => p.GetValue(validationContext.ObjectInstance, null)).OfType<DateTime>();
            DateTime valueAsDateTime;

            try
            {
                valueAsDateTime = (DateTime)value;
            }
            catch (Exception)
            {
                return new ValidationResult("Attribute not used on DateTime field!");
            }

            if (valueAsDateTime < DateTime.Now)
            {
                return new ValidationResult("Start Date cannot be in the past!");
            }
            foreach (var val in values)
            {
                if (val < valueAsDateTime)
                {
                    return new ValidationResult("End Date cannot be before start date!");
                }
            }
            return null;
        }
    }
}