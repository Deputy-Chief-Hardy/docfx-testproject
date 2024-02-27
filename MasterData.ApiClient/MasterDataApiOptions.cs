namespace KnorrBremse.Insight.MasterData.ApiClient;

/// <summary>
/// Options for the MasterData.API, which can be mapped to a section of appsettings.json.
/// </summary>
public class MasterDataApiOptions
{
	public const string ConfigurationSectionName = "MasterDataApi";

	public string? Url { get; set; }
}
