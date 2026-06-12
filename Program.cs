using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi().AllowAnonymous();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Your Automated Reference Routes
app.MapGet("/mytest", () => "Automation test!")
   .WithSummary("My Automated Test Route");

app.MapGet("/api/catalog", () => MediaData.Catalog)
   .WithSummary("Get Complete Media Catalog")
   .WithDescription("Returns a list of all movies and books currently saved in your database system portfolio.");

app.Run();

// Your Virtual Portfolio Database Storage List
public static class MediaData
{
    public static List<MediaItem> Catalog = new List<MediaItem>
    {
        new MediaItem { Id = 1, Title = "Inception", Creator = "Christopher Nolan", ReleaseYear = 2010, Rating = 5 }
    };
}

// Your Core Data Structure Blueprint Model
public class MediaItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Creator { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int Rating { get; set; }
}