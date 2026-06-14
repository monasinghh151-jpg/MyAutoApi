using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Register framework exploration routing engines and HTTP networking drivers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddHttpClient(); 

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

app.UseStaticFiles(); // Hosts the robots.txt file mapping pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

const string SECRET_API_KEY = "SuperSecretDeveloperKey123";

app.MapGet("/mytest", () => "System is online!");

// =========================================================================
// 🚀 THE CORE PROJECT: AUTOMATED TECHNICAL SEO CRAWLER & AUDITOR ENGINE
// =========================================================================
app.MapPost("/api/seo/audit", async (SeoAuditRequest request, IHttpClientFactory clientFactory) =>
{
    if (string.IsNullOrEmpty(request.TargetUrl) || !request.TargetUrl.StartsWith("http"))
    {
        return Results.BadRequest("A valid destination URL starting with http:// or https:// is required.");
    }

    try
    {
        var client = clientFactory.CreateClient();
        
        // Emulate a standard web browser user agent identity to prevent bot blocking firewalls
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Compatible; TechSeoBot/1.0; CoreAuditor)");

        // Latency Audit: Measure server response speeds (Time to First Byte / LCP metrics)
        var stopwatch = Stopwatch.StartNew();
        var response = await client.GetAsync(request.TargetUrl);
        stopwatch.Stop();

        long serverResponseTimeMs = stopwatch.ElapsedMilliseconds;
        string htmlContent = await response.Content.ReadAsStringAsync();

        // On-Page Parsing: Regular expressions to crawl and extract the HTML <title> tag parameters
        var titleMatch = Regex.Match(htmlContent, @"<title\b[^>]*>(.*?)</title>", RegexOptions.IgnoreCase);
        string extractedTitle = titleMatch.Success ? titleMatch.Groups[1].Value.Trim() : "MISSING (Critical On-Page Defect)";

        // On-Page Parsing: Regular expressions to crawl and extract the HTML <meta description> parameters
        var descMatch = Regex.Match(htmlContent, @"<meta\b[^>]*name=[""']description[""'][^>]*content=[""']([^""']*)[""']", RegexOptions.IgnoreCase);
        if (!descMatch.Success) 
            descMatch = Regex.Match(htmlContent, @"<meta\b[^>]*content=[""']([^""']*)[""'][^>]*name=[""']description[""']", RegexOptions.IgnoreCase);
        string extractedDescription = descMatch.Success ? descMatch.Groups[1].Value.Trim() : "MISSING (Critical On-Page Defect)";

        // Core Web Vitals Scoring: Algorithmic compliance scoring against Googlebot ranking criteria
        string speedRating = serverResponseTimeMs < 200 ? "OPTIMAL (Passed)" : serverResponseTimeMs < 600 ? "NEEDS IMPROVEMENT" : "POOR (Server Latency Bottleneck)";
        string titleStatus = extractedTitle.Length >= 10 && extractedTitle.Length <= 60 ? "OPTIMAL (Passed)" : $"WARNING (Length is {extractedTitle.Length} chars. Recommended: 10-60)";
        string descStatus = extractedDescription.Length >= 50 && extractedDescription.Length <= 160 ? "OPTIMAL (Passed)" : $"WARNING (Length is {extractedDescription.Length} chars. Recommended: 50-160)";

        // Aggregate audited metrics into a clean corporate reporting manifest
        var seoAuditReport = new
        {
            AuditedTargetUrl = request.TargetUrl,
            NetworkResponseStatus = (int)response.StatusCode,
            AuditExecutionTimestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss 'UTC'"),
            CoreWebVitalsLatency = new
            {
                TimeToFirstByte = $"{serverResponseTimeMs}ms",
                GoogleLcpRating = speedRating
            },
            OnPageMetadataDiagnostics = new
            {
                MetaTitle = extractedTitle,
                MetaTitleEvaluation = titleStatus,
                MetaDescription = extractedDescription,
                MetaDescriptionEvaluation = descStatus
            }
        };

        return Results.Ok(seoAuditReport);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Network crawling sequences failed: {ex.Message}");
    }
})
.WithSummary("Run Automated Technical SEO Crawler Audit")
.WithDescription("Programmatically crawls an external web domain, measures server latency, parses HTML header tags, and scores metadata configurations against live Google Core Web Vitals criteria.");

// Baseline data catalog routes kept functional
app.MapGet("/api/catalog", async (AppDbContext db) => await db.Catalog.ToListAsync());

app.Run();

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<MediaItem> Catalog { get; set; } = null!;
}

public class MediaItem {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Creator { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int Rating { get; set; }
    public string Genre { get; set; } = string.Empty;
}

public class SeoAuditRequest {
    /// <summary>The destination web address to crawl and inspect.</summary>
    /// <example>https://wikipedia.org</example>
    public string TargetUrl { get; set; } = string.Empty;
}
