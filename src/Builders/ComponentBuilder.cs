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
                Type = TypeIdentifier.Object
            };

            foreach (var property in properties)
            {
                string name = NameToCamelCase(property);

                if (IsRequired(property))
                    schema.Required.Add(name);

                var (propType, format) = TypeIdentifier.Id(property.PropertyType);

                if (propType == TypeIdentifier.Object)
                    BuildObjectSchema(schema, property, name);
                else if (propType == TypeIdentifier.Array)
                    BuildArraySchema(schema, property, name);
                else
                    BuildPropertySchema(schema, name, propType, format);
            }

            return (TypeIdentifier.Name(type), schema);
        }

        private void BuildPropertySchema(OpenApiSchema schema, string name, string propType, string format)
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

        private void BuildArraySchema(OpenApiSchema schema, PropertyInfo property, string name)
        {
            var itemType = property.PropertyType
                                    .GetGenericArguments()
                                    .First();

            var (key, itemSchema) = ReadType(itemType);
            _components.Schemas.Add(key, itemSchema);

            schema.Properties.Add(name, new OpenApiSchema()
            {
                Type = TypeIdentifier.Array,
                Items = new OpenApiSchema()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = key
                    }
                }
            });
        }

        private void BuildObjectSchema(OpenApiSchema schema, PropertyInfo property, string name)
        {
            var (key, propertySchema) = ReadType(property.PropertyType);
            _components.Schemas.Add(key, propertySchema);
            schema.Properties.Add(name, new OpenApiSchema()
            {
                Type = TypeIdentifier.Object,
                Reference = new OpenApiReference()
                {
                    Id = key
                }
            });
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