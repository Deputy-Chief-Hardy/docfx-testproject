using KnorrBremse.Insight.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace KnorrBremse.Insight.ServiceShared.Extensions;

public static class OpenApiExtensions
{

	/// <summary>
	/// Adds an OpenAPI service to the builder. Configuration is performed through appsettings.json.
	/// </summary>
	/// <param name="builder"></param>
	/// <returns></returns>
    public static IHostApplicationBuilder AddDefaultOpenApi(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        var openApi = configuration.GetSection("OpenApi");

        if (!openApi.Exists())
        {
            return builder;
        }

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            /// {
            ///   "OpenApi": {
            ///     "Document": {
            ///         "Title": ..
            ///         "Version": ..
            ///         "Description": ..
            ///     }
            ///   }
            /// }
            var document = openApi.GetRequiredSection("Document");

            var version = document.GetRequiredValue("Version") ?? "v1";

            options.SwaggerDoc(version, new OpenApiInfo
            {
                Title = document.GetRequiredValue("Title"),
                Version = version,
                Description = document.GetRequiredValue("Description")
            });

            // Set the comments path for the Swagger JSON and UI.
            //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlFile = document.GetRequiredValue("XmlCommentFile");
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }

            var identitySection = configuration.GetSection("Identity");

            if (!identitySection.Exists())
            {
                // No identity section, so no authentication open api definition
                return;
            }

            // {
            //   "Identity": {
            //     "Url": "http://identity",
            //     "Scopes": {
            //         "basket": "Basket API"
            //      }
            //    }
            // }

            //var identityUrlExternal = identitySection.GetRequiredValue("Url");
            //var scopes = identitySection.GetRequiredSection("Scopes").GetChildren().ToDictionary(p => p.Key, p => p.Value);

            //options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            //{
            //    Type = SecuritySchemeType.OAuth2,
            //    Flows = new OpenApiOAuthFlows()
            //    {
            //        // TODO: Change this to use Authorization Code flow with PKCE
            //        Implicit = new OpenApiOAuthFlow()
            //        {
            //            AuthorizationUrl = new Uri($"{identityUrlExternal}/connect/authorize"),
            //            TokenUrl = new Uri($"{identityUrlExternal}/connect/token"),
            //            Scopes = scopes,
            //        }
            //    }
            //});

            //options.OperationFilter<AuthorizeCheckOperationFilter>([scopes.Keys.ToArray()]);
        });

        return builder;
    }

	/// <summary>
	/// Enables the OpenAPI / Swagger endpoint. Configuration is performed through appsettings.json.
	/// </summary>
	/// <param name="app"></param>
	/// <returns></returns>
	public static IApplicationBuilder UseDefaultOpenApi(this WebApplication app)
	{
		var configuration = app.Configuration;
		var openApiSection = configuration.GetSection("OpenApi");

		if (!openApiSection.Exists())
		{
			return app;
		}

		app.UseSwagger();
		app.UseSwaggerUI(setup =>
		{
			/// {
			///   "OpenApi": {
			///     "Endpoint: {
			///         "Name": 
			///     },
			///     "Auth": {
			///         "ClientId": ..,
			///         "AppName": ..
			///     }
			///   }
			/// }

			var pathBase = configuration["PATH_BASE"];
			var authSection = openApiSection.GetSection("Auth");
			var endpointSection = openApiSection.GetRequiredSection("Endpoint");

			var swaggerUrl = endpointSection["Url"] ?? $"{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty)}/swagger/v1/swagger.json";

			setup.SwaggerEndpoint(swaggerUrl, endpointSection.GetRequiredValue("Name"));

			//if (authSection.Exists())
			//{
			//    setup.OAuthClientId(authSection.GetRequiredValue("ClientId"));
			//    setup.OAuthAppName(authSection.GetRequiredValue("AppName"));
			//}
		});

		// Add a redirect from the root of the app to the swagger endpoint
		app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

		return app;
	}
}