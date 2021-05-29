using System;
using System.Linq;
using FluentAssertions;
using Microsoft.OpenApi.Models;
using OpenApiBuilder.Tests.TestUtilities;
using OpenApiBuilder.Tests.TestUtilities.ExampleClasses;
using Xunit;

namespace OpenApiBuilder.Tests
{
    public class ResponseBuilderTests
    {
        [Fact]
        public void CheckNormalReferenceId()
        {
            var builder = new ResponseBuilder(new OpenApiComponents());
            builder.AddJsonContent<SinglePropertyClass>();

            var result = builder.Build();
            result.Content.Should().HaveKey("application/json");
            result.Content["application/json"].Schema.Reference.Id.Should().Be("SinglePropertyClass");
        }

        [Fact]
        public void CheckGenericReferenceId()
        {
            var builder = new ResponseBuilder(new OpenApiComponents());
            builder.AddJsonContent<Result<ListProjectsViewResult>>();

            var result = builder.Build();
            result.Content["application/json"].Schema.Reference.Id.Should().Be("ListProjectsViewResultResult");
        }
    }  
}