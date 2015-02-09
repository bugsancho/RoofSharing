namespace RoofSharing.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public class DateTimeCombinedValidationAttribute : ValidationAttribute
    {
        public DateTimeCombinedValidationAttribute(params string[] propertyNames)
        {
            this.PropertyNames = propertyNames;
        }

        public string[] PropertyNames { get; private set; }
        
        //public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        //{
        //    // TODO: Implement this method
        //    return null;
        //    throw new NotImplementedException();
        //}

        protected override ValidationResult IsValid(object startingDate, ValidationContext validationContext)
        {
            var properties = this.PropertyNames.Select(validationContext.ObjectType.GetProperty);
            var values = properties.Select(p => p.GetValue(validationContext.ObjectInstance, null)).OfType<DateTime>();
            DateTime valueAsDateTime;
            try
            {
                valueAsDateTime = (DateTime)startingDate;
            }
            catch (Exception)
            {
                return new ValidationResult("Attribute not used on DateTime field!");
            }

            if (valueAsDateTime < DateTime.Now)
            {
                return new ValidationResult(string.Format("{0} cannot be in the past!", validationContext.DisplayName));
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