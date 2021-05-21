using System;
using Microsoft.OpenApi.Models;

namespace OpenApiBuilder
{
    public class ResponseBuilder : BaseBuilder<OpenApiResponse>
    {
        public ResponseBuilder AddContent<T>()
        {
            return this;
        }
    }
}
