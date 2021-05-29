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
        private PathBuilder _pathBuilder { get; set; }

        public PathBuilderTests()
        {
            _pathBuilder = new PathBuilder("/api/temp", new OpenApiComponents())
                .AddGetOperation(get => {
                    get.AddTag("Temp Gets")
                        .AddParametersFromType<ListProjectsView>(ParameterLocation.Query)
                        .AddOKResponse(response => {
                            response.AddJsonContent<Result<ListProjectsViewResult>>();
                        })
                        .AddBadRequestResponse(response => {
                            response.AddJsonContent<Result>();
                        });
                });   
        }

        [Fact]
        public void SimpleGetRequest()
        {
            var path = _pathBuilder.Build();

            _pathBuilder.Key.Should().Be("/api/temp", "The url key should be correct");
            path.Operations.Should().HaveKey(OperationType.Get, "The operation key should be get");
            
            var operation = path.Operations.First().Value;
            operation.Tags.Should().Have(t => t.Name == "Temp Gets", "Should have the correct tag");

            operation.Parameters.Should().Have(p => p.Name == nameof(ListProjectsView.UserId), "Expecting there to be a property with a name");

            operation.Responses.FirstOrDefault(r => r.Key == HttpStatusCode.OK.ToString()).Should().NotBeNull("Should have an OK response code");
            operation.Responses.FirstOrDefault(r => r.Key == HttpStatusCode.BadRequest.ToString()).Should().NotBeNull("Should have a BadRequest response code");
        }

        [Fact]
        public void SimpleGetRequest_CheckResponseModel()
        {
            var path = _pathBuilder.Build();
            var get = path.Operations[OperationType.Get];

            get.Should().NotBeNull();
            
            var okResponse = get.Responses[HttpStatusCode.OK.ToString()];
            okResponse.Content.Should().NotBeNull();
            okResponse.Content.Should().HaveKey("application/json");

            var json = okResponse.Content["application/json"];
            json.Schema.Reference.Should().NotBeNull();
            
            var reference = json.Schema.Reference;
            reference.IsLocal.Should().BeTrue();
            reference.Id.Should().Be("ListProjectsViewResultResult");
        }
    }
}
