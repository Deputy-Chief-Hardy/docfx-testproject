using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace KnorrBremse.Insight.MasterData.ApiClient.Extensions;
public static class MasterDataApiExtensions
{
	/// <summary>
	/// Adds a <see cref="MasterDataApiClient"/> to the application services.
	/// </summary>
	/// <param name="builder"></param>
	/// <exception cref="ApplicationException">If appsettings.json does not contain a proper configuration for MasterDataApi.</exception>
	public static void AddMasterDataApiClient(this IHostApplicationBuilder builder)
	{
		// Add the client for MasterData.API.
		var masterDataApiOptions = new MasterDataApiOptions();
		builder.Configuration.GetRequiredSection(MasterDataApiOptions.ConfigurationSectionName).Bind(masterDataApiOptions);
		if (masterDataApiOptions?.Url == null)
		{
			throw new ApplicationException($"'{MasterDataApiOptions.ConfigurationSectionName}:{nameof(masterDataApiOptions.Url)}' was not configured in appsettings.json.");
		}

		// TODO: we have not implemented authentication yet.
		builder.Services.AddHttpClient<MasterDataClient, MasterDataClient>(client =>
		{
			client.BaseAddress = new Uri(masterDataApiOptions.Url);
		});
	}
}
