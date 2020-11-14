using Microsoft.EntityFrameworkCore;

namespace DevKnack.Stores.EF
{
    internal static class StoresModelBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilesEntity>()
                .HasKey(e => new { e.Owner, e.Path });
        }
    }
}