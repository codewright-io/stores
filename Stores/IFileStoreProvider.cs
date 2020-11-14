using System.Threading.Tasks;

namespace DevKnack.Stores
{
    public interface IFileStoreProvider
    {
        Task<IFileStore> CreateFileStoreAsync(string url);
    }
}