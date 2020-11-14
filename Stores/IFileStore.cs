using System.Threading.Tasks;

namespace DevKnack.Stores
{
    /// <summary>
    /// A storage provider that can read and write files
    /// </summary>
    public interface IFileStore
    {
        /// <summary>
        /// Read a file
        /// </summary>
        Task<string?> ReadStringFileAsync(string url, string path);

        /// <summary>
        /// Write a file
        /// </summary>
        Task WriteStringFileAsync(string url, string path, string contents);

        /// <summary>
        /// Delete a file
        /// </summary>
        Task DeleteFileAsync(string url, string path);
    }
}
