using System;
using System.Net;
using Microsoft.OpenApi.Models;

namespace OpenApiBuilder
{
    public static class OperationBuilderExtensions
    {
        public static OperationBuilder AddParametersFromType<T>(this OperationBuilder opBuilder, ParameterLocation location)
        {
            opBuilder.AddParameter(new ParameterBuilder(typeof(T), location));
            return opBuilder;
        }

        public static OperationBuilder AddOKResponse(this OperationBuilder opBuilder, Action<ResponseBuilder> respAction)
            => opBuilder.AddResponseWithAction(HttpStatusCode.OK, respAction);

        public static OperationBuilder AddBadRequestResponse(this OperationBuilder opBuilder, Action<ResponseBuilder> respAction)
            => opBuilder.AddResponseWithAction(HttpStatusCode.BadRequest, respAction);

        private static OperationBuilder AddResponseWithAction(this OperationBuilder opBuilder, HttpStatusCode code, Action<ResponseBuilder> respAction)
        {
            var responseBuilder = new ResponseBuilder();
            respAction.Invoke(responseBuilder);

            opBuilder.AddResponse(code, responseBuilder);
            return opBuilder;
        }
    }
}
