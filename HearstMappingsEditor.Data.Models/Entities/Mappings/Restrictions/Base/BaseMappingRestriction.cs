using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HearstMappingsEditor.Data.Models
{
    [NotMapped]
    public class BaseMappingRestriction
    {
        public override string ToString()
        {
            var type = this.GetType();
            var propertyNames = type
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(RestrictionValueAttribute)))
                .Select(p => new
                {
                    Property = p,
                    Attribute = (RestrictionValueAttribute)Attribute.GetCustomAttribute(p, typeof(RestrictionValueAttribute), true)
                })
                .ToDictionary(p => p.Property.Name, p => p.Attribute.IsSetFlagName);

            var parts = new List<string>();
            foreach (var name in propertyNames.Keys)
            {
                if ((bool?)type.GetProperty(propertyNames[name]).GetValue(this) ?? false)
                {
                    var valueProperty = type.GetProperty(name);
                    var value = valueProperty.GetValue(this);
                    parts.Add($"{name} = {value ?? "null"}");
                }
            }

            return string.Join(", ", parts);
        }
    }
}
