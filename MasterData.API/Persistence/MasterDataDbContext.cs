using KnorrBremse.Insight.MasterData.API.Persistence.Entities;
using KnorrBremse.Insight.ServiceShared.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace KnorrBremse.Insight.MasterData.API.Persistence;

/// <summary>
/// DbContext for the MasterData tables.
/// </summary>
internal class MasterDataDbContext : DbContext
{
	public MasterDataDbContext(DbContextOptions<MasterDataDbContext> options) : base(options) { }

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
	{
		// Replace an annoying default convention with our entity / table naming convention. 
		configurationBuilder.Conventions.Remove(typeof(TableNameFromDbSetConvention));
		configurationBuilder.Conventions.Add(_ => new TableNameWithoutEntityConvention());
	}

	// --- Define a DbSet for each entity class that is mapped to a database table ---

	public DbSet<AssemblyLineEntity> AssemblyLines { get; set; }
	public DbSet<StationEntity> Stations { get; set; }
	public DbSet<PlcEntity> Plcs { get; set; }

}
