namespace System
{
    /// <summary>
    /// String helper functions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Trim byte order mark and zero width space characters
        /// </summary>
        public static string TrimBOM(this string s) => s.Trim(new char[] { '\uFEFF', '\u200B' });

        public static string Shorten(this string s, int length) => s.Length > length ? s.Substring(0, length) : s;
    }
}