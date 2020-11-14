namespace DevKnack.Stores.AspNetCore.Commands
{
    public class CreatePullRequestCommand
    {
        public string Url { get; set; } = "";

        public string Name { get; set; } = "";

        public string TargetName { get; set; } = "";
    }
}