using System;
using System.Linq;
using Builders;
using BuilderTests.TestUtilities;
using BuilderTests.TestUtilities.ExampleClasses;
using FluentAssertions;
using Microsoft.OpenApi.Models;
using Xunit;

namespace BuilderTests
{
    public class ParameterBuildTests
    {
        [Fact]
        public void BuildSinglePropertyClass()
        {
            var parameterBuilder = ParameterBuilder.ForType<SinglePropertyClass>(ParameterLocation.Query);

            var results = parameterBuilder.Build();

            results.Should().HaveCount(1);
            
            var property = results.First();
            property.Name.Should().Be(nameof(SinglePropertyClass.UserId));
            property.Schema.Type.Should().Be(TypeIdentifier.String);
            property.Schema.Format.Should().BeNull();
            property.In.Should().Be(ParameterLocation.Query);
        }

        [Fact]
        public void BuildMultiplePropertyClass()
        {
            var parameterBuilder =  ParameterBuilder.ForType<MultiPropertyClass>(ParameterLocation.Query);

            var results = parameterBuilder.Build();

            results.Should().HaveCount(2);

            results[0].Name.Should().Be(nameof(MultiPropertyClass.Id));
            results[0].Schema.Type.Should().Be(TypeIdentifier.Integer);
            results[0].Schema.Format.Should().Be("int32");

            results[1].Name.Should().Be(nameof(MultiPropertyClass.Description));
            results[1].Schema.Type.Should().Be(TypeIdentifier.String);
            results[1].Schema.Format.Should().Be(null);
        }
    }
}
