using System.ComponentModel.DataAnnotations;

namespace DevKnack.Stores.EF
{
    public class FilesEntity
    {
        [Required(AllowEmptyStrings = false), StringLength(300)]
        public string Path { get; set; } = "";

        [Required(AllowEmptyStrings = false), StringLength(100)]
        public string Owner { get; set; } = "";

        [Required(AllowEmptyStrings = true)]
        public string Contents { get; set; } = "";
    }
}