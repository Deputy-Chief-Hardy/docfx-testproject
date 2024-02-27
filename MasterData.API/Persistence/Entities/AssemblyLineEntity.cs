namespace KnorrBremse.Insight.MasterData.API.Persistence.Entities;

/// <summary>
/// This class maps to DB table AssemblyLine.
/// </summary>
internal class AssemblyLineEntity
{
    public int Id { get; set; }
	public string SapName { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public bool IsActive { get; set; }

    #region Navigation properties

    public List<StationEntity> Stations { get; set; } = [];

	#endregion
}
