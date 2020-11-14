using Microsoft.EntityFrameworkCore;

namespace DevKnack.Stores.EF
{
    public class StoresDbContext : DbContext
    {
        public DbSet<FilesEntity> Files { get; set; } = null!;

        public StoresDbContext(DbContextOptions<StoresDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            StoresModelBuilder.Build(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}