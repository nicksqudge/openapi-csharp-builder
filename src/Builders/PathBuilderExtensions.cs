using System;

namespace OpenApiBuilder
{
    public static class PathBuilderExtensions
    {
        public static PathBuilder AddGetOperation(this PathBuilder pathBuilder, Action<OperationBuilder> opBuilderAction)
        {
            var opBuilder = new OperationBuilder();
            opBuilderAction.Invoke(opBuilder);

            pathBuilder.AddOperation(Microsoft.OpenApi.Models.OperationType.Get, opBuilder);
            return pathBuilder;
        }
    }
}
