using AutomationTestResultManager.Areas.Identity.Data;
using AutomationTestResultManager.CommonEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutomationTestResultManager.Data;

public class AutomationTestResultManagerContext : IdentityDbContext<ApplicationUser>
{
    public AutomationTestResultManagerContext(DbContextOptions<AutomationTestResultManagerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<ATRFeature> ATRFeatures { get; set; }
    public DbSet<ATRTestCase> ATRTestCases { get; set; }
    public DbSet<ATRComponent> ATRComponents { get; set; }
}
