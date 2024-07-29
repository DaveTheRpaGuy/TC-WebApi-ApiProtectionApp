using AspNetCoreRateLimit;
using ApiProtection.StartupConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCaching();

builder.Services.AddMemoryCache(); // this will happen per server so round robin could cause the limit to be exceeded
builder.AddRateLimitServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.UseIpRateLimiting();

app.Run();
