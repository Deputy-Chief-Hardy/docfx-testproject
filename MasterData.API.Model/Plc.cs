namespace KnorrBremse.Insight.MasterData.API.Model;

/// <summary>
/// PLC = Programmable Logic Controller, in German Speicherprogrammierbare Steuerung (SPS)
/// </summary>
public class Plc
{
	/// <summary>
	/// Internal Id
	/// </summary>
	public int Id { get; set; }
	/// <summary>
	/// Name of the PLC
	/// </summary>
	public string Name { get; set; } = string.Empty;
	/// <summary>
	/// IP Address
	/// </summary>
	public string IPAddress { get; set; } = string.Empty;
	/// <summary>
	/// Manufacturer
	/// </summary>
	public string Manufacturer { get; set; } = string.Empty;
	/// <summary>
	/// Type name
	/// </summary>
	public string TypeName { get; set; } = string.Empty;

	/// <summary>
	/// Connector type
	/// </summary>
	[Obsolete("Check whether we need this property.")]
	public string ConnectorType { get; set; } = string.Empty;

}
