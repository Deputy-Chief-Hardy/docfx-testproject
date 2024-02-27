using KnorrBremse.Insight.MasterData.API.Persistence;
using KnorrBremse.Insight.ServiceShared.Extensions;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace KnorrBremse.Insight.MasterData.API.Extensions;

internal static class HostApplicationBuilderExtensions
{
	/// <summary>
	///		Add services specific to this application to the builder.
	///		This is in contrast to <see cref="ServiceDefaultExtensions.AddServiceDefaults(IHostApplicationBuilder)"/>,
	///		which adds services for all applications.
	/// </summary>
	/// <param name="builder"></param>
	public static void AddApplicationServices(this IHostApplicationBuilder builder)
	{
		// Add database context
		var persistenceOptions = new PersistenceOptions();
		builder.Configuration.GetRequiredSection(PersistenceOptions.ConfigurationSectionName).Bind(persistenceOptions);
		builder.Services.AddDbContext<MasterDataDbContext>(c => c.UseSqlServer(persistenceOptions.ConnectionString));

		// Add Mapster services for mapping between entity classes and business classes
		builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);
		builder.Services.AddScoped<IMapper, ServiceMapper>();
	}
}
