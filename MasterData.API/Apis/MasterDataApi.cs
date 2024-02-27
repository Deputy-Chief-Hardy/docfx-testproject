using KnorrBremse.Insight.MasterData.API.Model;
using KnorrBremse.Insight.MasterData.API.Persistence;
using KnorrBremse.Insight.MasterData.API.Persistence.Entities;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

internal static class MasterDataApi
{
	/// <summary>
	/// Maps the endpoints for the Master Data API.
	/// </summary>
    public static IEndpointRouteBuilder MapMasterDataApi(this IEndpointRouteBuilder app)
    {
        // Routes for querying assembly lines
        app.MapGet("/AssemblyLines", GetAllAssemblyLines);
        app.MapGet("/AssemblyLines/{id:int}", GetAssemblyLineById);
		app.MapGet("/AssemblyLinesCount", CountAllAssemblyLines);

		// Routes for modifying assembly lines
        app.MapPost("/AssemblyLines", CreateAssemblyLine);
		app.MapPut("/AssemblyLines/{id:int}", UpdateAssemblyLineById);
        app.MapDelete("/AssemblyLines/{id:int}", DeleteAssemblyLineById);

		// Routes for querying stations
		//app.MapGet("/AssemblyLines/{id:int}/stations", GetAssemblyLineById);
		//app.MapGet("/AssemblyLines/{id:int}/stations/{stationId:int}/plc/{id}", GetAssemblyLineById);

		//app.MapGet("/plc/forAssemblyLine/{id}", GetAssemblyLineById);
		//app.MapGet("/plc/{id}", GetAssemblyLineById);

		return app;
    }

	#region AssemblyLines

	/// <summary>
	/// Retrieve a list of all assembly lines.
	/// </summary>
	/// <param name="context">From dependency injection.</param>
	/// <param name="mapper">From dependency injection.</param>
	/// <returns>A list of all assembly lines.</returns>
	public static async Task<Ok<List<AssemblyLine>>> GetAllAssemblyLines(
		[FromServices] MasterDataDbContext context,
		[FromServices] IMapper mapper)
	{
        var entities = await context.AssemblyLines.ToListAsync();
		var result = mapper.Map<List<AssemblyLine>>(entities);
        return TypedResults.Ok(result);
	}

	/// <summary>
	/// Get information about an assembly line by its Id.
	/// </summary>
	/// <param name="context">From dependency injection.</param>
	/// <param name="mapper">From dependency injection.</param>
	/// <param name="id">Id of the assembly line.</param>
	/// <returns></returns>
	public static async Task<Results<Ok<AssemblyLine>, NotFound>> GetAssemblyLineById(
        [FromServices] MasterDataDbContext context,
		[FromServices] IMapper mapper,
        int id)
    {
        var entity = await context.AssemblyLines.SingleOrDefaultAsync(a => a.Id == id);

        if (entity == null)
        {
            return TypedResults.NotFound();
        }

		var assemblyLine = mapper.Map<AssemblyLine>(entity);
        return TypedResults.Ok(assemblyLine);
    }

	/// <summary>
	/// Updates the assembly line with given properties.
	/// </summary>
	/// <param name="context">From dependency injection.</param>
	/// <param name="id"></param>
	/// <param name="itemToUpdate"></param>
	/// <returns></returns>
	public static async Task<Results<Created, NotFound<string>>> UpdateAssemblyLineById(
        [FromServices] MasterDataDbContext context,
		int id,
		AssemblyLine itemToUpdate)
    {
		var entity = await context.AssemblyLines.SingleOrDefaultAsync(i => i.Id == id);

		if (entity == null)
		{
			return TypedResults.NotFound($"AssemblyLine with id {itemToUpdate.Id} not found.");
		}
		
		entity.SapName = itemToUpdate.SapName;
		entity.Description = itemToUpdate.Description;
		entity.IsActive = itemToUpdate.IsActive;

		await context.SaveChangesAsync();
		return TypedResults.Created($"/api/v1/MasterData/AssemblyLines/{itemToUpdate.Id}");
    }

	/// <summary>
	/// Create a new assembly line.
	/// </summary>
	/// <param name="context">From dependency injection.</param>
	/// <param name="assemblyLine"></param>
	/// <returns></returns>
	public static async Task<Created> CreateAssemblyLine(
        [FromServices] MasterDataDbContext context,
		AssemblyLine assemblyLine)
    {
		var entity = new AssemblyLineEntity()
		{
			SapName = assemblyLine.SapName,
			Description = assemblyLine.Description,
			IsActive = assemblyLine.IsActive
		};
        context.AssemblyLines.Add(entity);
        await context.SaveChangesAsync(); // Saving also generates and updates the Id property

        return TypedResults.Created($"/api/v1/MasterData/AssemblyLines/{entity.Id}");
    }

    /// <summary>
    /// Delete an assembly line. It cannot be deleted if any stations are assigned.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static async Task<Results<NoContent, NotFound>> DeleteAssemblyLineById(
        [FromServices] MasterDataDbContext context,
        int id)
    {
        AssemblyLineEntity? item = context.AssemblyLines.SingleOrDefault(x => x.Id == id);

        if (item is null)
        {
            return TypedResults.NotFound();
        }

		// TODO: check whether stations are present

        context.AssemblyLines.Remove(item);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

	/// <summary>
	/// Count the assembly lines.
	/// </summary>
	/// <param name="context">From dependency injection.</param>
	/// <returns></returns>
	public static async Task<Ok<int>> CountAllAssemblyLines(
		[FromServices] MasterDataDbContext context)
	{
		var count = await context.AssemblyLines.CountAsync();
		return TypedResults.Ok(count);
	}

	#endregion

	#region Stations



	#endregion
}
