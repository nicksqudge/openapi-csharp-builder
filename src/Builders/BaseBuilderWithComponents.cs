using System;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace OpenApiBuilder
{
    public abstract class BaseBuilderWithComponents<T> : BaseBuilder<T>
        where T: class, new()
    {
        public OpenApiComponents Components { get; private set; }

        public BaseBuilderWithComponents(OpenApiComponents components) : base()
        {
            Components = components;
        }
    }  
}