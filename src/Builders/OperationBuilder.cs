using System;
using System.Net;
using Microsoft.OpenApi.Models;

namespace OpenApiBuilder
{
    public class OperationBuilder : BaseBuilder<OpenApiOperation>
    {
        public OperationBuilder AddTag(string tagName)
        {
            _result.Tags.Add(new OpenApiTag()
            {
                Name = tagName
            });
            return this;
        }

        public OperationBuilder AddResponse(HttpStatusCode code, ResponseBuilder responseBuilder)
        {
            _result.Responses.Add(code.ToString(), responseBuilder.Build());
            return this;
        }

        public OperationBuilder AddParameter(ParameterBuilder parBuilder)
        {
            parBuilder.Build().ForEach(par => _result.Parameters.Add(par));
            return this;
        }
    }
}
