# openapi-csharp-builder
Open API Builder patterns for the Microsoft.OpenApi.NET JSON for use in Swagger

This is an early alpha to help with some building for another project. Please feel free to fork from it to extend it if you can. If you have any requests raise an Issue and I will get around to adding the functionality to the next version.

## Installation

Install from [Nuget](https://www.nuget.org/packages/OpenApiBuilder/)

`dotnet add package OpenApiBuilder`

## Usage

```
using Swashbuckle.AspNetCore.SwaggerGen;
using OpenApiBuilder;

public class SwaggerData : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        new PathBuilder($"/api/someCommand", swaggerDoc)
            .AddOperation(OperationType.Post, post =>
            {
                post
                    .AddTag("Commands")
                    .AddBodyParameter(command.Value, par =>
                    {
                        par.IsRequired();
                    })
                    .AddResponse<Result>(HttpStatusCode.OK);
            })
            .Build();
    }
}
```
