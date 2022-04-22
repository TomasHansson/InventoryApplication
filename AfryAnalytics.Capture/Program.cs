using AfryAnalytics.Capture.DataAccess;
using AfryAnalytics.Capture.Models;
using AfryAnalytics.Capture.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<MarketService>();
builder.Services.AddScoped<RevenueService>();
builder.Services.AddScoped<SiteService>();
builder.Services.AddScoped<WindSpeedService>();

builder.Services.AddDbContext<CapturePriceDbContext>(options => options.UseSqlite("Data Source=CapturePrice.db"));
SeedDatabase();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void SeedDatabase()
{
    var options = new DbContextOptionsBuilder<CapturePriceDbContext>();
    options.UseSqlite("Data Source=CapturePrice.db");
    var dbContext = new CapturePriceDbContext(options.Options);

    var currentSites = dbContext.WindSites.ToList();
    if (currentSites.Any())
    {
        return;
    }

    var site = new WindSite
    {
        Longitude = 10,
        Latitude = 10,
        Availability = 1,
        MaxCapacityMw = 100,
        WindSpeedAtMaxCapacity = 50
    };
    dbContext.WindSites.Add(site);

    var marketPrices = new MarketPrices
    {
        Longitude = 10,
        Latitude = 10,
        EurosPerMw = 10
    };
    dbContext.MarketPrices.Add(marketPrices);

    var windSpeeds = new WindSpeed
    {
        Longitude = 10,
        Latitude = 10,
        AverageWindSpeed = 50
    };
    dbContext.WindSpeeds.Add(windSpeeds);

    dbContext.SaveChanges();
}
