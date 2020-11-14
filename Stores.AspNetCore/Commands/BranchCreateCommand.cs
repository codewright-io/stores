namespace DevKnack.Stores.AspNetCore.Commands
{
    public class BranchCreateCommand
    {
        public string Url { get; set; } = "";

        public string Name { get; set; } = "";

        public string SourceName { get; set; } = "";
    }
}