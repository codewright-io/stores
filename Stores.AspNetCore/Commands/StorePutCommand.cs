namespace DevKnack.Stores.AspNetCore.Commands
{
    public class StorePutCommand
    {
        public string Url { get; set; } = "";

        public string Path { get; set; } = "";

        public string Contents { get; set; } = "";
    }
}