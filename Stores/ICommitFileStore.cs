using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevKnack.Stores
{
    /// <summary>
    /// A file storage provider that requires a secondary commit operation to force persistance
    /// e.g. a cache that downloads to another server, a git provider that submits pull-requests
    /// </summary>
    public interface ICommitFileStore : IFileStore
    {
        /// <summary>
        /// Get a list of changes pending commit
        /// </summary>
        Task<IEnumerable<(string, string)>> GetChangesAsync();

        /// <summary>
        /// Commit all changes, and flush the pending changes list
        /// </summary>
        Task CommitAllAsync();
    }
}
