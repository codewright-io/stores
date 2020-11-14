using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevKnack.Stores.EF.Migrations
{
    public class StoresInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateStoreTable();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropStoreTable();
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.BuildStoreModel();
        }
    }
}
