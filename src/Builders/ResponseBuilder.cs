using System.Runtime.Intrinsics.X86;
using System;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace OpenApiBuilder
{
    public class ResponseBuilder : BaseBuilderWithComponents<OpenApiResponse>
    {
        public ResponseBuilder(OpenApiComponents components) : base(components)
        {
        }

        public ResponseBuilder AddJsonContent<T>()
        {
            var reference = new OpenApiReference();

            var inputType = typeof(T);

            reference.Id = TypeIdentifier.Name(inputType);

            _result.Content.Add("application/json", new OpenApiMediaType()
            {
                Schema = new OpenApiSchema()
                {
                    Reference = reference
                }
            });
            return this;
        }
    }
}
