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

            if (inputType.GenericTypeArguments.Any())
            {
                foreach (var type in inputType.GenericTypeArguments)
                    reference.Id += type.Name;

                reference.Id += inputType.Name.Replace($"`{inputType.GenericTypeArguments.Count()}", "");
            }
            else
               reference.Id = inputType.Name;

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
