using Matario.Config;
using Matario.Middlewares;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Matario.Application;
using Matario.Infrastructure;
using Matario.Persistence;
using Matario.Filters;
using Matario.Application.Contracts.Services.AuthenticationServiceModule;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Import Services from other projects
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);

var rateLimitOptions = new RateLimitOptions();
builder.Services.Configure<RateLimitOptions>(builder.Configuration.GetSection(nameof(RateLimitOptions)));

builder.Configuration.GetSection(nameof(RateLimitOptions)).Bind(rateLimitOptions);
var fixedPolicy = "fixed";

// filter services
builder.Services.AddScoped<HasAuthPermission>();
builder.Services.AddScoped<IsSuperAdminFilter<IManageJwtService>>();

// Rate Limiting
builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: fixedPolicy, options =>
    {
        options.PermitLimit = rateLimitOptions.PermitLimit;
        options.Window = TimeSpan.FromSeconds(rateLimitOptions.Window);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = rateLimitOptions.QueueLimit;
    }));

var app = builder.Build();

//  Register Filters


app.UseMiddleware<ExceptionMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRateLimiter();
app.Run();

