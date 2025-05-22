using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlavorTalk.Shared.GenericControllersStuff;
public class GenericControllerDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // Identify all generic controller types from the assembly
        var assembly = Assembly.GetExecutingAssembly();
        var endpointTypes = assembly.GetTypes()
            .Where(t => t.GetCustomAttributes<EndpointAttribute>(false).Any())
            .ToList();

        foreach (var endpointType in endpointTypes)
        {
            var attribute = endpointType.GetCustomAttribute<EndpointAttribute>();
            if (attribute == null) continue;

            var commandType = endpointType.GetNestedType("Command");
            var responseType = endpointType.GetNestedType("Response");

            if (commandType == null || responseType == null) continue;

            // Get or create the path item for this endpoint
            string route = attribute.Route;
            if (!route.StartsWith("/"))
                route = "/" + route;

            if (!swaggerDoc.Paths.ContainsKey(route))
                swaggerDoc.Paths.Add(route, new OpenApiPathItem());

            var pathItem = swaggerDoc.Paths[route];

            // Create the operation (POST, PUT, etc.)
            var operation = new OpenApiOperation
            {
                OperationId = endpointType.Name,
                Tags = new List<OpenApiTag> { new OpenApiTag { Name = GetControllerNameFromRoute(route) } },
                RequestBody = CreateRequestBody(commandType, context),
                Responses = CreateResponses(responseType, context)
            };

            // Add security requirement if authorization is required
            if (attribute.RequiresAuthorization)
            {
                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                        }
                    }
                };
            }

            // Add the operation to the path item
            switch (attribute.HttpMethod.ToString().ToUpper())
            {
                case "GET":
                    pathItem.Operations[OperationType.Get] = operation;
                    break;
                case "POST":
                    pathItem.Operations[OperationType.Post] = operation;
                    break;
                case "PUT":
                    pathItem.Operations[OperationType.Put] = operation;
                    break;
                case "DELETE":
                    pathItem.Operations[OperationType.Delete] = operation;
                    break;
                case "PATCH":
                    pathItem.Operations[OperationType.Patch] = operation;
                    break;
            }
        }
    }

    private string GetControllerNameFromRoute(string route)
    {
        // Extract controller name from route, e.g., "/api/plates/update" -> "Plates"
        var segments = route.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length > 1)
        {
            // Use the second segment as the controller name (assuming first is usually "api")
            return char.ToUpper(segments[1][0]) + segments[1].Substring(1);
        }
        return "API";
    }

    private OpenApiRequestBody CreateRequestBody(Type commandType, DocumentFilterContext context)
    {
        var schema = context.SchemaGenerator.GenerateSchema(commandType, context.SchemaRepository);

        return new OpenApiRequestBody
        {
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = schema
                }
            },
            Required = true
        };
    }

    private OpenApiResponses CreateResponses(Type responseType, DocumentFilterContext context)
    {
        var schema = context.SchemaGenerator.GenerateSchema(responseType, context.SchemaRepository);

        return new OpenApiResponses
        {
            ["200"] = new OpenApiResponse
            {
                Description = "Success",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = schema
                    }
                }
            },
            ["400"] = new OpenApiResponse
            {
                Description = "Bad Request",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema { Type = "object" } // Generic error schema
                    }
                }
            },
            ["401"] = new OpenApiResponse
            {
                Description = "Unauthorized"
            }
        };
    }
}
