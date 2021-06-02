using System;
using System.Linq;

namespace OpenApiBuilder
{
    public static class ComponentBuilderExtensions
    {
        public static ComponentBuilder LoadTypeToSchema<T>(this ComponentBuilder builder)
        {
            return builder.LoadTypeToSchema(typeof(T));
        }
    }  
}