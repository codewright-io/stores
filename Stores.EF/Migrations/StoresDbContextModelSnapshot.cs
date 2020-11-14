using DevKnack.Stores.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DevKnack.Stores.EF.Migrations
{
    [DbContext(typeof(StoresDbContext))]
    public class StoresDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.BuildStoreModel();
        }
    }
}
