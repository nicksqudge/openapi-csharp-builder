using System;
using Microsoft.OpenApi.Models;

namespace OpenApiBuilder
{
    public class PathBuilder : BaseBuilder<OpenApiPathItem>
    {
        public string Key { get; private set; }

        public PathBuilder(string key)
        {
            Key = key;
        }

        public PathBuilder AddOperation(OperationType operationType, OperationBuilder opBuilder)
        {
            _result.Operations.Add(operationType, opBuilder.Build());
            return this;
        }
    }
}
