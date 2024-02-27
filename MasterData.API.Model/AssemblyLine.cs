namespace KnorrBremse.Insight.MasterData.API.Model;

/// <summary>
/// An assembly line which can have multiple stations.
/// </summary>
public class AssemblyLine
{
	/// <summary>
	/// Id
	/// </summary>
    public int Id { get; set; }
	/// <summary>
	/// SAP Name
	/// </summary>
	public string SapName { get; set; } = string.Empty;
	/// <summary>
	/// Description
	/// </summary>
	public string Description { get; set; } = string.Empty;
	/// <summary>
	/// Is active
	/// </summary>
	public bool IsActive { get; set; }
}
