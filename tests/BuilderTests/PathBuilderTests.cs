using System;
using Xunit;
using OpenApiBuilder;
using FluentAssertions;
using System.Linq;
using OpenApiBuilder.Tests.TestUtilities;
using OpenApiBuilder.Tests.TestUtilities.ExampleClasses;
using System.Net;
using Microsoft.OpenApi.Models;

namespace OpenApiBuilder.Tests
{
    public class PathBuilderTests
    {
        [Fact]
        public void SimpleGetRequest()
        {
            var pathBuilder = new PathBuilder("/api/temp")
                .AddGetOperation(get => {
                    get.AddTag("Temp Gets")
                        .AddParametersFromType<ListProjectsView>(ParameterLocation.Query)
                        .AddOKResponse(response => {
                            response.AddContent<Result<ListProjectsViewResult>>();
                        })
                        .AddBadRequestResponse(response => {
                            response.AddContent<Result>();
                        });
                });

            var path = pathBuilder.Build();

            pathBuilder.Key.Should().Be("/api/temp", "The url key should be correct");
            path.Operations.Should().HaveKey(OperationType.Get, "The operation key should be get");
            
            var operation = path.Operations.First().Value;
            operation.Tags.Should().Have(t => t.Name == "Temp Gets", "Should have the correct tag");

            operation.Parameters.Should().Have(p => p.Name == nameof(ListProjectsView.UserId), "Expecting there to be a property with a name");

            operation.Responses.FirstOrDefault(r => r.Key == HttpStatusCode.OK.ToString()).Should().NotBeNull("Should have an OK response code");
            operation.Responses.FirstOrDefault(r => r.Key == HttpStatusCode.BadRequest.ToString()).Should().NotBeNull("Should have a BadRequest response code");
        }
    }
}
