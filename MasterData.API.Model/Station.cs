namespace KnorrBremse.Insight.MasterData.API.Model;

/// <summary>
/// A station of an assembly line.
/// </summary>
public class Station
{
	/// <summary>
	/// Internal Id
	/// </summary>
	public int Id { get; set; }
	/// <summary>
	/// Number
	/// </summary>
	public string Number { get; set; } = string.Empty;
	/// <summary>
	/// Description
	/// </summary>
	public string Description { get; set; } = string.Empty;
	/// <summary>
	/// Is active
	/// </summary>
	public bool IsActive { get; set; }

	#region Navigation properties

	/// <summary>
	/// Id of the assembly line.
	/// </summary>
	/// <remarks>TODO: is this property necessary?</remarks>
	public int AssemblyLineId { get; set; }
	//public AssemblyLine AssemblyLine { get; set; }

	#endregion
}
