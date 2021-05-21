using System;
using Microsoft.OpenApi.Models;

namespace Builders
{
    public class SchemaBuilder
    {
        private Type _targetType;
        private OpenApiSchema _result;

        public static SchemaBuilder ForType<T>()
            => new SchemaBuilder(typeof(T));

        public SchemaBuilder(Type targetType)
        {
            _targetType = targetType;
            _result = new OpenApiSchema();
        }

        public (string Name, OpenApiSchema Data) Build()
        {
            _result = BuildProperties(_result, _targetType);
            return (_targetType.Name, _result);
        }

        private OpenApiSchema BuildProperties(OpenApiSchema result, Type input)
        {
            var properties = input.GetProperties();

            foreach (var property in properties)
            {
                var data = TypeIdentifier.Id(property.PropertyType);

                if (data.Type == TypeIdentifier.Array)
                {
                    result.Properties.Add(
                        StringFormatter.ToCamelCase(property.Name),
                        new OpenApiSchema()
                        {
                            Type = data.Type,
                            Format = data.Format
                        }
                    );
                }
                else
                {
                    var subBuilder = new SchemaBuilder(property.PropertyType)
                        .Build();

                    result.Properties.Add(
                        StringFormatter.ToCamelCase(property.Name),
                        subBuilder.Data
                    );
                }
            }

            return result;
        }
    }
}
