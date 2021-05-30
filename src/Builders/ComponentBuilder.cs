using System.Runtime.Intrinsics.X86;
using System.Xml.Serialization;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;

namespace OpenApiBuilder
{
    public class ComponentBuilder
    {
        private OpenApiComponents _components;

        public ComponentBuilder(OpenApiComponents components)
        {
            _components = components;
        }

        public ComponentBuilder LoadTypeToSchema(Type type)
        {
            var result = ReadType(type);
            _components.Schemas.Add(result.Key, result.Value);
            return this;
        }

        private (string Key, OpenApiSchema Value) ReadType(Type type)
        {
            var properties = type.GetProperties();
            var schema = new OpenApiSchema()
            {
                Type = "object"
            };

            foreach (var property in properties)
            {
                string name = NameToCamelCase(property);

                if (IsRequired(property))
                    schema.Required.Add(name);

                var (propType, format) = TypeIdentifier.Id(property.PropertyType);

                if (propType == TypeIdentifier.Object)
                {
                    var (key, propertySchema) = ReadType(property.PropertyType);
                    _components.Schemas.Add(key, propertySchema);
                    schema.Reference = new OpenApiReference()
                    {
                        Id = key
                    };
                }
                else if (propType == TypeIdentifier.Array)
                {

                }
                else
                {
                    
                }
            }
        }

        private bool IsRequired(PropertyInfo prop)
        {
            return Attribute.IsDefined(prop, typeof(RequiredAttribute));
        }

        private string NameToCamelCase(PropertyInfo prop)
        {
            return Char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1);
        }
    }  
}