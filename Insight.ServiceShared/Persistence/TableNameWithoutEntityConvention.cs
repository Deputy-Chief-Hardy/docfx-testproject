using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace KnorrBremse.Insight.ServiceShared.Persistence;

/// <summary>
///		A convention that configures the table name from the class name by removing the suffix "Entity". 
/// </summary>
/// <remarks>
///		In the Insight project, all entity classes are named "{table-name}Entity". 
///		So this convention removes "Entity", leaving just the original table name.
///		<br />
///		<example>e.g. class <c>AssemblyLineEntity</c> maps to table <c>AssemblyLine</c>.</example>
/// </remarks>
public class TableNameWithoutEntityConvention : IModelFinalizingConvention
{
	/// <inheritdoc cref="TableNameWithoutEntityConvention"/>
	public TableNameWithoutEntityConvention() {	}

	public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder,
		IConventionContext<IConventionModelBuilder> context)
	{
		foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
		{
			var tableName = entityType.GetTableName();
			if (tableName != null && tableName.EndsWith("Entity"))
			{
				entityType.SetTableName(tableName.Replace("Entity", ""));
			}
		}
	}
}
