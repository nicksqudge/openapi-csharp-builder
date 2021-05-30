using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace OpenApiBuilder
{
    public class ParameterBuilder : BaseListBuilder<OpenApiParameter>
    {
        private Type _targetType;
        private ParameterLocation _location;

        public static ParameterBuilder ForType<T>(ParameterLocation location)
            => new ParameterBuilder(typeof(T), location);

        public ParameterBuilder(Type targetType, ParameterLocation location)
        {
            _targetType = targetType;
            _location = location;
        }

        public override List<OpenApiParameter> Build()
        {
            LoadProperties();

            return base.Build();
        }

        private void LoadProperties()
        {
            var properties = _targetType.GetProperties();

            foreach (var property in properties)
            {
                var apiParameter = new OpenApiParameter();

                apiParameter.In = _location;
                apiParameter.Name = property.Name;
                apiParameter.Schema = new OpenApiSchema();

                (apiParameter.Schema.Type, apiParameter.Schema.Format) = TypeIdentifier.Id(property.PropertyType);

                Add(apiParameter);
            }
        }
    }
}
