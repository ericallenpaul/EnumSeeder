using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace EnumSeeder.API
{
    public class SwaggerHelper
    {
        public static void ConfigureSwaggerGen(SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.SwaggerDoc("v1", new Info
            {
                Title = "SSL Central",
                Version = $"v1",
                Description = "An API for centralized SSL management with Lets Encrypt."
            });

            swaggerGenOptions.AddSecurityDefinition("Bearer", new ApiKeyScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = "header",
                Type = "apiKey"
            });

            var basePath = AppContext.BaseDirectory;

            swaggerGenOptions.DescribeAllEnumsAsStrings();
            string filePath = Path.Combine(basePath, "SSLCentral.API.xml");
            swaggerGenOptions.IncludeXmlComments(filePath);
            filePath = Path.Combine(basePath, "SSLCentral.Models.xml");
            swaggerGenOptions.IncludeXmlComments(filePath);

        }

        public static void ConfigureSwagger(SwaggerOptions swaggerOptions)
        {
            swaggerOptions.RouteTemplate = "api-docs/{documentName}/swagger.json";
        }

        public static void ConfigureSwaggerUI(SwaggerUIOptions swaggerUIOptions)
        {
            swaggerUIOptions.SwaggerEndpoint($"/api-docs/v1/swagger.json", $"v1 Docs");
            swaggerUIOptions.RoutePrefix = "api-docs";
        }
    }
}
