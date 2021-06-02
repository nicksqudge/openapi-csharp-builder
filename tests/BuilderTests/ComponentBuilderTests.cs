using System;
using System.Linq;
using FluentAssertions;
using Microsoft.OpenApi.Models;
using OpenApiBuilder.Tests.TestUtilities;
using OpenApiBuilder.Tests.TestUtilities.ExampleClasses;
using Xunit;

namespace OpenApiBuilder.Tests
{
    public class ComponentBuilderTests
    {
        [Fact]
        public void ListProjectsViewResultAsSchema()
        {
            var components = new OpenApiComponents();

            new ComponentBuilder(components)
                .LoadTypeToSchema<ListProjectsViewResult>();

            components.Schemas.Should().HaveKey(
                nameof(ListProjectsViewResult), 
                nameof(ProjectDto)
            );

            var listProjectSchema = components.Schemas
                .FirstOrDefault(s => s.Key == nameof(ListProjectsViewResult)).Value;
            listProjectSchema.Should().NotBeNull();
            listProjectSchema.Type.Should().Be("object");

            var properties = listProjectSchema.Properties
                .Should().HaveCount(1);

            listProjectSchema.Properties.Should().HaveKey("projects");

            var project = listProjectSchema.Properties["projects"];
            project.Type.Should().Be("array");
            project.Items.Reference.Should().NotBeNull();
            project.Items.Reference.Id.Should().Be(nameof(ProjectDto));
        }

        [Fact]
        public void CheckProjectSchema()
        {
            var components = new OpenApiComponents();

            new ComponentBuilder(components)
                .LoadTypeToSchema<ProjectDto>();

            components.Schemas.Should().HaveKey("ProjectDto");

            var projectDto = components.Schemas["ProjectDto"];
            projectDto.Type.Should().Be("object");
            projectDto.Properties.Should().HaveKey("id");
            projectDto.Properties.Should().HaveKey("name");

            var id = projectDto.Properties["id"];
            id.Type.Should().Be("string");
            
            var name = projectDto.Properties["name"];
            name.Type.Should().Be("string");
        }

        [Fact]
        public void NestedGenericType()
        {
            var components = new OpenApiComponents();

            new ComponentBuilder(components)
                .LoadTypeToSchema<Result<ListProjectsViewResult>>();

            components.Schemas.Should().HaveKeys(
                "ListProjectsViewResultResult",
                nameof(ListProjectsViewResult),
                nameof(ProjectDto)
            );

            var result = components.Schemas["ListProjectsViewResultResult"];
            result.Type.Should().Be("object");
            result.Properties.Should().HaveKeys(
                "error",
                "isSuccess",
                "isFailure",
                "value"
            );
            
            var resultValue = result.Properties["value"];
            resultValue.Type.Should().Be("object");
            resultValue.Reference.Id.Should().Be(nameof(ListProjectsViewResult));
        }
    }  
}