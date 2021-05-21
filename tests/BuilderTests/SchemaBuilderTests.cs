using System;
using Builders;
using BuilderTests.TestUtilities.ExampleClasses;
using FluentAssertions;
using Xunit;

namespace BuilderTests
{
    public class SchemaBuilderTests
    {
        [Fact]
        public void CheckSimpleResultClass()
        {
            var schema = SchemaBuilder.ForType<SinglePropertyClass>()
                .Build();

            schema.Name.Should().Be(nameof(SinglePropertyClass));
            schema.Data.Properties.Should().HaveCount(1);
        }

        [Fact]
        public void CheckMultipleResultClass()
        {
            var schema = SchemaBuilder.ForType<MultiPropertyClass>()
                .Build();

            schema.Name.Should().Be(nameof(MultiPropertyClass));
            schema.Data.Properties.Should().HaveCount(2);
        }

        [Fact]
        public void CheckNestedClass()
        {
            var schema = SchemaBuilder.ForType<NestedClass>()
                .Build();

            schema.Name.Should().Be(nameof(NestedClass));
            schema.Data.Properties.Should().HaveCount(2);

            schema.Data.Properties.ContainsKey("single").Should().BeTrue();
            schema.Data.Properties.ContainsKey("multi").Should().BeTrue();

            schema.Data.Properties["single"].Type.Should().Be(TypeIdentifier.Array);
            schema.Data.Properties["single"].Properties.ContainsKey("id").Should().BeTrue();
        }
    }
}
