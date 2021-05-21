using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenApiBuilder.Tests.TestUtilities.ExampleClasses
{
    public class ListProjectsView
    {
        [Required]
        public string UserId { get; set; }
    }

    public class ListProjectsViewResult
    {
        public List<ProjectDto> Projects { get; set; }
    }

    public class ProjectDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static ProjectDto New(string name)
            => new ProjectDto
            {
                Name = name
            };

        public static ProjectDto Existing(string id)
            => new ProjectDto()
            {
                Id = id
            };
    }
}
