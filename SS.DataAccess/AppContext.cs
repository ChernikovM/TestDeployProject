using Microsoft.EntityFrameworkCore;
using SS.DataAccess.Entities;

namespace SS.DataAccess;

public sealed class AppContext : DbContext
{
    public DbSet<StickerPack> StickerPacks { get; set; }

    public DbSet<Label> Labels { get; set; }

    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
        this.Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<StickerPack>(
            entity =>
            {
                entity
                    .HasIndex(prop => prop.TelegramLink)
                    .IsUnique();
            });

        builder.Entity<Label>(
            entity =>
            {
                entity
                    .HasIndex(prop => prop.Name)
                    .IsUnique();
            });
    }
}