using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmailAgentServer.DBEntity;
using Microsoft.EntityFrameworkCore;

namespace EmailAgentServer;

public class EmailAgentDbContext:DbContext
{
    /**
     * Synchronize with database by:
     * dotnet ef migrations add {Update Info}
     * dotnet ef database update
     */
    
    private string DbPath { get; }
    
    public DbSet<User> User { get; set; }
    public DbSet<Setting> Setting { get; set; }
    
    public DbSet<Application> Application { get; set; }
    
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    
    public DbSet<EmailPlaceholder> EmailPlaceholders { get; set; }



    public EmailAgentDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "EmailAgent.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
            .Property(e => e.lastUpdateTime)
            .HasDefaultValueSql("datetime('now')");
        modelBuilder.Entity<Setting>()
            .Property(e => e.lastUpdateTime)
            .HasDefaultValueSql("datetime('now')");
        modelBuilder.Entity<Application>()
            .Property(e => e.lastUpdateTime)
            .HasDefaultValueSql("datetime('now')");
        modelBuilder.Entity<EmailTemplate>()
            .Property(e => e.LastUpdateTime)
            .HasDefaultValueSql("datetime('now')");
    }
}




