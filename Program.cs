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

// 1. READ ALL ROUTE
app.MapGet("/api/catalog", () => MediaData.Catalog)
   .WithSummary("Get Complete Media Catalog")
   .WithDescription("Returns a list of all movies and books currently saved in your database system portfolio.");

// 2. CREATE NEW ROUTE
app.MapPost("/api/catalog", (MediaItem newItem) => {
    newItem.Id = MediaData.Catalog.Count + 1;
    MediaData.Catalog.Add(newItem);
    return Results.Created($"/api/catalog/{newItem.Id}", newItem);
})
.WithSummary("Add New Media Item")
.WithDescription("Accepts a new movie or book item object and permanently saves it into your running database portfolio list.");

// 3. FILTER SEARCH ROUTE
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

// 4. PURGE DELETION ROUTE
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

// 5. METADATA UPDATE ROUTE
app.MapPut("/api/catalog/{id:int}", (int id, MediaItem updatedItem) =>
{
    var existingItem = MediaData.Catalog.FirstOrDefault(item => item.Id == id);
    if (existingItem == null)
    {
        return Results.NotFound($"Item with ID {id} was not found in your catalog database.");
    }

    existingItem.Title = updatedItem.Title;
    existingItem.Creator = updatedItem.Creator;
    existingItem.ReleaseYear = updatedItem.ReleaseYear;
    existingItem.Rating = updatedItem.Rating;
    existingItem.Genre = updatedItem.Genre;

    return Results.Ok(existingItem);
});

// LAUNCH ENVIRONMENT
app.Run();

// VIRTUAL PORTFOLIO DATABASE STORAGE LIST
public static class MediaData
{
    public static List<MediaItem> Catalog = new List<MediaItem>
    {
        new MediaItem { Id = 1, Title = "Inception", Creator = "Christopher Nolan", ReleaseYear = 2010, Rating = 5, Genre = "Sci-Fi" }
    };
}

// CORE DATA STRUCTURE BLUEPRINT MODEL WITH AUTOMATED DOCS METADATA
public class MediaItem
{
    /// <summary>The unique sequential identifier tracking primary ID key value.</summary>
    /// <example>1</example>
    public int Id { get; set; }

    /// <summary>The name designation title of the movie or book item asset.</summary>
    /// <example>Inception</example>
    public string Title { get; set; } = string.Empty;

    /// <summary>The specific director or author creator handle name.</summary>
    /// <example>Christopher Nolan</example>
    public string Creator { get; set; } = string.Empty;

    /// <summary>The literal 4-digit calendar year of public release.</summary>
    /// <example>2010</example>
    public int ReleaseYear { get; set; }

    /// <summary>A metric rating evaluation value scaled from 1 to 5 stars.</summary>
    /// <example>5</example>
    public int Rating { get; set; }

    /// <summary>The dynamic categorical tag classification parameter of the item.</summary>
    /// <example>Sci-Fi</example>
    public string Genre { get; set; } = string.Empty;
}
