using System;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace OpenApiBuilder
{
    public class ComponentBuilder
    {
        private OpenApiComponents _components;

        public ComponentBuilder(OpenApiComponents components)
        {
            _components = components;
        }

        public ComponentBuilder LoadTypeToSchema<T>()
        {
            var result = ReadType(typeof(T));
            _components.Schemas.Add(result.Key, result.Value);
            return this;
        }

        private (string Key, OpenApiSchema Value) ReadType(Type type)
        {

        }
    }  
}