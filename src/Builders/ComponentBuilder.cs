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
                    schema.Properties.Add(name, SchemaWithReference(key));
                }
                else if (propType == TypeIdentifier.Array)
                {
                    var itemType = property.PropertyType
                        .GetGenericArguments()
                        .First();

                    var (key, itemSchema) = ReadType(itemType);
                    _components.Schemas.Add(key, itemSchema);

                    schema.Properties.Add(name, new OpenApiSchema()
                    {
                        Type = TypeIdentifier.Array,
                        Items = SchemaWithReference(key)
                    });
                }
                else
                {
                    schema.Properties.Add(
                        name,
                        new OpenApiSchema()
                        {
                            Type = propType,
                            Format = format,
                        }
                    );
                }
            }

            return (type.Name, schema);
        }

        private OpenApiSchema SchemaWithReference(string referenceId)
        {
            return new OpenApiSchema()
            {
                Reference = new OpenApiReference()
                {
                    Id = referenceId
                }
            };
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