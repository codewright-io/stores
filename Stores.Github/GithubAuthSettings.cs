namespace DevKnack.Stores.Github
{
    public class GithubAuthSettings
    {
        public GithubAuthSettings(string appName, long appId, string clientId, string clientSecret, bool disabled)
        {
            AppId = appId;
            AppName = appName;
            ClientId = clientId;
            ClientSecret = clientSecret;
            Disabled = disabled;
        }

        #region OAUTH settings

        /// <summary>Github App Name</summary>
        public string AppName { get; }

        /// <summary>Github App Id</summary>
        public long AppId { get; }

        /// <summary>Github client ID</summary>
        public string ClientId { get; }

        /// <summary>Github client secret</summary>
        public string ClientSecret { get; }

        public bool Disabled { get; }

        #endregion OAUTH settings
    }
}