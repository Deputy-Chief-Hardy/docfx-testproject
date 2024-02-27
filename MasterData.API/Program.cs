using Microsoft.AspNetCore.Authentication.Negotiate;
using KnorrBremse.Insight.MasterData.API.Extensions;
using KnorrBremse.Insight.ServiceShared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddServiceDefaults();
builder.AddDefaultOpenApi();

builder.AddApplicationServices();

//builder.Services.AddControllers();

//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
//   .AddNegotiate();

//builder.Services.AddAuthorization(options =>
//{
//    // By default, all incoming requests will be authorized according to the default policy.
//    options.FallbackPolicy = options.DefaultPolicy;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDefaultOpenApi();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.MapDefaultEndpoints();

app.MapGroup("/api/v1/MasterData")
    .WithTags("Master Data API")
    .MapMasterDataApi();

app.Run();
