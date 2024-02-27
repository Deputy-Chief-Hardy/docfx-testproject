namespace KnorrBremse.Insight.MasterData.API.Persistence.Entities;

/// <summary>
/// This class maps to DB table Station.
/// </summary>
internal class StationEntity
{
	public int Id { get; set; }
	public string Number { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public bool IsActive { get; set; }

	#region Navigation properties

	public int AssemblyLineId { get; set; }
	public AssemblyLineEntity AssemblyLine { get; set; } = null!;

	public int PlcId { get; set; }
	public PlcEntity Plc { get; set; } = null!;

	#endregion
}
