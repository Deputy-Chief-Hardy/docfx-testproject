using Microsoft.Data.SqlClient;

namespace KnorrBremse.Insight.MasterData.API.Persistence;

internal class PersistenceOptions
{
	public const string ConfigurationSectionName = "Persistence";

	public string Server { get; set; } = null!;
	public string Instance { get; set; } = null!;
	public string Catalog { get; set; } = null!;
	public string User { get; set; } = null!;
	public string Password { get; set; } = null!;
	public string CurrentLanguage { get; set; } = null!;

	public string ConnectionString
	{
		get
		{
			var connectionStringBuildder = new SqlConnectionStringBuilder();
			connectionStringBuildder.DataSource = string.IsNullOrEmpty(Instance) ? Server : Server + "\\" + Instance;
			connectionStringBuildder.UserID = User;
			connectionStringBuildder.Password = Password;
			connectionStringBuildder.InitialCatalog = Catalog;
			connectionStringBuildder.CurrentLanguage = CurrentLanguage;

			connectionStringBuildder.IntegratedSecurity = false;
			connectionStringBuildder.TrustServerCertificate = true;

			return connectionStringBuildder.ConnectionString;
		}

	}
}
