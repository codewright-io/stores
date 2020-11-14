using Blazored.LocalStorage;
using System.Threading.Tasks;

namespace DevKnack.Stores.Browser
{
    public class LocalStorageFileStore : IFileStore
    {
        private readonly ILocalStorageService _storageService;

        public LocalStorageFileStore(ILocalStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<string?> ReadStringFileAsync(string url, string path) => await _storageService.GetItemAsync<string>($"{url}&{path}");

        public Task WriteStringFileAsync(string url, string path, string contents) => _storageService.SetItemAsync($"{url}&{path}", contents);

        public Task DeleteFileAsync(string url, string path) => _storageService.RemoveItemAsync($"{url}&{path}");
    }
}