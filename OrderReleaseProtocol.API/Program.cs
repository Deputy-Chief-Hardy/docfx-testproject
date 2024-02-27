using KnorrBremse.Insight.ServiceShared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddServiceDefaults();
builder.AddDefaultOpenApi();

// TODO
//builder.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDefaultOpenApi();
}

app.MapDefaultEndpoints();

// TODO
//app.MapGroup("/api/v1/dafp")
//	.WithTags("DAFP API")
//	.MapDafpApi();

app.Run();
