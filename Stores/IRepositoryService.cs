using System.Threading.Tasks;

namespace DevKnack.Stores
{
    public interface IRepositoryService : IRepositoryQuery
    {
        Task CreateRepositoryAsync(string name);
    }
}