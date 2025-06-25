var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();
// create a new get endpoint that gives the weather forecast for a given city   
app.MapGet("/weather/{city}", (string city) =>
{
    // Simulate a weather forecast response
    var forecasts = new[]
    {
        new { City = city, TemperatureC = 20, Summary = "Sunny" },
        new { City = city, TemperatureC = 15, Summary = "Cloudy" },
        new { City = city, TemperatureC = 10, Summary = "Rainy" }
    };
    
    return Results.Ok(forecasts);
});
// New endpoint to predict if it will rain
app.MapGet("/will-it-rain/{city}", (string city) =>
{
    // Simulate a prediction response
    var willRain = new Random().Next(0, 2) == 1; // Randomly predict rain
    return Results.Ok(new { City = city, WillItRain = willRain });
});
app.Run();