namespace DevKnack.Common.Settings
{
    public class JwtAuthSettings
    {
        public JwtAuthSettings(string key, string issuer, bool disabled)
        {
            Key = key;
            Issuer = issuer;
            Disabled = disabled;
        }

        #region JWT settings
        /// <summary>JWT secret key</summary>
        public string Key { get; }

        /// <summary>JWT Issuer</summary>
        public string Issuer { get; }

        public bool Disabled { get; }

        #endregion JWT settings
    }
}
