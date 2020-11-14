using DevKnack.Common.Exceptions;
using System.Threading.Tasks;

namespace DevKnack.Stores.EF
{
    public class EfFileStore : IFileStore
    {
        private readonly StoresDbContext _context;

        public EfFileStore(StoresDbContext context)
        {
            _context = context;
        }

        public async Task<string?> ReadStringFileAsync(string url, string path)
        {
            string owner = url.Replace("ef:", "");
            if (string.IsNullOrEmpty(owner))
                throw new NotFoundException(url);

            var entity = await _context.Files.FindAsync(owner, path);
            if (entity is null)
                return null; // throw new NotFoundException(url); ?

            return entity.Contents;
        }

        public async Task WriteStringFileAsync(string url, string path, string contents)
        {
            string owner = url.Replace("ef:", ""); // Remove the protocol
            if (string.IsNullOrEmpty(owner))
                throw new NotFoundException(url);

            var entity = await _context.Files.FindAsync(owner, path);
            if (entity is null)
            {
                await _context.Files.AddAsync(new FilesEntity { Path = path, Owner = owner, Contents = contents });
            }
            else
            {
                entity.Contents = contents;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteFileAsync(string url, string path)
        {
            string owner = url.Replace("ef:", "");
            if (string.IsNullOrEmpty(owner))
                throw new NotFoundException(url);

            var entity = await _context.Files.FindAsync(owner, path);
            if (entity is null)
                return; // throw new NotFoundException(url); ?

            _context.Files.Remove(entity);
        }
    }
}