namespace Microsoft.EntityFrameworkCore.Migrations
{
    public static class MigrationBuilderExtensions
    {
        public static void CreateStoreTable(this MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Path = table.Column<string>(maxLength: 300, nullable: false),
                    Owner = table.Column<string>(maxLength: 100, nullable: false),
                    Contents = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => new { x.Owner, x.Path });
                });
        }

        public static void DropStoreTable(this MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Files");
        }
    }
}