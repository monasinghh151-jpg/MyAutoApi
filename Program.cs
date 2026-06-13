using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi().AllowAnonymous();
  app.MapScalarApiReference(options => {
        options.WithTheme(ScalarTheme.DeepSpace);
    });
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
// This route allows you to add a brand-new movie or book directly to your database list
app.MapPost("/api/catalog", (MediaItem newItem) => {
    newItem.Id = MediaData.Catalog.Count + 1;
    MediaData.Catalog.Add(newItem);
    return Results.Created($"/api/catalog/{newItem.Id}", newItem);
})
.WithSummary("Add New Media Item")
.WithDescription("Accepts a new movie or book item object and permanently saves it into your running database portfolio list.");
// This route allows users to filter and search your media catalog by creator/director
app.MapGet("/api/catalog/search", (string? creator) => 
{
    if (string.IsNullOrEmpty(creator))
    {
        return Results.Ok(MediaData.Catalog);
    }
    
    var filteredList = MediaData.Catalog
        .Where(item => item.Creator.Contains(creator, StringComparison.OrdinalIgnoreCase))
        .ToList();
        
    return Results.Ok(filteredList);
})
.WithSummary("Search Catalog by Creator")
.WithDescription("Filters the virtual database collection and returns only items matching the specified director or author name parameter.");
// This route allows users to remove a movie or book from the database using its unique ID number
app.MapDelete("/api/catalog/{id:int}", (int id) => 
{
    var itemToRemove = MediaData.Catalog.FirstOrDefault(item => item.Id == id);
    if (itemToRemove == null)
    {
        return Results.NotFound($"Item with ID {id} was not found in your catalog.");
    }
    
    MediaData.Catalog.Remove(itemToRemove);
    return Results.Ok($"Successfully deleted '{itemToRemove.Title}' from the database catalog portfolio.");
})
.WithSummary("Delete Media Item by ID")
.WithDescription("Scans the virtual collection array and permanently removes the matching media object record.");

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