using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GithubPullRequestReviewer.PullRequestAPI.Authorization
{
    public class AccessTokenHeaderParametr : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "access_token",
                In = ParameterLocation.Header,
                Description = "GitHub User Access Token",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
        }
    }
}
