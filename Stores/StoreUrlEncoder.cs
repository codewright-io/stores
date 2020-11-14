using System.Web;

namespace DevKnack.Stores
{
    /// <summary>
    /// Special URL encoder
    /// </summary>
    public static class StoreUrlEncoder
    {
        public static string Encode(string url)
        {
            if (url.Contains('/') || url.Contains(':'))
                return HttpUtility.UrlEncode(url);
            return url;

            //return url.Replace("https://github.com/", "gh:").Replace("git://github.com/", "gith:").Replace("/", "|");
        }

        public static string Decode(string data)
        {
            if (data.Contains('/') || data.Contains(':'))
                return data;

            return HttpUtility.UrlDecode(data);
            //return data.Replace("gh:", "https://github.com/").Replace("gith:", "git://github.com/").Replace("|", "/");
        }
    }
}