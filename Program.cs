using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Register our permanent SQLite database service file
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=catalog.db"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi().AllowAnonymous();
    app.MapScalarApiReference(options => {
        options.Theme = ScalarTheme.DeepSpace;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Your Automated Reference Routes
app.MapGet("/mytest", () => "Automation test!")
   .WithSummary("My Automated Test Route");

// 1. READ ALL ROUTE - Pulls data directly from the physical SQL database disk
app.MapGet("/api/catalog", async (AppDbContext db) => 
    await db.Catalog.ToListAsync())
   .WithSummary("Get Complete Media Catalog")
   .WithDescription("Returns a list of all movies and books permanently saved in your SQLite database file.");

// 2. CREATE NEW ROUTE - Writes and saves data directly to the physical SQL database disk
app.MapPost("/api/catalog", async (MediaItem newItem, AppDbContext db) => {
    db.Catalog.Add(newItem);
    await db.SaveChangesAsync();
    return Results.Created($"/api/catalog/{newItem.Id}", newItem);
})
.WithSummary("Add New Media Item")
.WithDescription("Accepts a new movie or book item object and permanently saves it into your hard drive's SQL database.");

// 3. FILTER SEARCH ROUTE - Searches through physical database records using parameters
app.MapGet("/api/catalog/search", async (string? creator, AppDbContext db) => 
{
    if (string.IsNullOrEmpty(creator))
    {
        return Results.Ok(await db.Catalog.ToListAsync());
    }
    
    var filteredList = await db.Catalog
        .Where(item => item.Creator.Contains(creator, StringComparison.OrdinalIgnoreCase))
        .ToListAsync();
        
    return Results.Ok(filteredList);
})
.WithSummary("Search Catalog by Creator")
.WithDescription("Filters the physical SQL database collection and returns only items matching the specified director or author name parameter.");

// 4. PURGE DELETION ROUTE - Permanently scrubs a record out of the SQL database by its ID
app.MapDelete("/api/catalog/{id:int}", async (int id, AppDbContext db) => 
{
    var itemToRemove = await db.Catalog.FirstOrDefaultAsync(item => item.Id == id);
    if (itemToRemove == null)
    {
        return Results.NotFound($"Item with ID {id} was not found in your catalog.");
    }
    
    db.Catalog.Remove(itemToRemove);
    await db.SaveChangesAsync();
    return Results.Ok($"Successfully deleted '{itemToRemove.Title}' from the database catalog portfolio.");
})
.WithSummary("Delete Media Item by ID")
.WithDescription("Scans the physical SQL database table and permanently removes the matching media object record.");

// 5. METADATA UPDATE ROUTE - Completely overwrites database columns dynamically by ID
app.MapPut("/api/catalog/{id:int}", async (int id, MediaItem updatedItem, AppDbContext db) =>
{
    var existingItem = await db.Catalog.FirstOrDefaultAsync(item => item.Id == id);
    if (existingItem == null)
    {
        return Results.NotFound($"Item with ID {id} was not found in your catalog database.");
    }

    existingItem.Title = updatedItem.Title;
    existingItem.Creator = updatedItem.Creator;
    existingItem.ReleaseYear = updatedItem.ReleaseYear;
    existingItem.Rating = updatedItem.Rating;
    existingItem.Genre = updatedItem.Genre;

    await db.SaveChangesAsync();
    return Results.Ok(existingItem);
})
.WithSummary("Update Media Item Fields by ID")
.WithDescription("Scans the SQL database table, locates the targeted record, and completely updates its metadata fields dynamically.");

// LAUNCH ENVIRONMENT
app.Run();

// PHYSICAL DATABASE CORE INTERACTION MANAGER
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<MediaItem> Catalog { get; set; } = null!;
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
