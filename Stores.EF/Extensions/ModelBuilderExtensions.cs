namespace Microsoft.EntityFrameworkCore
{
    public static class ModelBuilderExtensions
    {
        public static void BuildStoreModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("DevKnack.Stores.EF.FilesEntity", b =>
            {
                b.Property<string>("Path")
                    .IsRequired()
                    .HasMaxLength(300);

                b.Property<string>("Owner")
                    .IsRequired()
                    .HasMaxLength(100);

                b.Property<string>("Contents")
                    .IsRequired();

                b.HasKey("Owner", "Path");

                b.ToTable("Files");
            });
        }
    }
}