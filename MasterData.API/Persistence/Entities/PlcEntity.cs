namespace KnorrBremse.Insight.MasterData.API.Persistence.Entities;

/// <summary>
/// This class maps to DB table Plc.
/// </summary>
internal class PlcEntity
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string IPAddress { get; set; } = string.Empty;
	public string Manufacturer { get; set; } = string.Empty;
	public string TypeName { get; set; } = string.Empty;

	[Obsolete("Check whether we need this property.")]
	public string ConnectorType { get; set; } = string.Empty;

	#region Navigation properties

	public List<StationEntity> Stations { get; set; } = [];

	#endregion
}
